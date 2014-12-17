using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class invis_label : UserControl
    {
        private string value1 = "";
        public invis_label()
        {
            InitializeComponent();
        
           // SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
        private void back()
        {

        }
        public string val
        {
            get { return value1; }
            set{value1 = value;}
        }

        protected override void OnPaint(PaintEventArgs e)
        { 
            System.Drawing.SolidBrush brush = new SolidBrush(this.ForeColor);
            
            e.Graphics.DrawString(value1, this.Font, brush, e.ClipRectangle.Location);
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
           
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
    }
}
