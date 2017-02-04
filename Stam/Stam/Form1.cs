using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Graphics g = this.CreateGraphics();
            Pen selPen = new Pen(Color.Blue);
            g.DrawRectangle(selPen, 10, 10, 50, 50);
            g.Dispose();

            this.ShowDialog();
        }
    }
}
