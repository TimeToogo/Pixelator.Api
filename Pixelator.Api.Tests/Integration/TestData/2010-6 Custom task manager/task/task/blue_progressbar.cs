using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

public class ProgressBarEx : ProgressBar
{
    private LinearGradientBrush brush = null;
    public Rectangle blue = new Rectangle();
    public ProgressBarEx()
    {
        this.SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        
        brush = new LinearGradientBrush(e.ClipRectangle, this.ForeColor, this.ForeColor, 0F, false);
        Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
        if (ProgressBarRenderer.IsSupported)
            ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rec);
        rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
        rec.Height = rec.Height - 4;
        rec.Width = rec.Width + 3;
        rec.Height = rec.Height + 3;
        e.Graphics.FillRectangle(brush, 0, 0, rec.Width, rec.Height);
        blue = rec;

        Bitmap pt = new Bitmap(1, 1);
        pt.SetPixel(0, 0, WindowsFormsApplication1.Form1.DefaultBackColor);
        e.Graphics.DrawImage(pt, e.ClipRectangle.Location);
        pt.SetPixel(0, 0, WindowsFormsApplication1.Form1.DefaultBackColor);
        Point bottom = e.ClipRectangle.Location;
        bottom.Y = e.ClipRectangle.Location.Y + e.ClipRectangle.Size.Height - 1;
        e.Graphics.DrawImage(pt, bottom);
        bottom.X = e.ClipRectangle.Location.Y + e.ClipRectangle.Size.Width - 1;
        e.Graphics.DrawImage(pt, bottom);
        bottom.X = e.ClipRectangle.Location.Y + e.ClipRectangle.Size.Width - 1;
        bottom.Y = e.ClipRectangle.Location.Y + e.ClipRectangle.Size.Height - 1;
        e.Graphics.DrawImage(pt, bottom);
    }
}
