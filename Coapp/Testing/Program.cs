using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing
{
    class Program
    {

        Form myform = new Form();
        private void initForm()
        {
            myform.Text = "Main Window";
            myform.Size = new Size(640, 400);
            myform.FormBorderStyle = FormBorderStyle.FixedDialog;
            myform.StartPosition = FormStartPosition.CenterScreen;
        }
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    Graphics g = e.Graphics;
        //    using (Pen selPen = new Pen(Color.Blue))
        //    {
        //        g.DrawRectangle(selPen, 10, 10, 50, 50);
        //    }
        //}

        private void Grap()
        {
            Graphics g = myform.CreateGraphics();
            Brush selPen = new SolidBrush(Color.Blue);
            g.FillRectangle(selPen, 10, 10, 50, 50);
            g.Dispose();

            myform.ShowDialog();


        }
        static void Main(string[] args)
        {

            Form myform = new Form();

            myform.Text = "Main Window";
            myform.Size = new Size(640, 400);
            myform.FormBorderStyle = FormBorderStyle.FixedDialog;
            myform.StartPosition = FormStartPosition.CenterScreen;

            myform.Show();    //  ->  First Show

            //  -> Then Draw

            Graphics g = myform.CreateGraphics();
            Pen selPen = new Pen(Color.Blue);
            Brush selPen1 = new SolidBrush(Color.Blue);
            g.DrawRectangle(selPen, 10, 10, 50, 50);
            g.FillRectangle(selPen1, 10, 10, 50, 20);
            g.DrawLine(selPen, new Point(10, 30), new Point(60, 30));
            g.Dispose();

            Console.ReadKey();


        }
    }
}
