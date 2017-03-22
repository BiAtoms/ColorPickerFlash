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
    public partial class Form1 : Form
    {
        private TextBox text;

        private Bitmap backImageUp;
        private Bitmap backImageDown;

        private PictureBox upSidePictureBox;
        private PictureBox downSidePictureBox;

        private Color color;
        private Color previousColor;

        public Form1()
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

            upSidePictureBox = new PictureBox();
            downSidePictureBox = new PictureBox();

            backImageUp = new Bitmap(Resources.UpHandSide);
            backImageDown = new Bitmap(Resources.DownHandSide);

            text = new TextBox();

            upSidePictureBox.Size = new Size(180, 90);
            upSidePictureBox.Image = backImageUp;
            upSidePictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            downSidePictureBox.Size = new Size(180, 90);
            downSidePictureBox.Image = backImageDown;
            downSidePictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            this.Cursor = Cursors.Cross;

            this.KeyPress += OnKeyPress;
            upSidePictureBox.MouseDown += OnClickOnForm1;
            downSidePictureBox.MouseDown += OnClickOnForm1;

            this.MouseMove += OnMouseMove;
            upSidePictureBox.MouseMove += OnMouseMove;
            downSidePictureBox.MouseMove += OnMouseMove;

            text.Location = new Point(MousePosition.X + 20, MousePosition.Y - 20);

            this.Controls.Add(text);
            this.Controls.Add(upSidePictureBox);
            this.Controls.Add(downSidePictureBox);


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

            color = GetCurrentColor();

            text.Text = color.Name.Substring(2);
            text.Font = new Font("Arial", 13);
            text.BackColor = color;
            text.Location = new Point(MousePosition.X + 20, MousePosition.Y - 20);

        }

        private void OnMouseMove(object sender, EventArgs e)
        {
            int x, y;

            x = MousePosition.X;
            y = MousePosition.Y;

            upSidePictureBox.Location = new Point(x - 90, y - 90);
            downSidePictureBox.Location = new Point(x - 90, y);

            //text.Location = new Point(x, y);                // This also slows down the app

            // color = GetCurrentColor();                     // This kills the app

            if (color != previousColor)
            {
                //  upSidePictureBox.BackColor = color;        // These two lines 
                //  downSidePictureBox.BackColor = color;      // behaves as a bug
                previousColor = color;
            }

            #region MyRegion
            // printscreen.Save(@"C:\Users\aslan\Desktop\Aslan\AtlTechInfo\ColorPickerFlash\ColorPickerFlash\Images\printScre.jpg", ImageFormat.Jpeg);

            // color = GetCurrentColor();
            //upSidePictureBox.BackColor = color; 
            //downSidePictureBox.BackColor = color; 
            #endregion
        }

        private Color GetCurrentColor()
        {
            int x, y;
            x = MousePosition.X;
            y = MousePosition.Y;

            Bitmap printscreen = new Bitmap(2, 2);

            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(x - 2, y - 2, 0, 0, printscreen.Size);

            return printscreen.GetPixel(1, 1);
        }

        #region MyRegion
        // Code for allowing clicking through of the form
        /*  protected override void WndProc(ref Message m)
          {
              const uint WM_NCHITTEST = 0x84;
            //graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            //Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

              const int HTTRANSPARENT = -1;
              const int HTCLIENT = 1;
              const int HTCAPTION = 2;
              // ... or define an enum with all the values

              if (m.Msg == WM_NCHITTEST)
              {
                  // If it's the message we want, handle it.
                  if (false)
                  {
                      // If we're drawing, we want to see mouse events like normal.
                      m.Result = new IntPtr(HTCLIENT);
                  }
                  else
                  {
                      // Otherwise, we want to pass mouse events on to the desktop,
                      // as if we were not even here.
                      m.Result = new IntPtr(HTTRANSPARENT);
                  }
                  return;  // bail out because we've handled the message
              }

              // Otherwise, call the base class implementation for default processing.
              base.WndProc(ref m);
          }
          */
        #endregion
    }
}
