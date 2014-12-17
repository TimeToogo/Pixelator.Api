using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WIDA.Forms
{
    public partial class optionsForm : Form
    {
        public Conf Conf = null;
        public optionsForm(Conf Conf)
        {
            this.Conf = Conf;
            InitializeComponent();
        }

        private void optionsForm_Load(object sender, EventArgs e)
        {
            runOnStartUpCheckBox.Checked = Conf.RunOnStartup;
            minimizeOnStartUpCheckBox.Checked = Conf.MinimizeOnStartup;
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void optionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Conf.RunOnStartup = runOnStartUpCheckBox.Checked;
            Properties.Settings.Default.RunOnStartup = Conf.RunOnStartup;
            Conf.MinimizeOnStartup = minimizeOnStartUpCheckBox.Checked;
            Properties.Settings.Default.MinimizeOnStartup = Conf.MinimizeOnStartup;
        }
    }
}
