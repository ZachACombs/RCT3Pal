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
    public partial class Form_RawData : Form
    {
        public Form_RawData(string text)
        {
            InitializeComponent();

            TextBox_RawData.Text = text;
        }
    }
}
