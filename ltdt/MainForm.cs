using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DijkstraFlightFinder
{
    public partial class MainForm : Form
    {
        private GraphData graphManager;
        private MapRenderer mapRenderer;

        public MainForm()
        {
            InitializeComponent();

            graphManager = new GraphData();
            mapRenderer = new MapRenderer();

            cboStart.Items.AddRange(graphManager.Graph.Keys.ToArray());
            cboEnd.Items.AddRange(graphManager.Graph.Keys.ToArray());
            cboStart.SelectedIndex = 0;
            cboEnd.SelectedIndex = 1;
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            string start = cboStart.SelectedItem.ToString();
            string end = cboEnd.SelectedItem.ToString();

            if (start == end)
            {
                MessageBox.Show("Điểm đi và điểm đến phải khác nhau!");
                return;
            }

            lstSteps.Items.Clear();
            lstSteps.Items.Add("=== BẮT ĐẦU DIJKSTRA ===");
            lstSteps.Items.Add($"Từ: {start} → Đến: {end}");
            lstSteps.Items.Add("");

            graphManager.RunDijkstra(start, end, s => lstSteps.Items.Add(s));

            if (graphManager.ShortestPath != null)
            {
                lblResult.Text =
                    $"Đường bay rẻ nhất: {string.Join(" → ", graphManager.ShortestPath)}\n" +
                    $"Tổng chi phí: ${graphManager.Distances[end]}";
            }
            else lblResult.Text = "Không tìm thấy đường bay!";

            pnlGraph.Invalidate();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cboStart.SelectedIndex = 0;
            cboEnd.SelectedIndex = 1;
            lstSteps.Items.Clear();
            lblResult.Text = "";
            pnlGraph.Invalidate();
        }

        private void PnlGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

           

            var graph = graphManager.Graph;
            var pos = graphManager.CityPositions;

            // VẼ CÁC CUNG BAY + THÀNH PHỐ
            foreach (var city in graph)
            {
                Point p1 = pos[city.Key];

                foreach (var neighbor in city.Value)
                {
                    Point p2 = pos[neighbor.Key];
                    bool isInPath =
                        graphManager.ShortestPath != null &&
                        graphManager.ShortestPath.Contains(city.Key) &&
                        graphManager.ShortestPath.Contains(neighbor.Key) &&
                        Math.Abs(graphManager.ShortestPath.IndexOf(city.Key) -
                                 graphManager.ShortestPath.IndexOf(neighbor.Key)) == 1;

                    using (Pen pen = new Pen(isInPath ? Color.Red : Color.Gray, isInPath ? 3 : 2))
                    {
                        pen.DashStyle = isInPath ?
                            System.Drawing.Drawing2D.DashStyle.Solid :
                            System.Drawing.Drawing2D.DashStyle.Dash;

                        mapRenderer.DrawCurvedLine(g, pen, p1, p2);
                    }
                }
            }

            // VẼ THÀNH PHỐ
            foreach (var city in pos)
            {
                Point p = city.Value;
                bool visited = graphManager.Visited?.Contains(city.Key) ?? false;
                bool inPath = graphManager.ShortestPath?.Contains(city.Key) ?? false;

                Color color = inPath ? Color.Red :
                              visited ? Color.LimeGreen :
                              Color.Gold;

                using (Brush b = new SolidBrush(color))
                    g.FillEllipse(b, p.X - 5, p.Y - 5, 10, 10);

                g.DrawString(city.Key, new Font("Arial", 9, FontStyle.Bold),
                    Brushes.White, p.X + 8, p.Y);
            }
        }
    }
}
