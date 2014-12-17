using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using WIDA.Storage;
using WIDA.Tasks;
using WIDA.Forms;
using WIDA.Utes;

namespace WIDA
{
    public partial class mainForm : Form
    {
        Conf Conf = new Conf();
        DataManager DataManager = new DataManager();

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            //Initialize unhandled exception handlers
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(UnhandledThreadException);

            //Load all files with DataManager class
            DataManager.Load();
            DataManager.AutoSave.Start();

            updateTaskListBox();
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception was encountered: " + ((Exception)e.ExceptionObject).Message, "Error");
        }

        private void UnhandledThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception was encountered: " + e.Exception.Message, "Error");
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            //Minimize if specified
            if (Conf.MinimizeOnStartup)
                this.HideForm();
        }

        //Display all tasks in their groups in the listboxs
        private void updateTaskListBox()
        {
            taskGroupDisplayListBox.Items.Clear();
            taskGroupDisplayListBox.Items.AddRange(this.DataManager.Tasks.GetTasksGroupNames());
            taskDisplayListBox.Items.Clear();
            taskDescriptionLabel.Text = String.Empty;
            editTaskButton.Enabled = false;
            removeTaskButton.Enabled = false;
            ActiveTaskCheckBox.Enabled = false;
        }

        //Update definitions (advanced users)
        private void defineButton_Click(object sender, EventArgs e)
        {
            definingDisplayForm Form = new definingDisplayForm(this.DataManager.Definitions);
            Form.ShowDialog();
            this.DataManager.Definitions = Form.Definitions;
            this.DataManager.Save();
        }

        //Creates a dialog to allow the user to specify a file path
        public string userChosenFilePath(string Description = "")
        {
            FolderBrowserDialog Dialog = new FolderBrowserDialog();
            Dialog.Description = Description;
            DialogResult Result = Dialog.ShowDialog();
            if (Result == System.Windows.Forms.DialogResult.Cancel)
                //return null if user cancels the dialog
                return null;
            else
                return Dialog.SelectedPath + "\\";
        }

        //Export the definitions to a user chosen path
        private void definitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.DataManager.Definitions.TotalCount() > 0)
            {
                string Path = userChosenFilePath("Please choose a path to export definitions");
                if (!string.IsNullOrEmpty(Path))
                    this.DataManager.ExportDefinitions(Path, Conf.DefinitionsFileName);
            }
            else
                MessageBox.Show("You need at least one trigger, condition or action");
        }

        //Export tasks to a user chosen path
        private void exportTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.DataManager.Tasks.TotalCount() > 0)
            {
                string Path = userChosenFilePath("Please choose a path to export definitions");
                if (!string.IsNullOrEmpty(Path))
                    this.DataManager.ExportTasks(Path, Conf.TasksFileName);
            }
            else
                MessageBox.Show("You need at least one task");
        }

        //Import other files (tasks or definitions)
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.Filter = "WIDA files (*." + Conf.Extension + ")|*." + Conf.Extension;
            Dialog.Multiselect = true;
            DialogResult Result = Dialog.ShowDialog();
            if (Result != System.Windows.Forms.DialogResult.Cancel)
            {
                foreach (string File in Dialog.FileNames)
                {
                    try
                    {
                        this.DataManager.ImportFile(File);
                    }
                    catch
                    {
                        MessageBox.Show("Could not import: " + File, "Error importing file");
                    }
                }
            }
        }

        //Create a new task
        private void createTaskButton_Click(object sender, EventArgs e)
        {
            createTaskForm Form = new createTaskForm(this.DataManager.Definitions);
            Form.ShowDialog();
            if (Form.IsFinished)
            {
                this.DataManager.Tasks.TaskList.Add(this.DataManager.Tasks.GetTaskNonConflictingName(Form.Task));
                updateTaskListBox();
            }
            this.DataManager.Save();
        }

        //Edit an existing task
        private void editTaskButton_Click(object sender, EventArgs e)
        {
            if (taskGroupDisplayListBox.SelectedIndex == -1 || taskDisplayListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task");
                return;
            }
            Task Original = this.DataManager.Tasks.GetTask(taskGroupDisplayListBox.SelectedItem.ToString(), taskDisplayListBox.SelectedItem.ToString());
            //Store if task is active
            bool TaskIsActive = Original.Active;
            //Disable task for editing
            Original.Active = false;
            createTaskForm Form = new createTaskForm(this.DataManager.Definitions, Original);
            Form.ShowDialog();
            if (Form.IsFinished)
            {
                //If not canceled overwrite the task with its new counterpart
                Original.Dispose();
                Task EditedTask = Form.Task;
                EditedTask.Active = TaskIsActive;
                this.DataManager.Tasks.TaskList[this.DataManager.Tasks.TaskList.IndexOf(Original)] = EditedTask;
            }
            else
                Original.Active = TaskIsActive;
            updateTaskListBox();
            this.DataManager.Save();
        }

        //Delete task (permentaly)
        private void removeTaskButton_Click(object sender, EventArgs e)
        {
            if (taskGroupDisplayListBox.SelectedIndex == -1 || taskDisplayListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task");
                return;
            }
            //Give the user a chance to not be stupid
            if (MessageBox.Show("Are you very sure? This is very permanent!", "Think!", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                return;
            Task Task = this.DataManager.Tasks.GetTask(taskGroupDisplayListBox.SelectedItem.ToString(), taskDisplayListBox.SelectedItem.ToString());
            Task.Dispose();
            this.DataManager.Tasks.TaskList.Remove(Task);
            updateTaskListBox();
            this.DataManager.Save();
        }

        //Enable/Disable task
        private void ActiveTaskCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (taskGroupDisplayListBox.SelectedIndex == -1 || taskDisplayListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task");
                return;
            }
            Task Task = this.DataManager.Tasks.GetTask(taskGroupDisplayListBox.SelectedItem.ToString(), taskDisplayListBox.SelectedItem.ToString());
            Task.Active = ActiveTaskCheckBox.Checked;
            this.DataManager.Save();
        }

        //Enable controls to edit task when a task is highlighted in the listbox
        private void taskDisplayListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Task Task = (taskDisplayListBox.SelectedIndex == -1) ? null : this.DataManager.Tasks.GetTask(taskGroupDisplayListBox.SelectedItem.ToString(), taskDisplayListBox.SelectedItem.ToString());
            taskDescriptionLabel.Text = (Task != null) ? Task.Description : String.Empty;
            editTaskButton.Enabled = (Task != null);
            removeTaskButton.Enabled = (Task != null);
            ActiveTaskCheckBox.Enabled = false;
            if (Task != null)
            {
                ActiveTaskCheckBox.Enabled = true;
                ActiveTaskCheckBox.Checked = Task.Active;
            }
        }

        //Display all tasks within the selected task group
        private void taskGroupDisplayListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            taskDescriptionLabel.Text = String.Empty;
            editTaskButton.Enabled = false;
            removeTaskButton.Enabled = false;
            ActiveTaskCheckBox.Enabled = false;
            taskDisplayListBox.Items.Clear();
            if (taskGroupDisplayListBox.SelectedIndex != -1)
            {
                foreach (Task Task in this.DataManager.Tasks.GetTasksFromGroupName(taskGroupDisplayListBox.SelectedItem.ToString()))
                    taskDisplayListBox.Items.Add(Task.Name);
            }
        }

        //Hide the form in the icon tray
        private void HideForm()
        {
            //Create context menu for the icon
            ContextMenu Menu = new System.Windows.Forms.ContextMenu();
            Menu.MenuItems.Add("Exit");
            Menu.MenuItems[0].Click += new EventHandler(exit_Click);
            mainNotifyIcon.Icon = this.Icon;
            mainNotifyIcon.MouseClick += new MouseEventHandler(mainNotifyIcon_Click);
            mainNotifyIcon.ContextMenu = Menu;
            this.Hide();
            mainNotifyIcon.Visible = true;
        }

        //Exit event for the notify icon context menu
        private void exit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        //Show form if icon is left clicked
        private void mainNotifyIcon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                this.ShowForm();
        }

        //Shows the form to the user
        private void ShowForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Focus();
            mainNotifyIcon.Visible = false;
        }

        //Hide form if minimized
        private void mainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.HideForm();
            }
        }

        //Shutdown application
        private void Exit()
        {
            this.DataManager.AutoSave.Stop();
            this.DataManager.Save(true);
            this.DataManager.Tasks.TaskList.ForEach(Task => Task.Dispose());
            Application.Exit();
        }

        //Main form closing event
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If it is the red X being clicked, cancel and hide
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideForm();
            }
        }

        //Display the options dialog to certain conf options
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionsForm Form = new optionsForm(Conf);
            Form.ShowDialog();
            Conf = Form.Conf;
            Utilities.RunOnStartup(!Conf.RunOnStartup);
            Properties.Settings.Default.Save();
        }

        //Exit WIDA Tasks
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Exit();
        }

        //Show about form
        private void aboutWIDATasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm Form = new AboutForm();
            Form.ShowDialog();
        }
    }
}
