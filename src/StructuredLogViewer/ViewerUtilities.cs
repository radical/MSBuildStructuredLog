using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace StructuredLogViewer
{
    class ViewerUtilities
    {
        public static void BrowseForMSBuildExe()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MSBuild.exe|MSBuild.exe";
            openFileDialog.Title = "Select MSBuild.exe location";
            openFileDialog.CheckFileExists = true;
            var result = openFileDialog.ShowDialog();
            if (result != true)
            {
                return;
            }

            SettingsService.AddRecentMSBuildLocation(openFileDialog.FileName);
        }
    }
}
