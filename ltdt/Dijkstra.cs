/*using System;
using System.Collections.Generic;
using System.Linq;

namespace DijkstraFlightFinder
{
    public class Dijkstra
    {
        public Dictionary<string, int> Distances { get; private set; }
        public Dictionary<string, string> Previous { get; private set; }
        public HashSet<string> Visited { get; private set; }
        public List<string> ShortestPath { get; private set; }

        private Dictionary<string, Dictionary<string, int>> graph;

        public Dictionary<string, int> SccId { get; private set; }
        private Dictionary<string, int> indexMap;
        private Dictionary<string, int> lowLink;
        private Stack<string> stackScc;
        private int indexCounter;
        private int sccCount;

        public Dijkstra(Dictionary<string, Dictionary<string, int>> graph)
        {
            this.graph = graph;
            FindScc();
        }

        public List<string> Run(string start, string end, Action<string> debug = null)
        {
            Distances = new Dictionary<string, int>();
            Previous = new Dictionary<string, string>();
            Visited = new HashSet<string>();

            var unvisited = graph.Keys.ToList();

            foreach (var city in graph.Keys)
            {
                Distances[city] = int.MaxValue;
                Previous[city] = null;
            }

            Distances[start] = 0;

            while (unvisited.Count > 0)
            {
                string current = unvisited
                    .Where(c => Distances[c] != int.MaxValue)
                    .OrderBy(c => Distances[c])
                    .FirstOrDefault();

                if (current == null)
                    break;

                unvisited.Remove(current);
                Visited.Add(current);

                debug?.Invoke($"Đang xét: {current} (cost: {Distances[current]})");

                if (current == end)
                    break;

                foreach (var next in graph[current])
                {
                    int newCost = Distances[current] + next.Value;

                    if (newCost < Distances[next.Key])
                    {
                        debug?.Invoke($"  → Cập nhật {next.Key}: {Distances[next.Key]} → {newCost}");
                        Distances[next.Key] = newCost;
                        Previous[next.Key] = current;
                    }
                }
            }

            ShortestPath = BuildPath(start, end);
            return ShortestPath;
        }

        private List<string> BuildPath(string start, string end)
        {
            var path = new List<string>();
            string node = end;

            while (node != null)
            {
                path.Insert(0, node);
                node = Previous[node];
            }

            if (path[0] != start)
                return null;

            return path;
        }

        // ====================== TARJAN SCC =========================

        private void FindScc()
        {
            SccId = new Dictionary<string, int>();
            indexMap = new Dictionary<string, int>();
            lowLink = new Dictionary<string, int>();
            stackScc = new Stack<string>();
            indexCounter = 0;
            sccCount = 0;

            foreach (var node in graph.Keys)
            {
                if (!indexMap.ContainsKey(node))
                    Tarjan(node);
            }
        }

        private void Tarjan(string u)
        {
            indexMap[u] = lowLink[u] = indexCounter++;
            stackScc.Push(u);

            foreach (var v in graph[u].Keys)
            {
                if (!indexMap.ContainsKey(v))
                {
                    Tarjan(v);
                    lowLink[u] = Math.Min(lowLink[u], lowLink[v]);
                }
                else if (!SccId.ContainsKey(v))
                {
                    lowLink[u] = Math.Min(lowLink[u], indexMap[v]);
                }
            }

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
*/