using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorPickerFlash
{
    class ColorShower : Control
    {
        public Color UpperSemiCircleColor
        {
            get
            {
                return upperSemiCirlcePen.Color;
            }
            set
            {
                upperSemiCirlcePen.Color = value;
            }
        }
        public Color LowerSemiCircleColor
        {
            get
            {
                return lowerSemiCirlcePen.Color;
            }
            set
            {
                lowerSemiCirlcePen.Color = value;
            }
        }
        public Color OuterCircleColor
        {
            get
            {
                return outerCirlcePen.Color;
            }
            set
            {
                outerCirlcePen.Color = value;
            }
        }
        public Point Center
        {
            set
            {
                value.Offset(-Width / 2, -Height / 2);
                Location = value;
            }
        }

        private const int penWidth = 18;
        private Rectangle outerRect;
        private Rectangle innerRect;

        private Pen outerCirlcePen = new Pen(Color.Gray, penWidth);
        private Pen upperSemiCirlcePen = new Pen(Color.Orange, penWidth);
        private Pen lowerSemiCirlcePen = new Pen(Color.LightSkyBlue, penWidth);
        public ColorShower(int size)
        {
            Width = size;
            Height = size;

            this.DoubleBuffered = true; //reduce flicker
            outerRect = new Rectangle(penWidth / 2, penWidth / 2, size - penWidth, size - penWidth);
            innerRect = new Rectangle((int)(penWidth * 1.5), (int)(1.5 * penWidth), size - 3 * penWidth, size - penWidth * 3);

        }

        public ColorShower() : this(225)
        { }

        //TODO: override Dispose method
        ~ColorShower()
        {
            outerCirlcePen.Dispose();
            lowerSemiCirlcePen.Dispose();
            upperSemiCirlcePen.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            //TODO: enable SmoothingMode.AntiAlias
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(outerCirlcePen, outerRect);
            e.Graphics.DrawArc(upperSemiCirlcePen, innerRect, 0, 180);
            e.Graphics.DrawArc(lowerSemiCirlcePen, innerRect, 180, 180);
        }
    }
}
