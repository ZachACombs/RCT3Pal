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
        private Form_Main ParentForm;
        private string OptionName;

        public OptionControl_Bool(Form_Main parentForm, string optionName, bool isChecked)
        {
            InitializeComponent();

            ParentForm = parentForm;
            OptionName = optionName;
            CheckBox_Bool.Text = Fun.MakeOptionDisplayName(optionName);
            SetValues(isChecked);
        }

        ///<summary>Sets values without updating options dictionary</summary>
        public void SetValues(bool isTrue)
        {
            CheckBox_Bool.Checked = isTrue;
        }
        private void CheckBox_Bool_Click(object sender, EventArgs e)
        {
            if (CheckBox_Bool.Checked)
                CheckBox_Bool.Checked = false;
            else
                CheckBox_Bool.Checked = true;
            ParentForm.SetOption(OptionName,
                new OptionValue(CheckBox_Bool.Checked));
        }
    }
}
