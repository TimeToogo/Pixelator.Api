namespace Physics_box
{
    partial class ball
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ball));
            this.SuspendLayout();
            // 
            // ball
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(100, 100);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ball";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Physics";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ball_FormClosing);
            this.Load += new System.EventHandler(this.display_ball_Load);
            this.Click += new System.EventHandler(this.ball_Click);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ball_KeyUp);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ball_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ball_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ball_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ball_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

