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
    public partial class Form_CustomAssets : Form
    {
        public bool UseCustomAssets { get { return Var_UseCustomAssets; } }
        private bool Var_UseCustomAssets;
        private string 
            Config_ExecutableDirectory,
            Config_SaveDirectory,
            Prefix_Custom,
            Prefix_Original;

        private void Button_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void Form_CustomAssets_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
                return;
            if (MessageBox.Show(
                "Any unsaved changes will be lost. Is this OK?",
                "Is This OK",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            e.Cancel = true;
        }

        private void CheckBox_UseCustomAssets_Click(object sender, EventArgs e)
        {
            if (CheckBox_UseCustomAssets.Checked)
            {
                CheckBox_UseCustomAssets.Checked = false;
                Var_UseCustomAssets = false;
            }
            else
            {
                CheckBox_UseCustomAssets.Checked = true;
                Var_UseCustomAssets = true;
            }
        }

        public Form_CustomAssets(
            bool useCustomAssets,
            string config_ExecutableDirectory,
            string config_SaveDirectory,
            string prefix_Custom,
            string prefix_Original)
        {
            InitializeComponent();

            Var_UseCustomAssets = useCustomAssets;
            CheckBox_UseCustomAssets.Checked = useCustomAssets;

            Config_ExecutableDirectory = config_ExecutableDirectory;
            Config_SaveDirectory = config_SaveDirectory;
            Prefix_Custom = prefix_Custom;
            Prefix_Original = prefix_Original;
        }
    }
}
