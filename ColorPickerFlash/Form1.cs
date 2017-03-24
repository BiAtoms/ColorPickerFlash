using ColorPickerFlash.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorPickerFlash
{
    public partial class MainForm : Form
    {
        private TextBox text;
        private ColorShower colorShower;

        private Color newColor;
        private Color previousColor;

        private Bitmap screenCapture;
        private bool mouseIsDown;

        public MainForm()
        {
            InitializeComponent();
            this.BackColor = Color.Red;
            this.TransparencyKey = Color.Red;
            previousColor = Color.FromArgb(123, 123, 123);
            WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            #region MyRegion
            //Opacity = 0.1;
            /*
             var myScreen = Screen.FromControl(this);
             var otherScreen = Screen.AllScreens.FirstOrDefault(s => !s.Equals(myScreen))
                            ?? myScreen;
             this.Left = otherScreen.WorkingArea.Left + 120;
             this.Top = otherScreen.WorkingArea.Top + 120;
             //  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            //
             */
            #endregion

            text = new TextBox();
            text.Font = new Font("Arial", 13);

            this.Cursor = Cursors.Cross;

            this.KeyPress += OnKeyPress;
            this.MouseMove += OnMouseMove;
            this.MouseDown += onMouseDown;
            this.MouseUp += onMouseUp;

            colorShower = new ColorShower();
            this.Controls.Add(text);
            this.Controls.Add(colorShower);
        }

        private void onMouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
            colorShower.Visible = false;
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            mouseIsDown = true;
            colorShower.Visible = true;
            CaptureScreen();
            colorShower.UpperSemiCircleColor = GetCurrentColor();
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void OnClickOnForm1(object sender, EventArgs e)
        {
            #region MyRegion
            // PrintScreen block

            //  var a = Screen.AllScreens;

            // this.Controls.Remove(text);
            //  this.Controls.Remove(pictureBox);

            // upSidePictureBox.Location = new Point(MousePosition.X - 90, MousePosition.Y - 90);
            //  downSidePictureBox.Location = new Point(MousePosition.X - 90, MousePosition.Y);

            // pictureBox.BackColor = Color.Red;
            // text.TextAlign = TabAlignment.
            //pictureBox.BackColor = Color.Transparent;
            //Color wcolor = printscreen.GetPixel(x, y);
            //this.BackgroundImage = printscreen; 
            #endregion

            newColor = GetCurrentColor();

            text.Text = newColor.Name.Substring(2);
            text.Font = new Font("Arial", 13);
            text.BackColor = newColor;
            text.Location = new Point(MousePosition.X + 20, MousePosition.Y - 20);
        }



        private void OnMouseMove(object sender, EventArgs e)
        {
            if (!mouseIsDown) return;
            int x, y;

            x = MousePosition.X;
            y = MousePosition.Y;

            newColor = GetCurrentColor();

            text.Text = newColor.Name.Substring(2);
            text.BackColor = newColor;
            text.Location = new Point(x + 20, y - 20);
            colorShower.Center = MousePosition;
            colorShower.LowerSemiCircleColor = newColor;
            colorShower.Invalidate(); //repaint
        }

        private void CaptureScreen()
        {
            if (screenCapture == null)
            {
                screenCapture = new Bitmap(Size.Width, Size.Height);
            }
            using (Graphics graphics = Graphics.FromImage(screenCapture))
            {
                graphics.CopyFromScreen(0, 0, 0, 0, screenCapture.Size);
            }
        }

        private Color GetCurrentColor()
        {
            int x = MousePosition.X;
            int y = MousePosition.Y;

            return screenCapture.GetPixel(x, y);
        }
    }
}
