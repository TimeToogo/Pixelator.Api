using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WIDA.Utes
{
    public static class Utilities
    {
        public static void RunOnStartup(bool Remove)
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (!Remove)
            {
                Key.SetValue(Conf.AppName, Application.ExecutablePath);
            }
            else
            {
                Key.DeleteValue(Conf.AppName, false);
            }
        }

        public static string getNonConflictingFileName(string Path, string FileName, string Extension)
        {
            int Count = 1;
            string Separator = ".";
            string FullPath = Path + FileName + Separator + Extension;
            if (!System.IO.File.Exists(FullPath))
                return FullPath;
            while (System.IO.File.Exists(FullPath))
            {
                FullPath = Path + FileName + "(" + Count.ToString() + ")" + Separator + Extension;
                Count++;
            }
            return FullPath;
        }
    }
}
