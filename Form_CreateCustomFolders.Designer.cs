
namespace RCT3Pal
{
    partial class Form_CreateCustomFolders
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CreateCustomFolders));
            this.Timer_CreateCustomFolders = new System.Windows.Forms.Timer(this.components);
            this.ProgressBar_Overall = new System.Windows.Forms.ProgressBar();
            this.Label_Current = new System.Windows.Forms.Label();
            this.ProgressBar_Current = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Timer_CreateCustomFolders
            // 
            this.Timer_CreateCustomFolders.Enabled = true;
            this.Timer_CreateCustomFolders.Tick += new System.EventHandler(this.Timer_CreateCustomFolders_Tick);
            // 
            // ProgressBar_Overall
            // 
            this.ProgressBar_Overall.Location = new System.Drawing.Point(12, 12);
            this.ProgressBar_Overall.Name = "ProgressBar_Overall";
            this.ProgressBar_Overall.Size = new System.Drawing.Size(366, 23);
            this.ProgressBar_Overall.TabIndex = 0;
            // 
            // Label_Current
            // 
            this.Label_Current.AutoSize = true;
            this.Label_Current.Location = new System.Drawing.Point(12, 67);
            this.Label_Current.Name = "Label_Current";
            this.Label_Current.Size = new System.Drawing.Size(35, 13);
            this.Label_Current.TabIndex = 1;
            this.Label_Current.Text = "label1";
            // 
            // ProgressBar_Current
            // 
            this.ProgressBar_Current.Location = new System.Drawing.Point(12, 41);
            this.ProgressBar_Current.Name = "ProgressBar_Current";
            this.ProgressBar_Current.Size = new System.Drawing.Size(366, 23);
            this.ProgressBar_Current.TabIndex = 2;
            // 
            // Form_CreateCustomFolders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 87);
            this.Controls.Add(this.ProgressBar_Current);
            this.Controls.Add(this.Label_Current);
            this.Controls.Add(this.ProgressBar_Overall);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_CreateCustomFolders";
            this.Text = "Form_CreateCustomFolders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_CreateCustomFolders_FormClosing);
            this.Load += new System.EventHandler(this.Form_CreateCustomFolders_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Timer_CreateCustomFolders;
        private System.Windows.Forms.ProgressBar ProgressBar_Overall;
        private System.Windows.Forms.Label Label_Current;
        private System.Windows.Forms.ProgressBar ProgressBar_Current;
    }
}