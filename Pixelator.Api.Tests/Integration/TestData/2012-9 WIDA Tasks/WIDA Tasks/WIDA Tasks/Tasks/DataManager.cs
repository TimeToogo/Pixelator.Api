using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WIDA.Storage;
using System.Xml;
using System.IO;
using WIDA.Utes;
using SevenZip;

namespace WIDA.Tasks
{
    public class DataManager
    {
        public Definitions Definitions = new Definitions();
        public Storage.Tasks Tasks = new Storage.Tasks();
        public Timer AutoSave = new Timer();
        public int AutoSaveFrequency
        {
            get { return AutoSave.Interval; }
            set { AutoSave.Interval = value; }
        }
        private bool Exporting = false;
        private System.Threading.Thread ExportingThread;

        public DataManager()
        {
            AutoSaveFrequency = Conf.AutoSaveInterval;
            AutoSave.Tick += new EventHandler(AutoSave_Tick);
        }

        //Imports default files
        public void Load()
        {
            bool RequiresSave = false;
            //Import definitions
            if (Directory.Exists(Conf.DefinitionsFolder))
            {
                //Try import default file
                try
                {
                    string[] DefinitionFiles = Directory.GetFiles(Conf.DefinitionsFolder, Conf.DefinitionsFileName + "." + Conf.DefintionsExtension + "." + Conf.Extension, SearchOption.TopDirectoryOnly);
                    DefinitionFiles.ToList().ForEach(DefinitionFile => this.ImportDefinitions(DefinitionFile));
                }
                catch //If invalid import backup
                {
                     string BackupDefinitionsFile = Conf.BackupFolder + Conf.DefinitionsFileName + "." + Conf.DefintionsExtension + "." + Conf.Extension;
                     if (System.IO.File.Exists(BackupDefinitionsFile))
                     {
                         this.ImportDefinitions(BackupDefinitionsFile);
                         RequiresSave = true;
                     }
                }
            }
            else
                Directory.CreateDirectory(Conf.DefinitionsFolder);

            //Import tasks
            if(Directory.Exists(Conf.TasksFolder))
            {
                //Try import default file
                try
                {
                    string[] TaskFiles = Directory.GetFiles(Conf.TasksFolder, Conf.TasksFileName + "." + Conf.TasksExtension + "." + Conf.Extension, SearchOption.TopDirectoryOnly);
                    TaskFiles.ToList().ForEach(TaskFile => this.ImportTasks(TaskFile));
                }
                catch //If invalid import backup
                {
                    string BackupTasksFile = Conf.BackupFolder + Conf.TasksFileName + "." + Conf.TasksExtension + "." + Conf.Extension;
                    if (System.IO.File.Exists(BackupTasksFile))
                    {
                        this.ImportTasks(BackupTasksFile);
                        RequiresSave = true;
                    }
                }
            }
            else
                Directory.CreateDirectory(Conf.TasksFolder);

            //Import all in import folder
            if (Directory.Exists(Conf.AutoImportFolder))
            {
                if(!Directory.Exists(Conf.DoneImportFolder))
                    Directory.CreateDirectory(Conf.DoneImportFolder);
                string[] ImportFiles = Directory.GetFiles(Conf.AutoImportFolder, "*." + Conf.Extension, SearchOption.TopDirectoryOnly);
                //Import file and move to done import folder
                ImportFiles.ToList().ForEach(ImportFile => { this.ImportFile(ImportFile); System.IO.File.Move(ImportFile, Conf.DoneImportFolder + new FileInfo(ImportFile).Name); });
                RequiresSave = true;
            }
            else
                Directory.CreateDirectory(Conf.AutoImportFolder);

            if (RequiresSave)
                this.Save();
        }

        private void AutoSave_Tick(object sender, EventArgs e)
        {
            this.Save();
        }

        public void Save(bool ForceWait = false)
        {
            if (!Exporting)
            {
                //Save data on separate thread as it can be very expensive affecting the gui
                ExportingThread = new System.Threading.Thread(() =>
                {
                    this.Backup();
                    Exporting = true;
                    try
                    {
                        ExportDefinitions(Conf.DefinitionsFolder, Conf.DefinitionsFileName, true);
                        ExportTasks(Conf.TasksFolder, Conf.TasksFileName, true);
                    }
                    catch
                    { }
                    Exporting = false;
                });
                ExportingThread.Start();
            }
            if (ForceWait && ExportingThread.ThreadState == System.Threading.ThreadState.Running)
                ExportingThread.Join();
        }

        //Duplicates the saved files in a separate folder
        private void Backup()
        {
            //Create backup folder if it does not exist
            if (!Directory.Exists(Conf.BackupFolder))
                Directory.CreateDirectory(Conf.BackupFolder);

            
            string DefaultDefinitionsFile = Path.Combine(Conf.DefinitionsFolder, Conf.DefinitionsFileName + "." + Conf.DefintionsExtension + "." + Conf.Extension);            
            //Check if file length is greater than zero
            if (System.IO.File.Exists(DefaultDefinitionsFile))
                if (new FileInfo(DefaultDefinitionsFile).Length > 0)
                {
                    //Delete file if already exists
                    string NewDefinitionsFile = Conf.BackupFolder + Conf.DefinitionsFileName + "." + Conf.DefintionsExtension + "." + Conf.Extension;
                    if (System.IO.File.Exists(NewDefinitionsFile))
                        System.IO.File.Delete(NewDefinitionsFile);
                    System.IO.File.Copy(DefaultDefinitionsFile, NewDefinitionsFile);
                }

            string DefaultTasksFile = Path.Combine(Conf.TasksFolder, Conf.TasksFileName + "." + Conf.TasksExtension + "." + Conf.Extension);
            //Check if file length is greater than zero
            if (System.IO.File.Exists(DefaultTasksFile))
                if (new FileInfo(DefaultTasksFile).Length > 0)
                {
                    //Delete file if already exists
                    string NewTasksFile = Conf.BackupFolder + Conf.TasksFileName + "." + Conf.TasksExtension + "." + Conf.Extension;
                    if (System.IO.File.Exists(NewTasksFile))
                        System.IO.File.Delete(NewTasksFile);
                    //Copy default tasks file
                    System.IO.File.Copy(DefaultTasksFile, NewTasksFile);
                }
        }

        public void ExportDefinitions(string Path, string FileName, bool Overwrite = false)
        {
            string Output = Utilities.getNonConflictingFileName(Path, FileName, Conf.DefintionsExtension + "." + Conf.Extension);
            if (Overwrite)
            {
                Output = Path + FileName + "." + Conf.DefintionsExtension + "." + Conf.Extension;
                //Empty/Create file
                System.IO.File.Create(Output).Close();
            }
            //Create if does not exist
            if (!System.IO.File.Exists(Output))
                System.IO.File.Create(Output).Close();

            this.WriteData(Output, Definitions.ToXML().OuterXml);
        }

        public void ExportTasks(string Path, string FileName, bool Overwrite = false)
        {
            string Output = Utilities.getNonConflictingFileName(Path, FileName, Conf.TasksExtension + "." + Conf.Extension);
            if (Overwrite)
            {
                Output = Path + FileName + "." + Conf.TasksExtension + "." + Conf.Extension;
                //Empty/Create file
                System.IO.File.Create(Output).Close();
            }
            //Create if does not exist
            if (!System.IO.File.Exists(Output))
                System.IO.File.Create(Output).Close();

            this.WriteData(Output, Tasks.ToXML().OuterXml);
        }

        public void ImportFile(string FilePath)
        {
            if (FilePath.EndsWith("." + Conf.DefintionsExtension + "." + Conf.Extension))
            {
                this.ImportDefinitions(FilePath);
            }
            else if (FilePath.EndsWith("." + Conf.TasksExtension + "." + Conf.Extension))
            {
                this.ImportTasks(FilePath);
            }
        }

        private void ImportDefinitions(string FilePath)
        {
            XmlDocument Doc = new XmlDocument();
            Doc.LoadXml(ReadData(FilePath));
            this.Definitions.LoadXML(Doc);
        }

        private void ImportTasks(string FilePath)
        {
            XmlDocument Doc = new XmlDocument();
            Doc.LoadXml(ReadData(FilePath));
            this.Tasks.LoadXML(Doc);
        }

        //Wrapper for writing data with compression and other standardizations
        private void WriteData(string FilePath, string Data)
        {
            using (FileStream Stream = new FileStream(FilePath, FileMode.Open))
            {
                using (BinaryWriter Writer = new BinaryWriter(Stream))
                {
                    byte[] Bytes = Encoding.UTF8.GetBytes(Data);
                    Bytes = SevenZip.Compression.LZMA.SevenZipHelper.Compress(Bytes);
                    Writer.Write(Bytes);
                    Bytes = null;
                    Data = null;
                }
            }
        }

        //Wrapper for reading data with compression and other standardizations
        private string ReadData(string FilePath)
        {
            using (FileStream Stream = new FileStream(FilePath, FileMode.Open))
            {
                using (BinaryReader Reader = new BinaryReader(Stream))
                {
                    byte[] Data = Reader.ReadBytes((int)Reader.BaseStream.Length);
                    Data = SevenZip.Compression.LZMA.SevenZipHelper.Decompress(Data);
                    return Encoding.UTF8.GetString(Data);
                }
            }
        }
    }
}
