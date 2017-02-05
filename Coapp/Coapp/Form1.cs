using System;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using REDGraphData;
using Microsoft.Msagl.GraphViewerGdi;
using System.Text;
using System.Drawing.Drawing2D;

namespace Coapp
{
    public partial class Form1 : Form
    {
        GraphData data = new GraphData();
        Graph graph = new Graph();
        GViewer gViewer = new GViewer();
        readonly ToolTip toolTip1 = new ToolTip();
        object selectedObject;

        public Form1()
        {
            InitializeComponent();
            gViewer.ObjectUnderMouseCursorChanged += new EventHandler<ObjectUnderMouseCursorChangedEventArgs>
                (gViewer_ObjectUnderMouseCursorChanged);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            base.OnPaint(e);
            if (graph.NodeCount ==0)
            {
                createGraph();
            }
            DrawUtil.DrawFromGraph(ClientRectangle, graph, e.Graphics);
        }
            

            void gViewer_ObjectUnderMouseCursorChanged(object sender,
            ObjectUnderMouseCursorChangedEventArgs e)
        {
            if (e.OldObject != null)
            {
                selectedObject = e.OldObject.DrawingObject;
            }
            else
            {
                selectedObject = null;
            }
            selectedObject = gViewer.SelectedObject;
            Edge edge = selectedObject as Edge;
            if (edge != null)
            {
                this.gViewer.SetToolTip(toolTip1, String.Format(edge.LabelText));
            }


            gViewer.Invalidate();
        }


        private void AddNodes()
        {
            foreach (var table in data.AddTables())
            {
                Node node = new Node(table.Name);
                node.LabelText = getTableData(table);
                graph.AddNode(node);

            }
        }
        private void toWayoneToMany(string tableA, string tableB, string relation)
        {
            Edge e = (Edge)graph.AddEdge(tableA, tableB);
            e.Attr.ArrowheadAtSource = ArrowStyle.None;
            e.Attr.ArrowheadAtTarget = ArrowStyle.Normal;
            e.LabelText = relation;

        }

        private void AddEdges()
        {
            foreach (var rel in data.AddVerticies())
            {

                if (rel.RelationType.Equals(RelationType.ManyToMany))
                {
                    Edge e = (Edge)graph.AddEdge(rel.TableA.Name, rel.RelationType.ToString(), rel.TableB.Name);
                    e.Attr.ArrowheadAtTarget = ArrowStyle.None;
                    e.Attr.ArrowheadAtSource = ArrowStyle.None;
                    e.LabelText = rel.RelationType.ToString();
                }
                else if (rel.RelationType.Equals(RelationType.OneToMany))
                {
                    toWayoneToMany(rel.TableA.Name, rel.TableB.Name, rel.RelationType.ToString());
                }
                else if (rel.RelationType.Equals(RelationType.ManyToOne))
                {
                    toWayoneToMany(rel.TableB.Name, rel.TableA.Name, rel.RelationType.ToString());
                }
                else if (rel.RelationType.Equals(RelationType.OneToOne))
                {

                    Edge e = (Edge)graph.AddEdge(rel.TableA.Name, rel.TableB.Name);
                    e.Attr.ArrowheadAtSource = ArrowStyle.Normal;
                    e.Attr.ArrowheadAtTarget = ArrowStyle.Normal;
                    e.LabelText = rel.RelationType.ToString();
                }
            }

        }

        private string getTableData(Table table)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(table.Name);
            foreach (var col in table.Columns)
            {
                sb.AppendLine(col.Name);
            }
            return sb.ToString();

        }

        private void createGraph()
        {
            AddNodes();
            AddEdges();
            gViewer.Graph = graph;
            SuspendLayout();
            gViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(gViewer);
            ResumeLayout();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            createGraph();

        }
    }
}
