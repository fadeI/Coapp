using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Point = Microsoft.Msagl.Core.Geometry.Point;
using Rectangle = Microsoft.Msagl.Core.Geometry.Rectangle;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Drawing;

namespace Coapp
{
    public partial class Form1 : Form
    {
       // GeometryGraph _geometryGraph;
       Graph graph = new Graph();
        public Form1()
        {
            InitializeComponent();
            SizeChanged += Form1_SizeChanged;
            // the magic calls for invoking doublebuffering
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        void Form1_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            //base.OnPaint(e);
            //if (_geometryGraph == null)
            //{
            //    _geometryGraph = CreateAndLayoutGraph();
            //}
            //DrawingUtils.DrawFromGraph(ClientRectangle, _geometryGraph, e.Graphics);


            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            base.OnPaint(e);
            if (graph.NodeCount == 0)
            {
                graph = CreateAndLayoutGraph();
            }
            DrawUtil.DrawFromGraph(ClientRectangle, graph, e.Graphics);
        }


        public Graph CreateAndLayoutGraph()
        {



            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Graph graph = new Graph();
            Microsoft.Msagl.Drawing.Node a = new Microsoft.Msagl.Drawing.Node("ddddddd");

            graph.AddNode(a);



            List<string> tablenames = new List<string>
            {
                "Person\nddsadasda\ndsadasdas" , "PersonDetails"
            };

            foreach (var p in tablenames)
            {
                DrawUtil.AddNodes(p, graph, 120, 200);
            }

            MessageBox.Show(graph.NodeCount.ToString());
            viewer.Graph = graph;
            //associate the viewer with the form
            SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(viewer);
            ResumeLayout();
            return graph;

            //GeometryGraph graph = new GeometryGraph();
            //double width = 120;
            //double height = 200;

            //foreach (string id in "c d e".Split(' '))
            //{
            //    DrawingUtils.AddNode(id, graph, width, height);
            //}

            //var settings = new SugiyamaLayoutSettings
            //{
            //    Transformation = PlaneTransformation.Rotation(Math.PI / 2),
            //    EdgeRoutingSettings = { EdgeRoutingMode = EdgeRoutingMode.Spline }
            //};
            //var layout = new LayeredLayout(graph, settings);
            //layout.Run();
            //return graph;


        }


 


        //public static void SetGraphTransform(GeometryGraph geometryGraph, System.Drawing.Rectangle rectangle, Graphics graphics)
        //{
        //    RectangleF clientRectangle = rectangle;
        //    var gr = geometryGraph.BoundingBox;
        //    if (clientRectangle.Height > 1 && clientRectangle.Width > 1)
        //    {
        //        var scale = Math.Min(clientRectangle.Width * 0.9 / gr.Width, clientRectangle.Height * 0.9 / gr.Height);
        //        var g0 = (gr.Left + gr.Right) / 2;
        //        var g1 = (gr.Top + gr.Bottom) / 2;

        //        var c0 = (clientRectangle.Left + clientRectangle.Right) / 2;
        //        var c1 = (clientRectangle.Top + clientRectangle.Bottom) / 2;
        //        var dx = c0 - scale * g0;
        //        var dy = c1 - scale * g1;
        //        /*
        //        //instead of setting transormation for graphics it is possible to transform the geometry graph, just to test that GeometryGraph.Transform() works
            
        //        var planeTransformation=new PlaneTransformation(scale,0,dx, 0, scale, dy); 
        //        geometryGraph.Transform(planeTransformation);
        //        */
        //        graphics.Transform = new Matrix((float)scale, 0, 0, (float)scale, (float)dx, (float)dy);
        //    }
        //}

   

 
        
        public void initGraph()
        {
           
          
        }
        public void CustomizedNode()
        {
           


        }

    }
}
