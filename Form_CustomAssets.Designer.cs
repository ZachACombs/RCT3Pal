
namespace RCT3Pal
{
    partial class Form_CustomAssets
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CustomAssets));
            this.CheckBox_UseCustomAssets = new System.Windows.Forms.CheckBox();
            this.Button_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CheckBox_UseCustomAssets
            // 
            this.CheckBox_UseCustomAssets.AutoCheck = false;
            this.CheckBox_UseCustomAssets.AutoSize = true;
            this.CheckBox_UseCustomAssets.Location = new System.Drawing.Point(12, 16);
            this.CheckBox_UseCustomAssets.Name = "CheckBox_UseCustomAssets";
            this.CheckBox_UseCustomAssets.Size = new System.Drawing.Size(117, 17);
            this.CheckBox_UseCustomAssets.TabIndex = 3;
            this.CheckBox_UseCustomAssets.Text = "Use Custom Assets";
            this.CheckBox_UseCustomAssets.UseVisualStyleBackColor = true;
            this.CheckBox_UseCustomAssets.Click += new System.EventHandler(this.CheckBox_UseCustomAssets_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_OK.Location = new System.Drawing.Point(12, 80);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 4;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Form_CustomAssets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 115);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.CheckBox_UseCustomAssets);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_CustomAssets";
            this.Text = "Custom Assets Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_CustomAssets_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox CheckBox_UseCustomAssets;
        private System.Windows.Forms.Button Button_OK;
    }
}