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
            
            // Tính khoảng cách giữa p1 và p2
            double distance = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));

            // Độ cong tỷ lệ theo khoảng cách
            int curveOffset = (int)(distance * 0.15);


            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;

            // Độ dài vector
            double len = Math.Sqrt(dx * dx + dy * dy);


            // Tạo điểm điều khiển (control point)
            // Công thức: xoay vector vuông góc 90 độ và dịch ra một đoạn curveOffset
            int controlX = midX - (int)((dy / len) * curveOffset);
            int controlY = midY + (int)((dx / len) * curveOffset);

            Point ctrl = new Point(controlX, controlY);


            // Vẽ đường cong Bezier:
            // p1 → ctrl → ctrl → p2
            // (dùng 2 control point giống nhau tạo đường cong nhẹ, mềm)
            g.DrawBezier(pen, p1, ctrl, ctrl, p2);
            DrawArrowHead(g, pen, ctrl, p2);
        }


        /*
         ============================================================
         VẼ MŨI TÊN Ở CUỐI ĐƯỜNG BAY
         ============================================================
        */
        public void DrawArrowHead(Graphics g, Pen pen, Point from, Point to)
        {
            int size = 15;
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
    }
}
