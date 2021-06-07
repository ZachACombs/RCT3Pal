using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCT3Pal
{
    public partial class Form_Config : Form
    {
        public string ExecutableDirectory { get { return Var_ExecutableDirectory; } }
        private string Var_ExecutableDirectory;
        public string OptionsDirectory { get { return Var_OptionsDirectory; } }
        private string Var_OptionsDirectory;
        public string SaveDirectory { get { return Var_SaveDirectory; } }
        private string Var_SaveDirectory;
        public bool DontShowBeginningWarning {  get { return Var_DontShowBeginningWarning; } }
        private bool Var_DontShowBeginningWarning;

        public Form_Config(string executableDirectory, string optionsDirectory, string saveDirectory, bool dontShowBeginningWarning)
        {
            InitializeComponent();

            Var_ExecutableDirectory = executableDirectory;
            Var_OptionsDirectory = optionsDirectory;
            Var_SaveDirectory = saveDirectory;
            Var_DontShowBeginningWarning = dontShowBeginningWarning;
            TextBox_Executable.Text = Var_ExecutableDirectory;
            TextBox_Options.Text = Var_OptionsDirectory;
            TextBox_Save.Text = Var_SaveDirectory;
            CheckBox_DontShowBeginningWarning.Checked = Var_DontShowBeginningWarning;
        }

        private void TextBox_Executable_TextChanged(object sender, EventArgs e)
        {
            Var_ExecutableDirectory = TextBox_Executable.Text;
        }
        private void TextBox_Options_TextChanged(object sender, EventArgs e)
        {
            Var_OptionsDirectory = TextBox_Options.Text;
        }
        private void TextBox_Save_TextChanged(object sender, EventArgs e)
        {
            Var_SaveDirectory = TextBox_Save.Text;
        }

        private void Button_Executable_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = false;
            folderBrowser.Description = "Find Executable Directory";
            if (folderBrowser.ShowDialog() != DialogResult.OK)
                return;
            TextBox_Executable.Text = folderBrowser.SelectedPath;
        }
        private void Button_Options_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = false;
            folderBrowser.Description = "Find Options Directory";
            if (folderBrowser.ShowDialog() != DialogResult.OK)
                return;
            TextBox_Options.Text = folderBrowser.SelectedPath;
        }
        private void Button_Save_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = false;
            folderBrowser.Description = "Find Save Directory";
            if (folderBrowser.ShowDialog() != DialogResult.OK)
                return;
            TextBox_Save.Text = folderBrowser.SelectedPath;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Form_Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
                return;

            if (MessageBox.Show(
                    "Any unsaved configurations will be lost. Is this OK?",
                    "Is This OK",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            e.Cancel = true;
        }

        private void CheckBox_DontShowBeginningWarning_Click(object sender, EventArgs e)
        {
            if (CheckBox_DontShowBeginningWarning.Checked)
            {
                Var_DontShowBeginningWarning = false;
                CheckBox_DontShowBeginningWarning.Checked = false;
            }
            else
            {
                Var_DontShowBeginningWarning = true;
                CheckBox_DontShowBeginningWarning.Checked = true;
            }
        }
    }
}
