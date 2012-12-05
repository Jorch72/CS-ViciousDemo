using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ViciousDemo
{
    public partial class Vicious : Form
    {
        private Bitmap backBuffer;
        public Vicious()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            backBuffer = ViciousDemo.Program.Draw();
            ClientSize = new Size(backBuffer.Width, backBuffer.Height);
            Draw();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (backBuffer == null)
            {
                base.OnPaint(e);
                return;
            }
            e.Graphics.DrawImage(backBuffer, ClientRectangle);
            

        }
        public void Draw()
        {
            backBuffer = ViciousDemo.Program.Draw();
            this.Refresh();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            ViciousDemo.Program.Update();
            this.Draw();
        }

        private void Vicious_Load(object sender, EventArgs e)
        {

        }
    }
}
