
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DijkstraFlightFinder
{
    public partial class MainForm : Form
    {
        private Dictionary<string, Point> cityPositions;
        private Dictionary<string, Dictionary<string, int>> graph;
        private List<string> shortestPath;
        private Dictionary<string, int> distances;
        private Dictionary<string, string> previous;
        private HashSet<string> visited;

        public MainForm()
        {
            InitializeComponent();
            InitializeGraph();
            InitializeCityPositions();
        }

        private void InitializeGraph()
        {
            graph = new Dictionary<string, Dictionary<string, int>>
            {
                ["New York"] = new Dictionary<string, int>
                {
                    ["London"] = 350,
                    ["Paris"] = 400,
                    ["Tokyo"] = 1100
                },
                ["London"] = new Dictionary<string, int>
                {
                    ["New York"] = 350,
                    ["Paris"] = 100,
                    ["Dubai"] = 450,
                    ["Singapore"] = 700
                },
                ["Paris"] = new Dictionary<string, int>
                {
                    ["New York"] = 400,
                    ["London"] = 100,
                    ["Dubai"] = 380,
                    ["Sydney"] = 950
                },
                ["Tokyo"] = new Dictionary<string, int>
                {
                    ["New York"] = 1100,
                    ["Singapore"] = 550,
                    ["Sydney"] = 600
                },
                ["Dubai"] = new Dictionary<string, int>
                {
                    ["London"] = 450,
                    ["Paris"] = 380,
                    ["Singapore"] = 420,
                    ["Sydney"] = 800
                },
                ["Singapore"] = new Dictionary<string, int>
                {
                    ["London"] = 700,
                    ["Tokyo"] = 550,
                    ["Dubai"] = 420,
                    ["Sydney"] = 480
                },
                ["Sydney"] = new Dictionary<string, int>
                {
                    ["Paris"] = 950,
                    ["Tokyo"] = 600,
                    ["Dubai"] = 800,
                    ["Singapore"] = 480
                }
            };

            cboStart.Items.AddRange(graph.Keys.ToArray());
            cboEnd.Items.AddRange(graph.Keys.ToArray());
            cboStart.SelectedIndex = 0;
            cboEnd.SelectedIndex = 1;
        }

        private void InitializeCityPositions()
        {
            cityPositions = new Dictionary<string, Point>
            {
                ["New York"] = new Point(180, 170),      
                ["London"] = new Point(370, 140),        
                ["Paris"] = new Point(385, 155),        
                ["Tokyo"] = new Point(640, 165),         
                ["Dubai"] = new Point(450, 210),         
                ["Singapore"] = new Point(550, 280),    
                ["Sydney"] = new Point(640, 360)         
            };
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            string start = cboStart.SelectedItem.ToString();
            string end = cboEnd.SelectedItem.ToString();

            if (start == end)
            {
                MessageBox.Show("Điểm đi và điểm đến phải khác nhau!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lstSteps.Items.Clear();
            lstSteps.Items.Add("=== BẮT ĐẦU THUẬT TOÁN DIJKSTRA ===");
            lstSteps.Items.Add($"Từ: {start} → Đến: {end}");
            lstSteps.Items.Add("");

            Dijkstra(start, end);

            if (shortestPath != null && shortestPath.Count > 0)
            {
                string path = string.Join(" → ", shortestPath);
                int totalCost = distances[end];
                lblResult.Text = $"Đường bay rẻ nhất: {path}\n" +
                               $"Tổng chi phí: ${totalCost}";
            }
            else
            {
                lblResult.Text = "Không tìm thấy đường bay!";
            }

            pnlGraph.Invalidate();
        }

        private void DrawCurvedLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            int midX = (p1.X + p2.X) / 2;
            int midY = (p1.Y + p2.Y) / 2;

            double distance = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
            int curveOffset = (int)(distance * 0.15);

            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double length = Math.Sqrt(dx * dx + dy * dy);

            int controlX = midX - (int)((dy / length) * curveOffset);
            int controlY = midY + (int)((dx / length) * curveOffset);

            Point controlPoint = new Point(controlX, controlY);

            g.DrawBezier(pen, p1, controlPoint, controlPoint, p2);

            DrawArrowHead(g, pen.Brush, controlPoint, p2);
        }

        private void DrawArrowHead(Graphics g, Brush brush, Point from, Point to)
        {
            double angle = Math.Atan2(to.Y - from.Y, to.X - from.X);
            double arrowLength = 8;
            double arrowAngle = Math.PI / 6;

            Point arrowPoint1 = new Point(
                (int)(to.X - arrowLength * Math.Cos(angle - arrowAngle)),
                (int)(to.Y - arrowLength * Math.Sin(angle - arrowAngle))
            );

            Point arrowPoint2 = new Point(
                (int)(to.X - arrowLength * Math.Cos(angle + arrowAngle)),
                (int)(to.Y - arrowLength * Math.Sin(angle + arrowAngle))
            );

            g.FillPolygon(brush, new Point[] { to, arrowPoint1, arrowPoint2 });
        }

        private void Dijkstra(string start, string end)
        {
            distances = new Dictionary<string, int>();
            previous = new Dictionary<string, string>();
            visited = new HashSet<string>();
            var unvisited = new List<string>(graph.Keys);

            foreach (var city in graph.Keys)
            {
                distances[city] = int.MaxValue;
                previous[city] = null;
            }
            distances[start] = 0;

            lstSteps.Items.Add("Khởi tạo:");
            lstSteps.Items.Add($"  {start}: 0 (điểm xuất phát)");
            lstSteps.Items.Add($"  Các thành phố khác: ∞");
            lstSteps.Items.Add("");

            int step = 1;
            while (unvisited.Count > 0)
            {
                string current = unvisited
                    .Where(c => distances[c] != int.MaxValue)
                    .OrderBy(c => distances[c])
                    .FirstOrDefault();

                if (current == null || distances[current] == int.MaxValue)
                    break;

                unvisited.Remove(current);
                visited.Add(current);

                lstSteps.Items.Add($"Bước {step++}: Xét thành phố '{current}' (chi phí: ${distances[current]})");

                if (current == end)
                {
                    lstSteps.Items.Add($"  ✓ Đã đến đích!");
                    break;
                }

                foreach (var neighbor in graph[current])
                {
                    if (visited.Contains(neighbor.Key))
                        continue;

                    int newDist = distances[current] + neighbor.Value;

                    if (newDist < distances[neighbor.Key])
                    {
                        int oldDist = distances[neighbor.Key];
                        distances[neighbor.Key] = newDist;
                        previous[neighbor.Key] = current;

                        string oldDistStr = oldDist == int.MaxValue ? "∞" : $"${oldDist}";
                        lstSteps.Items.Add($"  → Cập nhật '{neighbor.Key}': {oldDistStr} → ${newDist}");
                    }
                    else
                    {
                        lstSteps.Items.Add($"  → Giữ nguyên '{neighbor.Key}': ${distances[neighbor.Key]}");
                    }
                }
                lstSteps.Items.Add("");
            }

            shortestPath = new List<string>();
            string curr = end;
            while (curr != null)
            {
                shortestPath.Insert(0, curr);
                curr = previous[curr];
            }

            if (shortestPath[0] != start)
                shortestPath = null;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cboStart.SelectedIndex = 0;
            cboEnd.SelectedIndex = 1;
            lblResult.Text = "";
            lstSteps.Items.Clear();
            shortestPath = null;
            distances = null;
            visited = null;
            pnlGraph.Invalidate();
        }

        private void PnlGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Vẽ nền bản đồ thế giới
            DrawWorldMap(g);

            // Vẽ các cạnh (đường bay)
            foreach (var city in graph)
            {
                Point p1 = cityPositions[city.Key];
                foreach (var neighbor in city.Value)
                {
                    Point p2 = cityPositions[neighbor.Key];

                    bool isInPath = shortestPath != null &&
                                   shortestPath.Contains(city.Key) &&
                                   shortestPath.Contains(neighbor.Key) &&
                                   Math.Abs(shortestPath.IndexOf(city.Key) -
                                           shortestPath.IndexOf(neighbor.Key)) == 1;

                    // Vẽ đường bay 
                    if (isInPath)
                    {
                        Pen flightPen = new Pen(Color.FromArgb(255, 0, 0), 3);
                        flightPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        DrawCurvedLine(g, flightPen, p1, p2);
                    }
                    else
                    {
                        Pen flightPen = new Pen(Color.FromArgb(100, 128, 128, 128), 2);
                        flightPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        DrawCurvedLine(g, flightPen, p1, p2);
                    }

                    int midX = (p1.X + p2.X) / 2;
                    int midY = (p1.Y + p2.Y) / 2 - 20; 
                    g.FillEllipse(isInPath ? Brushes.Yellow : Brushes.White,
                        midX - 18, midY - 10, 36, 20);
                    g.DrawEllipse(new Pen(isInPath ? Color.Red : Color.Gray, 1),
                        midX - 18, midY - 10, 36, 20);

                    g.DrawString($"${neighbor.Value}",
                        new Font("Arial", 7, FontStyle.Bold),
                        isInPath ? Brushes.DarkRed : Brushes.Black,
                        midX - 15, midY - 6);
                }
            }

            foreach (var city in cityPositions)
            {
                Point p = city.Value;
                bool isVisited = visited != null && visited.Contains(city.Key);
                bool isInPath = shortestPath != null && shortestPath.Contains(city.Key);

                if (isInPath)
                {
                    g.FillEllipse(new SolidBrush(Color.FromArgb(80, 255, 0, 0)),
                        p.X - 35, p.Y - 35, 70, 70);
                }

                Color cityColor = isInPath ? Color.FromArgb(255, 69, 0) :
                                 isVisited ? Color.FromArgb(50, 205, 50) :
                                 Color.FromArgb(255, 215, 0);

                g.FillEllipse(new SolidBrush(cityColor), p.X - 20, p.Y - 35, 40, 40);
                g.FillPolygon(new SolidBrush(cityColor), new Point[] {
                    new Point(p.X - 10, p.Y - 5),
                    new Point(p.X + 10, p.Y - 5),
                    new Point(p.X, p.Y + 10)
                });

                g.DrawEllipse(new Pen(Color.White, 3), p.X - 20, p.Y - 35, 40, 40);
                g.DrawEllipse(new Pen(Color.Black, 2), p.X - 20, p.Y - 35, 40, 40);

                g.FillEllipse(Brushes.White, p.X - 8, p.Y - 23, 16, 16);
                g.DrawString("✈", new Font("Arial", 10), Brushes.Black, p.X - 7, p.Y - 25);

                SizeF textSize = g.MeasureString(city.Key, new Font("Arial", 9, FontStyle.Bold));
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, 255, 255, 255)),
                    p.X - textSize.Width / 2 - 3, p.Y + 15, textSize.Width + 6, textSize.Height + 2);
                g.DrawRectangle(new Pen(Color.Black, 1),
                    p.X - textSize.Width / 2 - 3, p.Y + 15, textSize.Width + 6, textSize.Height + 2);

                g.DrawString(city.Key, new Font("Arial", 9, FontStyle.Bold),
                    Brushes.Black, p.X - textSize.Width / 2, p.Y + 16);

                if (distances != null && distances.ContainsKey(city.Key))
                {
                    string distText = distances[city.Key] == int.MaxValue ?
                        "∞" : $"${distances[city.Key]}";
                    SizeF costSize = g.MeasureString(distText, new Font("Arial", 10, FontStyle.Bold));

                    g.FillEllipse(new SolidBrush(Color.FromArgb(220, 255, 255, 0)),
                        p.X - 22, p.Y + 32, 44, 20);
                    g.DrawEllipse(new Pen(Color.DarkBlue, 2),
                        p.X - 22, p.Y + 32, 44, 20);

                    g.DrawString(distText, new Font("Arial", 10, FontStyle.Bold),
                        Brushes.DarkBlue, p.X - costSize.Width / 2, p.Y + 34);
                }
            }
        }

        private void DrawWorldMap(Graphics g)
        {
            System.Drawing.Drawing2D.LinearGradientBrush oceanBrush =
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, pnlGraph.Width, pnlGraph.Height),
                    Color.FromArgb(173, 216, 230),  
                    Color.FromArgb(135, 206, 235), 
                    45F);
            g.FillRectangle(oceanBrush, 0, 0, pnlGraph.Width, pnlGraph.Height);

            Pen gridPen = new Pen(Color.FromArgb(50, 200, 200, 200), 1);
            gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            for (int i = 0; i <= pnlGraph.Height; i += pnlGraph.Height / 6)
            {
                g.DrawLine(gridPen, 0, i, pnlGraph.Width, i);
            }

            for (int i = 0; i <= pnlGraph.Width; i += pnlGraph.Width / 8)
            {
                g.DrawLine(gridPen, i, 0, i, pnlGraph.Height);
            }

            Brush landBrush = new SolidBrush(Color.FromArgb(220, 245, 222));
            Pen landPen = new Pen(Color.FromArgb(34, 139, 34), 1);

            // Bắc Mỹ
            Point[] northAmerica = {
                new Point(50, 120), new Point(120, 100), new Point(200, 110),
                new Point(240, 140), new Point(220, 200), new Point(180, 240),
                new Point(140, 250), new Point(100, 230), new Point(70, 180)
            };
            g.FillPolygon(landBrush, northAmerica);
            g.DrawPolygon(landPen, northAmerica);

            // Nam Mỹ
            Point[] southAmerica = {
                new Point(180, 250), new Point(220, 260), new Point(240, 300),
                new Point(230, 360), new Point(200, 400), new Point(180, 390),
                new Point(170, 340), new Point(160, 280)
            };
            g.FillPolygon(landBrush, southAmerica);
            g.DrawPolygon(landPen, southAmerica);

            // Châu Âu
            Point[] europe = {
                new Point(340, 100), new Point(420, 90), new Point(450, 110),
                new Point(440, 140), new Point(400, 160), new Point(360, 150),
                new Point(330, 130)
            };
            g.FillPolygon(landBrush, europe);
            g.DrawPolygon(landPen, europe);

            // Châu Phi
            Point[] africa = {
                new Point(360, 180), new Point(420, 170), new Point(460, 200),
                new Point(470, 260), new Point(450, 320), new Point(400, 360),
                new Point(370, 350), new Point(340, 310), new Point(330, 240)
            };
            g.FillPolygon(landBrush, africa);
            g.DrawPolygon(landPen, africa);

            // Châu Á
            Point[] asia = {
                new Point(450, 100), new Point(550, 90), new Point(620, 100),
                new Point(660, 130), new Point(650, 180), new Point(600, 210),
                new Point(520, 200), new Point(480, 170), new Point(460, 130)
            };
            g.FillPolygon(landBrush, asia);
            g.DrawPolygon(landPen, asia);

            // Đông Nam Á (bán đảo)
            Point[] southeastAsia = {
                new Point(520, 210), new Point(560, 230), new Point(580, 260),
                new Point(570, 290), new Point(540, 300), new Point(510, 280)
            };
            g.FillPolygon(landBrush, southeastAsia);
            g.DrawPolygon(landPen, southeastAsia);

            // Úc
            Point[] australia = {
                new Point(580, 330), new Point(660, 320), new Point(700, 350),
                new Point(690, 390), new Point(630, 410), new Point(580, 400),
                new Point(560, 370)
            };
            g.FillPolygon(landBrush, australia);
            g.DrawPolygon(landPen, australia);

            // Tên đại dương
            Font oceanFont = new Font("Arial", 9, FontStyle.Italic);
            Brush oceanTextBrush = new SolidBrush(Color.FromArgb(150, 0, 100, 150));

            g.DrawString("ATLANTIC", oceanFont, oceanTextBrush, 250, 300);
            g.DrawString("PACIFIC", oceanFont, oceanTextBrush, 100, 350);
            g.DrawString("PACIFIC", oceanFont, oceanTextBrush, 680, 250);
            g.DrawString("INDIAN", oceanFont, oceanTextBrush, 500, 350);
        }
    }
}


