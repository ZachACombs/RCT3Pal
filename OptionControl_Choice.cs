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
    public partial class OptionControl_Choice : UserControl
    {
        public int SelectedIndex
        {
            get { return ComboBox_Choice.SelectedIndex; }
            set { ComboBox_Choice.SelectedIndex = value; }
        }

        public OptionControl_Choice(string optionName, int defaultIndex, string[] choices)
        {
            InitializeComponent();

            Label_Choice.Text = Fun.MakeOptionDisplayName(optionName);

            ComboBox_Choice.Items.Clear();
            foreach (string choice in choices)
                ComboBox_Choice.Items.Add(choice);
            ComboBox_Choice.SelectedIndex = defaultIndex;
        }
    }
}
