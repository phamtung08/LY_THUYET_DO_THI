using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DijkstraFlightFinder
{
    public class GraphData
    {
       
        public Dictionary<string, Dictionary<string, int>> Graph { get; private set; }

    
        public Dictionary<string, Point> CityPositions { get; private set; }

        // Dijkstra:
        public Dictionary<string, int> Distances { get; private set; }
        public Dictionary<string, string> Previous { get; private set; }
        public HashSet<string> Visited { get; private set; }
        public List<string> ShortestPath { get; private set; }

        // Tarjan SCC:
        public Dictionary<string, int> SccId { get; private set; }  // city -> id SCC
        private int sccCount;        // số SCC tìm được
        private int indexCounter;    // thứ tự DFS
        private Dictionary<string, int> indexMap;  
        private Dictionary<string, int> lowLink;   
        private Stack<string> stackScc;

        public GraphData()
        {
            InitializeGraph();           
            InitializeCityPositions();   
            FindSCCs();                  
        }

        /* ==========================
              KHỞI TẠO ĐỒ THỊ
           ========================== */
        private void InitializeGraph()
        {
            Graph = new Dictionary<string, Dictionary<string, int>>
            {
                ["New York"] = new Dictionary<string, int>
                {
                    ["London"] = 350,
                    ["Paris"] = 480,
                    ["Tokyo"] = 1500
                },

                ["London"] = new Dictionary<string, int>
                {
                    ["New York"] = 360,
                    ["Paris"] = 100,
                    ["Dubai"] = 450,
                    ["Singapore"] = 700
                },

                ["Paris"] = new Dictionary<string, int>
                {
                    ["New York"] = 420,
                    ["London"] = 105,
                    ["Dubai"] = 380,
                    ["Sydney"] = 950
                },

                ["Tokyo"] = new Dictionary<string, int>
                {
                    ["Singapore"] = 550,
                    ["Sydney"] = 600,
                    ["HaNoi"] = 100
                },

                ["HaNoi"] = new Dictionary<string, int>
                {
                    ["Tokyo"] = 100,
                    ["Singapore"] = 60,
                    ["Dubai"] = 500,
                    ["Sydney"] = 215
                },

                ["Dubai"] = new Dictionary<string, int>
                {
                    ["London"] = 470,
                    ["Paris"] = 400,
                    ["Singapore"] = 420,
                    ["Sydney"] = 800
                },

                ["Singapore"] = new Dictionary<string, int>
                {
                    ["London"] = 720,
                    ["Tokyo"] = 350,
                    ["Dubai"] = 430,
                    ["Sydney"] = 480,
                    ["HaNoi"] = 200
                },

                ["Sydney"] = new Dictionary<string, int>
                {
                    ["Paris"] = 960,
                    ["Tokyo"] = 650,
                    ["Dubai"] = 820,
                    ["Singapore"] = 500
                }
            };
        }

        /* ==========================
              TỌA ĐỘ ĐỂ VẼ BẢN ĐỒ
           ========================== */
        private void InitializeCityPositions()
        {
            CityPositions = new Dictionary<string, Point>
            {
                ["New York"] = new Point(180, 170),
                ["London"] = new Point(360, 105),
                ["Paris"] = new Point(365, 120),
                ["Tokyo"] = new Point(700, 165),
                ["HaNoi"] = new Point(615, 210),
                ["Dubai"] = new Point(500, 200),
                ["Singapore"] = new Point(600, 260),
                ["Sydney"] = new Point(730, 360)
            };
        }

        /* ==========================
                DIJKSTRA
           ========================== */
        public void RunDijkstra(string start, string end, Action<string> log)
        {
            // Mỗi thành phố: chi phí đi từ start -> city
            Distances = new Dictionary<string, int>();

            // previous[city] = thành phố đứng trước trong đường ngắn nhất
            Previous = new Dictionary<string, string>();

            Visited = new HashSet<string>();
            var unvisited = new List<string>(Graph.Keys);

            // Khởi tạo
            foreach (var city in Graph.Keys)
            {
                Distances[city] = int.MaxValue;  // chưa có đường đi
                Previous[city] = null;
            }

            Distances[start] = 0;  // điểm bắt đầu

            log("Khởi tạo:");
            log($"  {start}: 0 (điểm xuất phát)");
            log("  Các thành phố khác: ∞\n");

            int step = 1;

            while (unvisited.Count > 0)
            {
                // Lấy thành phố có chi phí nhỏ nhất
                string current = unvisited
                    .Where(c => Distances[c] != int.MaxValue)
                    .OrderBy(c => Distances[c])
                    .FirstOrDefault();

                if (current == null) break;  // không thể đi tiếp

                unvisited.Remove(current);
                Visited.Add(current);

                log($"Bước {step++}: Xét '{current}' (chi phí: ${Distances[current]})");

                // Nếu đã tới đích thì dừng
                if (current == end)
                {
                    log("  ✓ Đã đến đích!");
                    break;
                }

                // Relax cạnh
                foreach (var neighbor in Graph[current])
                {
                    if (Visited.Contains(neighbor.Key)) continue;

                    int newDist = Distances[current] + neighbor.Value;

                    if (newDist < Distances[neighbor.Key])
                    {
                        string oldVal = Distances[neighbor.Key] == int.MaxValue ? "∞" : $"${Distances[neighbor.Key]}";
                        log($"  → Cập nhật '{neighbor.Key}': {oldVal} → ${newDist}");

                        Distances[neighbor.Key] = newDist;
                        Previous[neighbor.Key] = current;
                    }
                }

                log("");
            }

            BuildShortestPath(start, end);
        }

        // Tạo danh sách đường đi sau khi chạy Dijkstra
        private void BuildShortestPath(string start, string end)
        {
            ShortestPath = new List<string>();
            string curr = end;

            while (curr != null)
            {
                ShortestPath.Insert(0, curr);
                curr = Previous[curr];
            }

            // Nếu không tìm được đường đi hợp lệ
            if (ShortestPath[0] != start)
                ShortestPath = null;
        }

        /* ==========================
                TARJAN SCC
           ========================== */
        private void FindSCCs()
        {
            SccId = new Dictionary<string, int>();
            indexMap = new Dictionary<string, int>();
            lowLink = new Dictionary<string, int>();
            stackScc = new Stack<string>();
            sccCount = 0;
            indexCounter = 0;

            // Duyệt từng node
            foreach (var node in Graph.Keys)
            {
                if (!indexMap.ContainsKey(node))
                    Tarjan(node);
            }
        }

        // DFS Tarjan
        private void Tarjan(string u)
        {
            indexMap[u] = lowLink[u] = indexCounter++;
            stackScc.Push(u);

            foreach (var v in Graph[u].Keys)
            {
                if (!indexMap.ContainsKey(v))
                {
                    Tarjan(v);
                    lowLink[u] = Math.Min(lowLink[u], lowLink[v]);
                }
                else if (!SccId.ContainsKey(v)) // v còn trong stack
                {
                    lowLink[u] = Math.Min(lowLink[u], indexMap[v]);
                }
            }

            // Nếu u là root của SCC
            if (lowLink[u] == indexMap[u])
            {
                string v;
                do
                {
                    v = stackScc.Pop();
                    SccId[v] = sccCount;
                }
                while (v != u);

                sccCount++;
            }
        }
    }
}
