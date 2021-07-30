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
    public partial class OptionControl_Int : UserControl
    {
        private int Var_DefaultValue; public int DefaultValue { get { return Var_DefaultValue; } }

        public int Min;
        public int Max;
        private int Var_Value;
        public void Set_Value(int value)
        {
            if (value < Min)
            {
                Var_Value = Min;
                return;
            }
            if (value > Max)
            {
                Var_Value = Max;
                return;
            }
            Var_Value = value;
        }
        public int Get_Value()
        {
            return Var_Value;
        }

        public OptionControl_Int(string optionName, int defaultValue, int min, int max)
        {
            InitializeComponent();

            Label_Int.Text = Fun.MakeOptionDisplayName(optionName);

            Min = min;
            Max = max;
            Var_DefaultValue = defaultValue;
            Set_Value(defaultValue);
            TextBox_Int.Text = Get_Value().ToString();
        }

        private void TextBox_Int_TextChanged(object sender, EventArgs e)
        {
            char[] cc = TextBox_Int.Text.ToCharArray();
            List<char> newValue_Chars = new List<char>();
            foreach (char c in cc)
            {
                if (c == 0x2D)
                {
                    if (newValue_Chars.Count == 0)
                        newValue_Chars.Add(c);
                }
                if (c >= 0x30 & c <= 0x39)
                    newValue_Chars.Add(c);
            }
            if (newValue_Chars.Count == 0)
            {
                Set_Value(0);
                return;
            }

            if (!int.TryParse(new string(newValue_Chars.ToArray()), out int newValue))
            {
                if (newValue_Chars[0] == 0x2D)
                    Set_Value((newValue_Chars.Count > 1) ? Min : 0);
                else
                    Set_Value(Max);
                return;
            }
            Set_Value(newValue);
        }

        private void TextBox_Int_Leave(object sender, EventArgs e)
        {
            TextBox_Int.Text = Get_Value().ToString();
        }
    }
}
