using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
    public partial class Canvas : Form
    {
        // A list to store a stroke as a list of points
        List<PointWithAttributes> stroke = new List<PointWithAttributes>();
        // A list to store delete points for a redo operations
        List<PointWithAttributes> redo = new List<PointWithAttributes>();
        // A list to store a drawing as a list of strokes
        List<List<PointWithAttributes>> strokes = new List<List<PointWithAttributes>>();
        // A list to store a drawing as a list of strokes for redo operations
        List<List<PointWithAttributes>> redos = new List<List<PointWithAttributes>>();
        // A temp list to store a stroke
        List<PointWithAttributes> tempStroke = new List<PointWithAttributes>();
        // A variable to hold the color to use
        Color c = Color.Black;

        public Canvas()
        {
            InitializeComponent();
        }

        // Paint a series of points to the canvas as the mouse moves
        public void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.Button != MouseButtons.None)
            {
                PointWithAttributes pt = new PointWithAttributes();
                pt.point = e.Location;
                pt.color = c;
                stroke.Add(pt);
                Invalidate();
            }
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            //foreach (List<PointWithAttributes> stroke in strokes)
            //{
                foreach (PointWithAttributes pt in stroke)
                {
                    Brush br = new SolidBrush(pt.color);
                    e.Graphics.FillRectangle(br, pt.point.X, pt.point.Y, 10, 10);
                    e.Graphics.DrawRectangle(Pens.Blue, pt.point.X, pt.point.Y, 10, 10);
                }
            //}
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                c = Color.Red;
            }
            if (e.KeyCode == Keys.B)
            {
                c = Color.AliceBlue;
            }
            if (e.KeyCode == Keys.Z && e.Control && strokes.Count > 0) // redo
            {
                redos.Add(strokes[strokes.Count - 1]);
                strokes.RemoveAt(strokes.Count - 1);
                Invalidate();
            }
            if (e.KeyCode == Keys.Y && e.Control && strokes.Count > 0) // redo
            {
                strokes.Add(redos[redos.Count - 1]);
                redos.RemoveAt(redos.Count - 1);
                Invalidate();
            }
        }
        // clear the previous stroke in preparation for creating a new stroke
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
        // after creating a new stroke, add that stroke to a list of strokes
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {

            // tempStroke.AddRange(stroke);
            // this will insert to the front of the list
            List<PointWithAttributes> tempStroke = new List<PointWithAttributes>(stroke);
            strokes.Add(tempStroke);
            //  tempStroke.Clear(); // this statement clears strokes
        }
    }
}
