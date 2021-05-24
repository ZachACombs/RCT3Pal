using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RCT3Pal
{
    public partial class Form_CreateCustomFolders : Form
    {
        private enum BeingCancelled
        {
            No = 0,
            ByUser = 1,
            ByError = 2,
        }
        private BeingCancelled IsBeingCancelled;
        private bool FormIsClosing;
        private void CloseForm()
        {
            if (IsBeingCancelled != BeingCancelled.No)
                DialogResult = DialogResult.Cancel;
            else
                DialogResult = DialogResult.OK;

            FormIsClosing = true;
            Timer_CreateCustomFolders.Enabled = false;
            Close();
        }

        string ExecutableDirectory;
        string SaveDirectory;
        string OriginalFolderPrefix;
        string CustomFolderPrefix;

        private string FormText;
        private int ProgressValue_Overall;
        private int ProgressValue_Current;
        private string LabelText_Current;

        public static DialogResult Perform(
            string executableDirectory,
            string saveDirectory,
            string customFolderPrefix,
            string originalFolderPrefix)
        {
            Form_CreateCustomFolders form = new Form_CreateCustomFolders(
                executableDirectory,
                saveDirectory,
                customFolderPrefix,
                originalFolderPrefix);
            return form.ShowDialog();
        }
        private Form_CreateCustomFolders(
            string executableDirectory,
            string saveDirectory,
            string customFolderPrefix,
            string originalFolderPrefix)
        {
            IsBeingCancelled = BeingCancelled.No;

            InitializeComponent();

            Text = "";
            Label_Current.Text = "";

            ExecutableDirectory = executableDirectory;
            SaveDirectory = saveDirectory;
            CustomFolderPrefix = customFolderPrefix;
            OriginalFolderPrefix = originalFolderPrefix;
        }

        private async void CreateCustomFolders()
        {
            BeingCancelled isBeingCancelled = BeingCancelled.No;

            //Find folders
            
            #region
            FormText = "Finding Folders";
            if (!Fun.TryToFindRCT3PalFolders(
                ExecutableDirectory,
                SaveDirectory,
                CustomFolderPrefix,
                OriginalFolderPrefix,
                out List<string> dirs_Exe_NotRCT3Pal,
                out List<string> dirs_Sav_NotRCT3Pal,
                out List<string> dirs_Exe_Custom,
                out List<string> dirs_Sav_Custom,
                out List<string> dirs_Exe_Original,
                out List<string> dirs_Sav_Original))
            {
                IsBeingCancelled = BeingCancelled.ByError;
                CloseForm();
                return;
            }
            #endregion

            //Delete existing custom folders
            #region
            FormText = "Deleting Existing Custom Folders in Executable Directory";
            ProgressBar_Current.Maximum = dirs_Exe_Custom.Count;
            ProgressBar_Current.Value = 0;
            await Task.Run(() =>
            {
                try
                {
                    int n = 0;
                    while ((n < dirs_Exe_Custom.Count) &
                        (IsBeingCancelled == BeingCancelled.No))
                    {
                        string dir = dirs_Exe_Custom[n];
                        LabelText_Current = Path.GetFileName(dir);
                        Directory.Delete(dir, true);
                        n += 1;
                        ProgressValue_Overall += 1;
                        ProgressValue_Current = n;
                    }
                }
                catch (Exception ex)
                {
                    Fun.ShowErrorMessage(
                    "Could not delete all custom directories in executable directory",
                    "Error Deleting Custom Directories",
                    ex);
                    IsBeingCancelled = BeingCancelled.ByError;
                }
            });
            if (IsBeingCancelled != BeingCancelled.No)
            {
                CloseForm();
                return;
            }
            FormText = "Deleting Existing Custom Folders in Save Directory";
            ProgressBar_Current.Maximum = dirs_Sav_Custom.Count;
            ProgressBar_Current.Value = 0;
            await Task.Run(() =>
            {
                try
                {
                    int n = 0;
                    while ((n < dirs_Sav_Custom.Count) &
                        (IsBeingCancelled == BeingCancelled.No))
                    {
                        string dir = dirs_Sav_Custom[n];
                        LabelText_Current = Path.GetFileName(dir);
                        Directory.Delete(dir, true);
                        n += 1;
                        ProgressValue_Overall += 1;
                        ProgressValue_Current = n;
                    }
                }
                catch (Exception ex)
                {
                    Fun.ShowErrorMessage(
                    "Could not delete all custom directories in save directory",
                    "Error Deleting Custom Directories",
                    ex);
                    IsBeingCancelled = BeingCancelled.ByError;
                }
            });
            if (IsBeingCancelled != BeingCancelled.No)
            {
                CloseForm();
                return;
            }
            #endregion

            //Create new custom folders
            #region
            FormText = "Creating new Custom Folders in Executable Directory";
            ProgressBar_Current.Maximum = dirs_Exe_NotRCT3Pal.Count;
            ProgressBar_Current.Value = 0;
            await Task.Run(() =>
            {
                try
                {
                    int n = 0;
                    while ((n < dirs_Exe_NotRCT3Pal.Count) &
                        (IsBeingCancelled == BeingCancelled.No))
                    {
                        string dir = dirs_Exe_NotRCT3Pal[n];
                        string dirName = Path.GetFileName(dir);
                        LabelText_Current = dirName;
                        Fun.CopyDirectory(
                            dir,
                            Fun.DirectoryAndFile(
                                ExecutableDirectory,
                                CustomFolderPrefix + dirName)
                            );
                        n += 1;
                        ProgressValue_Overall += 1;
                        ProgressValue_Current = n;
                    }
                }
                catch (Exception ex)
                {
                    Fun.ShowErrorMessage(
                    "Could not create all custom directories in executable directory",
                    "Error Creating Custom Directories",
                    ex);
                    IsBeingCancelled = BeingCancelled.ByError;
                }
            });
            if (IsBeingCancelled != BeingCancelled.No)
            {
                CloseForm();
                return;
            }
            FormText = "Creating new Custom Folders in Save Directory";
            ProgressBar_Current.Maximum = dirs_Sav_NotRCT3Pal.Count;
            ProgressBar_Current.Value = 0;
            await Task.Run(() =>
            {
                try
                {
                    int n = 0;
                    while ((n < dirs_Sav_NotRCT3Pal.Count) &
                        (IsBeingCancelled == BeingCancelled.No))
                    {
                        string dir = dirs_Sav_NotRCT3Pal[n];
                        string dirName = Path.GetFileName(dir);
                        LabelText_Current = dirName;
                        Fun.CopyDirectory(
                            dir,
                            Fun.DirectoryAndFile(
                                SaveDirectory,
                                CustomFolderPrefix + dirName)
                            );
                        n += 1;
                        ProgressValue_Overall += 1;
                        ProgressValue_Current = n;
                    }
                }
                catch (Exception ex)
                {
                    Fun.ShowErrorMessage(
                    "Could not create all custom directories in save directory",
                    "Error Creating Custom Directories",
                    ex);
                    IsBeingCancelled = BeingCancelled.ByError;
                }
            });
            if (IsBeingCancelled != BeingCancelled.No)
            {
                CloseForm();
                return;
            }
            #endregion

            CloseForm();
        }
        private void Timer_CreateCustomFolders_Tick(object sender, EventArgs e)
        {
            Text = FormText;
            ProgressBar_Overall.Value = Math.Max(
                ProgressValue_Overall,
                ProgressBar_Overall.Maximum);
            ProgressBar_Current.Value = ProgressValue_Current;
            Label_Current.Text = LabelText_Current;
        }

        private void Form_CreateCustomFolders_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!FormIsClosing)
            {
                Console.WriteLine("sadas");
                e.Cancel = true;
                IsBeingCancelled = BeingCancelled.ByUser;
            }
        }

        private void Form_CreateCustomFolders_Load(object sender, EventArgs e)
        {
            CreateCustomFolders();
        }
    }
}
