
namespace RCT3Pal
{
    partial class OptionControl_Bool
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
            this.CheckBox_Bool = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CheckBox_Bool
            // 
            this.CheckBox_Bool.AutoCheck = false;
            this.CheckBox_Bool.AutoSize = true;
            this.CheckBox_Bool.Location = new System.Drawing.Point(3, 3);
            this.CheckBox_Bool.Name = "CheckBox_Bool";
            this.CheckBox_Bool.Size = new System.Drawing.Size(80, 17);
            this.CheckBox_Bool.TabIndex = 0;
            this.CheckBox_Bool.Text = "checkBox1";
            this.CheckBox_Bool.UseVisualStyleBackColor = true;
            this.CheckBox_Bool.Click += new System.EventHandler(this.CheckBox_Bool_Click);
            // 
            // OptionControl_Bool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.Controls.Add(this.CheckBox_Bool);
            this.Name = "OptionControl_Bool";
            this.Size = new System.Drawing.Size(250, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CheckBox_Bool;
    }
}
