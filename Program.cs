using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RCT3Pal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            string programDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string configFilePath = programDirectory + "\\RCT3Pal_Config.xml";

            bool dontShowWarning = false;
            if (File.Exists(configFilePath))
            {
                ConfigFile.Open(configFilePath,
                    out string exe,
                    out string opt,
                    out string sav,
                    out dontShowWarning);
            }

            if (!dontShowWarning)
            {
                if (MessageBox.Show(
                    "This program is to designed to manipulate the files and folders inside the directories " +
                    "used by Roller Coaster Tycoon 3. " + Environment.NewLine +
                    Environment.NewLine +
                    "Keep in mind that this program is not perfect and may have bugs and issues, and Zach Combs " +
                    "will NOT be held responsible for any files or folders that are accidentally deleted during " +
                    "use of this program." + Environment.NewLine +
                    Environment.NewLine +
                    "Do you wish to continue?",
                    "WARNING",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning)
                    == DialogResult.No)
                {
                    return;
                }
            }

            Application.Run(new Form_Main(configFilePath, programDirectory));
        }
    }
}
