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
    public partial class Form_OtherOptions : Form
    {
        private DataTable DataTable_OptionAndValues;

        public static void EditOtherOptions(Dictionary<string, string> options)
        {
            Form_OtherOptions form = new Form_OtherOptions(options);
            if (form.ShowDialog() == DialogResult.Cancel)
                return;
            foreach (DataRow row in form.DataTable_OptionAndValues.Rows)
            {
                options[row["Option"].ToString()] = row["Values"].ToString().TrimStart((char)0x20);
            }
            
        }

        private Form_OtherOptions(Dictionary<string, string> options)
        {
            InitializeComponent();

            DataTable_OptionAndValues = new DataTable();
            DataTable_OptionAndValues.Columns.Add("Option");
            DataTable_OptionAndValues.Columns["Option"].ReadOnly = true;
            DataTable_OptionAndValues.Columns.Add("Values");
            foreach (KeyValuePair<string, string> option in options)
                DataTable_OptionAndValues.Rows.Add(option.Key, option.Value);

            DataGridView_OtherOptions.DataSource = DataTable_OptionAndValues;
        }

        private void Form_OtherOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
                return;

            if (MessageBox.Show(
                "Any unsaved data will be lost. Is this OK?", 
                "Is this OK?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
