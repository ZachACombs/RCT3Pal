
namespace RCT3Pal
{
    partial class Form_OtherOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_OtherOptions));
            this.Panel_Bottom = new System.Windows.Forms.Panel();
            this.Button_OK = new System.Windows.Forms.Button();
            this.DataGridView_OtherOptions = new System.Windows.Forms.DataGridView();
            this.Panel_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_OtherOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_Bottom
            // 
            this.Panel_Bottom.Controls.Add(this.Button_OK);
            this.Panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_Bottom.Location = new System.Drawing.Point(0, 182);
            this.Panel_Bottom.Name = "Panel_Bottom";
            this.Panel_Bottom.Size = new System.Drawing.Size(284, 29);
            this.Panel_Bottom.TabIndex = 0;
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(3, 3);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 0;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // DataGridView_OtherOptions
            // 
            this.DataGridView_OtherOptions.AllowUserToAddRows = false;
            this.DataGridView_OtherOptions.AllowUserToDeleteRows = false;
            this.DataGridView_OtherOptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_OtherOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView_OtherOptions.Location = new System.Drawing.Point(0, 0);
            this.DataGridView_OtherOptions.Name = "DataGridView_OtherOptions";
            this.DataGridView_OtherOptions.Size = new System.Drawing.Size(284, 182);
            this.DataGridView_OtherOptions.TabIndex = 1;
            // 
            // Form_OtherOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 211);
            this.Controls.Add(this.DataGridView_OtherOptions);
            this.Controls.Add(this.Panel_Bottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 250);
            this.Name = "Form_OtherOptions";
            this.Text = "Other Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_OtherOptions_FormClosing);
            this.Panel_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_OtherOptions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Bottom;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.DataGridView DataGridView_OtherOptions;
    }
}