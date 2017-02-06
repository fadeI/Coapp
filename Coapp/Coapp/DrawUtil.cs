using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2 = Microsoft.Msagl.Core.Geometry.Point;
namespace Coapp
{
    class DrawUtil
    {

        static int WIDTH = 30;
        public static void DrawFromGraph( Graph graph, Graphics graphics)
        {
            SetGraphTransform(graph, graphics);
            var pen = new Pen(Brushes.Black);
            DrawNodes(graph, pen, graphics);

        }
        static internal PointF PointF(P2 p) { return new PointF((float)p.X, (float)p.Y); }
        static System.Drawing.Drawing2D.GraphicsPath FillTheGraphicsPath(ICurve iCurve)
        {
            var curve = ((RoundedRect)iCurve).Curve;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            foreach (ICurve seg in curve.Segments)
                AddSegmentToPath(seg, ref path);
            return path;
        }

        private static void AddSegmentToPath(ICurve seg, ref System.Drawing.Drawing2D.GraphicsPath p)
        {
            const float radiansToDegrees = (float)(180.0 / Math.PI);
            LineSegment line = seg as LineSegment;
            if (line != null)
                p.AddLine(PointF(line.Start), PointF(line.End));
            else
            {
                CubicBezierSegment cb = seg as CubicBezierSegment;
                if (cb != null)
                    p.AddBezier(PointF(cb.B(0)), PointF(cb.B(1)), PointF(cb.B(2)), PointF(cb.B(3)));
                else
                {
                    Ellipse ellipse = seg as Ellipse;
                    if (ellipse != null)
                        p.AddArc((float)(ellipse.Center.X - ellipse.AxisA.Length), (float)(ellipse.Center.Y - ellipse.AxisB.Length),
                            (float)(2 * ellipse.AxisA.Length), (float)(2 * ellipse.AxisB.Length), (float)(ellipse.ParStart * radiansToDegrees),
                            (float)((ellipse.ParEnd - ellipse.ParStart) * radiansToDegrees));

                }
            }
        }

        
        public static void SetGraphTransform(Graph graph, Graphics graphics)
        {
            var pen = new Pen(Brushes.Black);
            using (System.Drawing.Drawing2D.Matrix m = graphics.Transform)
            {
                using (System.Drawing.Drawing2D.Matrix saveM = m.Clone())
                {
                    foreach (Microsoft.Msagl.Drawing.Node node in graph.Nodes)
                    {
                        graphics.SetClip(FillTheGraphicsPath(node.GeometryNode.BoundaryCurve));
                        using (var m2 = new System.Drawing.Drawing2D.Matrix(1, 0, 0, -1, 0, 2 * (float)node.GeometryNode.Center.Y))
                            m.Multiply(m2);
                        graphics.DrawLine(pen, PointF( node.GeometryNode.Center),
                           PointF(new Microsoft.Msagl.Core.Geometry.Point((int) node.GeometryNode.Center.X + 20,
                            (int)node.GeometryNode.Center.Y + 20)));
                        graphics.Transform = m;
                        graphics.Transform = saveM;
                        graphics.ResetClip();
                    }
                }
            }
            /*
            //instead of setting transormation for graphics it is possible to transform the geometry graph, just to test that GeometryGraph.Transform() works

            var planeTransformation=new PlaneTransformation(scale,0,dx, 0, scale, dy); 
            geometryGraph.Transform(planeTransformation);
            */
           // graphics.Transform = new Matrix((float)scale, 0, 0, (float)scale, (float)dx, (float)dy);
            }


        public static void DrawNodes(Graph graph, Pen pen, Graphics graphics)
        {
            foreach (Microsoft.Msagl.Drawing.Node n in graph.Nodes)
            {
                DrawNode(n, pen, graphics);
            }

        }

        public static RectangleF getHeaderRectangle(Microsoft.Msagl.Drawing.Node node)
        {
            Microsoft.Msagl.Core.Geometry.Point p = node.GeometryNode.Center;
            float w = (float)node.Width;
            float h = (float)node.Height;
            return new RectangleF((float)(p.X - w / 2), (float)(p.Y - h / 2), w, WIDTH);
        }
        public static void DrawNode(Microsoft.Msagl.Drawing.Node n, Pen pen, Graphics graphics)
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

            graph.AddNode(new Microsoft.Msagl.Drawing.Node(p));

        }
       
        


    }
}
