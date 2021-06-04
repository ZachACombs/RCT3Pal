
namespace RCT3Pal
{
    partial class OptionControl_Choice
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ComboBox_Choice = new System.Windows.Forms.ComboBox();
            this.Label_Choice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComboBox_Choice
            // 
            this.ComboBox_Choice.Dock = System.Windows.Forms.DockStyle.Right;
            this.ComboBox_Choice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Choice.FormattingEnabled = true;
            this.ComboBox_Choice.Location = new System.Drawing.Point(129, 0);
            this.ComboBox_Choice.Name = "ComboBox_Choice";
            this.ComboBox_Choice.Size = new System.Drawing.Size(121, 21);
            this.ComboBox_Choice.TabIndex = 0;
            // 
            // Label_Choice
            // 
            this.Label_Choice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label_Choice.Location = new System.Drawing.Point(0, 0);
            this.Label_Choice.Name = "Label_Choice";
            this.Label_Choice.Size = new System.Drawing.Size(129, 23);
            this.Label_Choice.TabIndex = 1;
            this.Label_Choice.Text = "label1";
            this.Label_Choice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OptionControl_Choice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.Controls.Add(this.Label_Choice);
            this.Controls.Add(this.ComboBox_Choice);
            this.Name = "OptionControl_Choice";
            this.Size = new System.Drawing.Size(250, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox_Choice;
        private System.Windows.Forms.Label Label_Choice;
    }
}
