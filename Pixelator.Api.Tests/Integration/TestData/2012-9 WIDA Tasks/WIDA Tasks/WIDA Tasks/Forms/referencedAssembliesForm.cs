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
    public partial class referencedAssembliesForm : Form
    {
        public List<string> ReferencedAssemblies = new List<string>();
        List<string> OriginalReferencedAssemblies = new List<string>();

        public referencedAssembliesForm(List<string> OriginalReferencedAssemblies)
        {
            this.OriginalReferencedAssemblies = OriginalReferencedAssemblies;
            InitializeComponent();
        }

        private void referencedAssembliesForm_Load(object sender, EventArgs e)
        {
            referencedAssembliesListBox.Items.AddRange(OriginalReferencedAssemblies.Cast<object>().ToArray());
        }

        private void referencedAssembliesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeButton.Enabled = (referencedAssembliesListBox.SelectedIndex != -1);
        }

        private void referencedAssembliesTextBox_TextChanged(object sender, EventArgs e)
        {
            addButton.Enabled = (referencedAssembliesTextBox.Text.Length > 0);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            referencedAssembliesListBox.Items.Add(referencedAssembliesTextBox.Text);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            referencedAssembliesListBox.Items.RemoveAt(referencedAssembliesListBox.SelectedIndex);
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void referencedAssembliesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ReferencedAssemblies = referencedAssembliesListBox.Items.Cast<string>().ToList();
        }
    }
}
