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
    public partial class OptionControl_Bool : UserControl
    {
        private bool Var_DefaultValue; public bool DefaultValue { get { return Var_DefaultValue; } }

        public bool IsTrue
        {
            get
            {
                return CheckBox_Bool.Checked;
            }
            set
            {
                CheckBox_Bool.Checked = value;
            }
        }

        public OptionControl_Bool(string optionName, bool isChecked)
        {
            InitializeComponent();

            CheckBox_Bool.Text = Fun.MakeOptionDisplayName(optionName);
            Var_DefaultValue = isChecked;
            CheckBox_Bool.Checked = isChecked;
        }

        private void CheckBox_Bool_Click(object sender, EventArgs e)
        {
            if (CheckBox_Bool.Checked)
                CheckBox_Bool.Checked = false;
            else
                CheckBox_Bool.Checked = true;
        }
    }
}
