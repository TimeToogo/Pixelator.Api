namespace MyCMD
{
    partial class shutdown_panel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(shutdown_panel));
            this.compname = new System.Windows.Forms.Label();
            this.domname = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comps = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comp_name1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // compname
            // 
            this.compname.AutoSize = true;
            this.compname.Location = new System.Drawing.Point(9, 28);
            this.compname.Name = "compname";
            this.compname.Size = new System.Drawing.Size(66, 13);
            this.compname.TabIndex = 0;
            this.compname.Text = "Host Name: ";
            // 
            // domname
            // 
            this.domname.AutoSize = true;
            this.domname.Location = new System.Drawing.Point(9, 47);
            this.domname.Name = "domname";
            this.domname.Size = new System.Drawing.Size(80, 13);
            this.domname.TabIndex = 1;
            this.domname.Text = "Domain Name: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Computers: ";
            // 
            // comps
            // 
            this.comps.FormattingEnabled = true;
            this.comps.Location = new System.Drawing.Point(12, 96);
            this.comps.Name = "comps";
            this.comps.Size = new System.Drawing.Size(260, 173);
            this.comps.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.Location = new System.Drawing.Point(12, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "Shutdown";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comp_name1
            // 
            this.comp_name1.AutoSize = true;
            this.comp_name1.Location = new System.Drawing.Point(9, 9);
            this.comp_name1.Name = "comp_name1";
            this.comp_name1.Size = new System.Drawing.Size(89, 13);
            this.comp_name1.TabIndex = 7;
            this.comp_name1.Text = "Computer Name: ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(217, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 20);
            this.button2.TabIndex = 8;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // shutdown_panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 321);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comp_name1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comps);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.domname);
            this.Controls.Add(this.compname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "shutdown_panel";
            this.Text = "Shutdown Panel";
            this.Load += new System.EventHandler(this.shutdown_panel_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.shutdown_panel_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label compname;
        private System.Windows.Forms.Label domname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox comps;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label comp_name1;
        private System.Windows.Forms.Button button2;
    }
}