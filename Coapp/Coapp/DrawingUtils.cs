using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Point = Microsoft.Msagl.Core.Geometry.Point;
using Rectangle = Microsoft.Msagl.Core.Geometry.Rectangle;

namespace Coapp
{
    class DrawingUtils
    {

        public  static void DrawFromGraph(System.Drawing.Rectangle clientRectangle, GeometryGraph geometryGraph, Graphics graphics)
        {
          // SetGraphTransform(geometryGraph, clientRectangle, graphics);

            var pen = new Pen(Brushes.Black);
            DrawNodes(geometryGraph, pen, graphics);
            //     DrawEdges(geometryGraph, pen, graphics);
        }


        public  static void DrawNodes(GeometryGraph geometryGraph, Pen pen, Graphics graphics)
        {
            foreach (Node n in geometryGraph.Nodes)
                DrawNode(n, pen, graphics);
        }

        public static RectangleF getHeaderRectangle(Node node )
        {
            Point p = node.Center;
            float w =(float) node.Width;
            float h = (float)node.Height;
            return new RectangleF((float)(p.X-w/2),(float)(p.Y - h / 2),w,50);
        }
        public static void DrawNode(Node n, Pen pen, Graphics graphics)
        {
            Brush selPen1 = new SolidBrush(Color.Blue);
            Point p = n.Center;
            Rectangle rec=n.BoundingBox;
            double w = n.Width;
            double h = n.Height;
            ICurve curve = n.BoundaryCurve;
            graphics.DrawPath(pen, CreateGraphicsPath(curve));
            graphics.DrawLine(pen, (float)(p.X - w / 2), (float)(p.Y - h / 2 + 30),
               (float)(p.X + w / 2), (float)(p.Y - h / 2 + 30));
            graphics.FillRectangle(selPen1, getHeaderRectangle(n));

        }


        public  static void AddNodes(string p, GeometryGraph graph, double w, double h)
        {
            Node temp = new Node(CreateCurve(w, h), p);        
            graph.Nodes.Add(temp);
    
        }

        public static ICurve CreateCurve(double w, double h)
        {

            return CurveFactory.CreateRectangle(w, h, new Point());
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

        static PointF PointF(Point point)
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
        public static void SetGraphTransform(GeometryGraph geometryGraph, System.Drawing.Rectangle rectangle, Graphics graphics)
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
        public static void AddNode(string id, GeometryGraph graph, double w, double h)
        {


            graph.Nodes.Add(new Node(CreateCurve(w, h), id));
        }

  

    }
}
