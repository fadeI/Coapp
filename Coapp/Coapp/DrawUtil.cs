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
        public static void DrawFromGraph(System.Drawing.Rectangle clientRectangle, Graph graph, Graphics graphics)
        {
            SetGraphTransform(graph, clientRectangle, graphics);

            var pen = new Pen(Brushes.Black);
            DrawNodes(graph, pen, graphics);
            //     DrawEdges(geometryGraph, pen, graphics);
        }


        public static void DrawNodes(Graph graph, Pen pen, Graphics graphics)
        {
           foreach (Node n in graph.Nodes)
            {
                DrawNode(n, pen, graphics);
            }
           //     DrawNode(n, pen, graphics);
        }

        public static RectangleF getHeaderRectangle(Node node)
        {
            Microsoft.Msagl.Core.Geometry.Point p = node.GeometryNode.Center;
            float w = (float)node.Width;
            float h = (float)node.Height;
            return new RectangleF((float)(p.X - w / 2), (float)(p.Y - h / 2), w, 50);
        }
        public static void DrawNode(Node n, Pen pen, Graphics graphics)
        {
            Brush selPen1 = new SolidBrush(System.Drawing.Color.Blue);
            Microsoft.Msagl.Core.Geometry.Point p = n.GeometryNode.Center;
            Microsoft.Msagl.Core.Geometry.Rectangle rec = n.BoundingBox;
            double w = n.Width;
            double h = n.Height;
            ICurve curve = n.GeometryNode.BoundaryCurve;
            graphics.DrawPath(pen, CreateGraphicsPath(curve));
            graphics.DrawLine(pen, (float)(p.X - w / 2), (float)(p.Y - h / 2 + 30),
               (float)(p.X + w / 2), (float)(p.Y - h / 2 + 30));
            graphics.FillRectangle(selPen1, getHeaderRectangle(n));

        }




        internal static GraphicsPath CreateGraphicsPath(ICurve iCurve)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            if (iCurve == null)
                return null;
           
            Curve c = iCurve as Curve;
            if (c != null)
            {
                foreach (ICurve seg in c.Segments)
                {
                    LineSegment ls = seg as LineSegment;
                    if (ls != null)
                        graphicsPath.AddLine(PointF(ls.Start), PointF(ls.End));
                }
            }
            else
            {
                var ls = iCurve as LineSegment;
                if (ls != null)
                    graphicsPath.AddLine(PointF(ls.Start), PointF(ls.End));
            }

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
        public static void SetGraphTransform(Graph geometryGraph, System.Drawing.Rectangle rectangle, Graphics graphics)
        {
            RectangleF clientRectangle = rectangle;
            var gr = geometryGraph.BoundingBox;
            if (clientRectangle.Height > 1 && clientRectangle.Width > 1)
            {
                var scale = Math.Min(clientRectangle.Width * 0.9 / gr.Width, clientRectangle.Height * 0.9 / gr.Height);
                var g0 = (gr.Left + gr.Right) / 2;
                var g1 = (gr.Top + gr.Bottom) / 2;

                var c0 = (clientRectangle.Left + clientRectangle.Right) / 2;
                var c1 = (clientRectangle.Top + clientRectangle.Bottom) / 2;
                var dx = c0 - scale * g0;
                var dy = c1 - scale * g1;
                /*
                //instead of setting transormation for graphics it is possible to transform the geometry graph, just to test that GeometryGraph.Transform() works
            
                var planeTransformation=new PlaneTransformation(scale,0,dx, 0, scale, dy); 
                geometryGraph.Transform(planeTransformation);
                */
                graphics.Transform = new Matrix((float)scale, 0, 0, (float)scale, (float)dx, (float)dy);
            }
        }


    }
}
