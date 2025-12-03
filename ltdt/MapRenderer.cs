using System;
using System.Drawing;

namespace DijkstraFlightFinder
{
    public class MapRenderer
    {
        public void DrawCurvedLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            int midX = (p1.X + p2.X) / 2;
            int midY = (p1.Y + p2.Y) / 2;

            double distance = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
            int curveOffset = (int)(distance * 0.15);

            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double len = Math.Sqrt(dx * dx + dy * dy);

            int controlX = midX - (int)((dy / len) * curveOffset);
            int controlY = midY + (int)((dx / len) * curveOffset);

            Point ctrl = new Point(controlX, controlY);

            g.DrawBezier(pen, p1, ctrl, ctrl, p2);
            DrawArrowHead(g, pen, ctrl, p2);
        }

        public void DrawArrowHead(Graphics g, Pen pen, Point from, Point to)
        {
            int size = 12;
            double angle = Math.Atan2(to.Y - from.Y, to.X - from.X);

            Point p1 = new Point(
                (int)(to.X - size * Math.Cos(angle - Math.PI / 6)),
                (int)(to.Y - size * Math.Sin(angle - Math.PI / 6))
            );

            Point p2 = new Point(
                (int)(to.X - size * Math.Cos(angle + Math.PI / 6)),
                (int)(to.Y - size * Math.Sin(angle + Math.PI / 6))
            );

            g.DrawLine(pen, to, p1);
            g.DrawLine(pen, to, p2);
        }

        public void DrawWorldMap(Graphics g, Rectangle area)
        {
            /*var oceanBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                area, Color.FromArgb(173, 216, 230), Color.FromArgb(135, 206, 235), 45f);

            g.FillRectangle(oceanBrush, area);

            Pen gridPen = new Pen(Color.FromArgb(50, 200, 200, 200), 1)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
            };

            for (int i = 0; i <= area.Height; i += area.Height / 6)
                g.DrawLine(gridPen, 0, i, area.Width, i);
            for (int i = 0; i <= area.Width; i += area.Width / 8)
                g.DrawLine(gridPen, i, 0, i, area.Height);
            */
            // Các vùng bản đồ... (giữ nguyên từ code cũ)
            // TÔM GỌN để tránh lặp quá dài — nhưng nội dung giống y hệt bản gốc
            /*Brush landBrush = new SolidBrush(Color.FromArgb(220, 245, 222));
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

            // Đông Nam Á 
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
            */
        }
    }
}
