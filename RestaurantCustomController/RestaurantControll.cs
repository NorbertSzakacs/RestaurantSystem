using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantCustomController
{
    public partial class RestaurantControll : Button
    {
        private int full = 4;
        private int act = 1;
        private Color fullColor = Color.LightGreen;
        private Color actColor = Color.Red;

        public int maxValue
        {
            get { return full; }
            set { full = value; Invalidate(); }
        }

        public int actValue
        {
            get { return act; }
            set { act = value; Invalidate(); }
        }

        public event EventHandler myClicked
        {
            add { this.Click += value; }
            remove { this.Click -= value;}
        }

        public RestaurantControll()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            int height = this.Size.Height - 1;
            int width = this.Size.Width - 1;

            if (act > full)
            {
                act = full;
            }

            float ratio = (full - act) / (float)full;
            int rectFullLength = (int)((float)ratio * (float)height);

            var path = new GraphicsPath();
            var recFull = new Rectangle(0, 0, width, rectFullLength);
            var recAct = new Rectangle(0, rectFullLength, width, height - rectFullLength);

            Brush brAct = new SolidBrush(actColor);
            pe.Graphics.FillRectangle(brAct, recAct);
            path.AddRectangle(recAct);

            Brush brFull = new SolidBrush(fullColor);
            pe.Graphics.FillRectangle(brFull, recFull);
            path.AddRectangle(recFull);

            string stringText = string.Format("{0}/{1}",act,full);
            FontFamily family = new FontFamily("Arial");
            int fontStyle = (int)FontStyle.Bold;
            int emSize = 16;
            Point origin = new Point(width/3, height/2);
            
            StringFormat format = StringFormat.GenericDefault;

            path.AddString(stringText,family,fontStyle,emSize,origin,format);

            Pen myPen = new Pen(Color.Black, 1);
            pe.Graphics.DrawPath(myPen, path);
            brAct.Dispose();
            brFull.Dispose();
        }
    }
}
