
namespace RCT3Pal
{
    partial class OptionControl_Int
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
            this.TextBox_Int = new System.Windows.Forms.TextBox();
            this.Label_Int = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBox_Int
            // 
            this.TextBox_Int.Dock = System.Windows.Forms.DockStyle.Right;
            this.TextBox_Int.Location = new System.Drawing.Point(150, 0);
            this.TextBox_Int.Name = "TextBox_Int";
            this.TextBox_Int.Size = new System.Drawing.Size(100, 20);
            this.TextBox_Int.TabIndex = 0;
            this.TextBox_Int.TextChanged += new System.EventHandler(this.TextBox_Int_TextChanged);
            this.TextBox_Int.Leave += new System.EventHandler(this.TextBox_Int_Leave);
            // 
            // Label_Int
            // 
            this.Label_Int.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label_Int.Location = new System.Drawing.Point(0, 0);
            this.Label_Int.Name = "Label_Int";
            this.Label_Int.Size = new System.Drawing.Size(150, 23);
            this.Label_Int.TabIndex = 1;
            this.Label_Int.Text = "label1";
            this.Label_Int.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OptionControl_Int
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.Controls.Add(this.Label_Int);
            this.Controls.Add(this.TextBox_Int);
            this.Name = "OptionControl_Int";
            this.Size = new System.Drawing.Size(250, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_Int;
        private System.Windows.Forms.Label Label_Int;
    }
}
