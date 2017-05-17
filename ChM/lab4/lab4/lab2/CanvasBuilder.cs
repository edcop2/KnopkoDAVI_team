using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace lab4
{
    public class CanvasBuilder
    {
        public delegate double MyFunc(double x);

        private static double width = 0;
        private static double height = 0;
        private static double xScale = 0;
        private static double yScale = 0;
        private static double x0 = 0;
        private static double y0 = 0;

        public static void ClearCanvas(Canvas canvas)
        {
            canvas.Children.Clear();

            width = 0;
            height = 0;
            xScale = 0;
            yScale = 0;
            x0 = 0;
            y0 = 0;
        }

        public static void DrawGrid(Canvas canvas, double xMax, double xMin, double yMax, double yMin)
        {
            width = canvas.ActualWidth;
            height = canvas.ActualHeight;
            xScale = width / (xMax - xMin);
            yScale = height / (yMax - yMin);
            x0 = -xMin * xScale;
            y0 = yMax * yScale;

            // Сетка
            double xStep = 1;
            while (xStep * xScale < 25)
                xStep *= 10;
            while (xStep * xScale > 250)
                xStep /= 10;
            for (double dx = xStep; dx < xMax; dx += xStep)
            {
                double x = x0 + dx * xScale;
                AddLine(canvas, Brushes.LightGray, x, 0, x, height);
                AddText(canvas, string.Format("{0:0.###}", dx), x + 2, y0 + 2);
            }
            for (double dx = -xStep; dx >= xMin; dx -= xStep)
            {
                double x = x0 + dx * xScale;
                AddLine(canvas, Brushes.LightGray, x, 0, x, height);
                AddText(canvas, string.Format("{0:0.###}", dx), x + 2, y0 + 2);
            }
            double yStep = 1;
            while (yStep * yScale < 20)
                yStep *= 10;
            while (yStep * yScale > 200)
                yStep /= 10;
            for (double dy = yStep; dy < yMax; dy += yStep)
            {
                double y = y0 - dy * yScale;
                AddLine(canvas, Brushes.LightGray, 0, y, width, y);
                AddText(canvas, string.Format("{0:0.###}", dy), x0 + 2, y - 2);
            }
            for (double dy = -yStep; dy > yMin; dy -= yStep)
            {
                double y = y0 - dy * yScale;
                AddLine(canvas, Brushes.LightGray, 0, y, width, y);
                AddText(canvas, string.Format("{0:0.###}", dy), x0 + 2, y - 2);
            }

            // Оси
            AddLine(canvas, Brushes.Black, x0, 0, x0, height);
            AddLine(canvas, Brushes.Black, 0, y0, width, y0);
            AddText(canvas, "0", x0 + 2, y0 + 2);
            AddText(canvas, "X", width - 10, y0 - 14);
            AddText(canvas, "Y", x0 - 10, 2);
        }
        public static void AddLine(Canvas canvas, SolidColorBrush color, double x1, double y1, double x2, double y2)
        {
            canvas.Children.Add(new Line() { X1 = x1, X2 = x2, Y1 = y1, Y2 = y2, Stroke = color });
        }
        private static void AddText(Canvas canvas, string text, double x, double y)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = Brushes.Black;
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            canvas.Children.Add(textBlock);
        }

        public static void DrawFunction(Canvas canvas, SolidColorBrush color,  MyFunc F)
        {
            Polyline polyline = new Polyline()
            {
                Stroke = color,
                ClipToBounds = true
            };
            
            for (double x = 0; x < width; ++x)
            {
                double dy = F((x - x0) / xScale);
                if (double.IsNaN(dy) || double.IsInfinity(dy))
                    continue;
                polyline.Points.Add(new System.Windows.Point(x, y0 - dy * yScale));
            }
            canvas.Children.Add(polyline);
        }

        public static void DrawPoints(Canvas canvas, SolidColorBrush color, List<Point> points)
        {
            Polyline polyline = new Polyline()
            {
                Stroke = color,
                ClipToBounds = true
            };

            for (int i = 0; i < points.Count; i++)
            {
                double dy = points[i].Y / xScale;
                if (double.IsNaN(dy) || double.IsInfinity(dy))
                    continue;
                polyline.Points.Add(new Point(x0 + points[i].X * xScale, y0 - points[i].Y * yScale));
            }
            canvas.Children.Add(polyline);
        }

        public static void DrawPoint(Canvas canvas, SolidColorBrush color, int size, double x, double y)
        {
            Ellipse ellipse = new Ellipse()
            {
                Stroke = color,
                Width = size,
                Height = size,
            };

            canvas.Children.Add(ellipse);
            Canvas.SetTop(ellipse, y0 - y * yScale - size / 2);
            Canvas.SetLeft(ellipse, x0 + x * xScale - size / 2);
        }

        public static void DrawLine(Canvas canvas, SolidColorBrush color, double x1, double y1, double x2, double y2)
        {
            double dy1 = y1 / xScale;
            double dy2 = y2 / xScale;
            canvas.Children.Add(new Line() { X1 = x1, X2 = x2, Y1 = y1 * yScale, Y2 = y2 * yScale, Stroke = color });
        }

    }
}
