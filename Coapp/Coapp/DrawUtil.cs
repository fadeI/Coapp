using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coapp
{
    class DrawUtil
    {

        static int WIDTH = 30;
        public static void DrawFromGraph(System.Drawing.Rectangle clientRectangle, Graph graph, Graphics graphics)
        {
            var pen = new Pen(Brushes.Black);
            DrawNodes(graph, pen, graphics);

        }


        public static void DrawNodes(Graph graph, Pen pen, Graphics graphics)
        {
            foreach (Node n in graph.Nodes)
            {
                DrawNode(n, pen, graphics);
            }

        }

        public static RectangleF getHeaderRectangle(Node node)
        {
            Microsoft.Msagl.Core.Geometry.Point p = node.GeometryNode.Center;
            float w = (float)node.Width;
            float h = (float)node.Height;
            return new RectangleF((float)(p.X - w / 2), (float)(p.Y - h / 2), w, WIDTH);
        }
        public static void DrawNode(Node n, Pen pen, Graphics graphics)
        {
            Brush selPen1 = new SolidBrush(System.Drawing.Color.Blue);
            Microsoft.Msagl.Core.Geometry.Point p = n.GeometryNode.Center;
            Microsoft.Msagl.Core.Geometry.Rectangle rec = n.BoundingBox;
            double w = n.Width;
            double h = n.Height;
            ICurve curve = n.GeometryNode.BoundaryCurve;
            graphics.DrawLine(pen, (float)(p.X - w / 2), (float)(p.Y - h / 2 + WIDTH),
               (float)(p.X + w / 2), (float)(p.Y - h / 2 + WIDTH));
            graphics.FillRectangle(selPen1, getHeaderRectangle(n));

        }

        internal static GraphicsPath CreateGraphicsPath(ICurve iCurve)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            if (iCurve == null)
                return null;

            var ls = iCurve as LineSegment;
            if (ls != null)
                graphicsPath.AddLine(PointF(ls.Start), PointF(ls.End));


            return graphicsPath;
        }

        static PointF PointF(Microsoft.Msagl.Core.Geometry.Point point)
        {
            return new PointF((float)point.X, (float)point.Y);
        }

        public static float EllipseSweepAngle(Ellipse el)
        {
            return (float)((el.ParEnd - el.ParStart) / Math.PI * 180);
        }

        public static float EllipseStartAngle(Ellipse el)
        {
            return (float)(el.ParStart / Math.PI * 180);
        }


        public static void AddNodes(string p, Graph graph, double w, double h)
        {

            graph.AddNode(new Node(p));

        }
       
        


    }
}
