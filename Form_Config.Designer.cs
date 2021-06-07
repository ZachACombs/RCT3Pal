
namespace RCT3Pal
{
    partial class Form_Config
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Config));
            this.TextBox_Executable = new System.Windows.Forms.TextBox();
            this.Label_Executable = new System.Windows.Forms.Label();
            this.Button_Executable = new System.Windows.Forms.Button();
            this.Button_Options = new System.Windows.Forms.Button();
            this.Label_Options = new System.Windows.Forms.Label();
            this.TextBox_Options = new System.Windows.Forms.TextBox();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Save = new System.Windows.Forms.Button();
            this.Label_Save = new System.Windows.Forms.Label();
            this.TextBox_Save = new System.Windows.Forms.TextBox();
            this.CheckBox_DontShowBeginningWarning = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TextBox_Executable
            // 
            this.TextBox_Executable.Location = new System.Drawing.Point(123, 12);
            this.TextBox_Executable.Name = "TextBox_Executable";
            this.TextBox_Executable.Size = new System.Drawing.Size(218, 20);
            this.TextBox_Executable.TabIndex = 0;
            this.TextBox_Executable.TextChanged += new System.EventHandler(this.TextBox_Executable_TextChanged);
            // 
            // Label_Executable
            // 
            this.Label_Executable.AutoSize = true;
            this.Label_Executable.Location = new System.Drawing.Point(12, 15);
            this.Label_Executable.Name = "Label_Executable";
            this.Label_Executable.Size = new System.Drawing.Size(105, 13);
            this.Label_Executable.TabIndex = 1;
            this.Label_Executable.Text = "Executable Directory";
            // 
            // Button_Executable
            // 
            this.Button_Executable.Location = new System.Drawing.Point(347, 12);
            this.Button_Executable.Name = "Button_Executable";
            this.Button_Executable.Size = new System.Drawing.Size(24, 20);
            this.Button_Executable.TabIndex = 2;
            this.Button_Executable.Text = "...";
            this.Button_Executable.UseVisualStyleBackColor = true;
            this.Button_Executable.Click += new System.EventHandler(this.Button_Executable_Click);
            // 
            // Button_Options
            // 
            this.Button_Options.Location = new System.Drawing.Point(347, 38);
            this.Button_Options.Name = "Button_Options";
            this.Button_Options.Size = new System.Drawing.Size(24, 20);
            this.Button_Options.TabIndex = 5;
            this.Button_Options.Text = "...";
            this.Button_Options.UseVisualStyleBackColor = true;
            this.Button_Options.Click += new System.EventHandler(this.Button_Options_Click);
            // 
            // Label_Options
            // 
            this.Label_Options.AutoSize = true;
            this.Label_Options.Location = new System.Drawing.Point(12, 41);
            this.Label_Options.Name = "Label_Options";
            this.Label_Options.Size = new System.Drawing.Size(88, 13);
            this.Label_Options.TabIndex = 4;
            this.Label_Options.Text = "Options Directory";
            // 
            // TextBox_Options
            // 
            this.TextBox_Options.Location = new System.Drawing.Point(123, 38);
            this.TextBox_Options.Name = "TextBox_Options";
            this.TextBox_Options.Size = new System.Drawing.Size(218, 20);
            this.TextBox_Options.TabIndex = 3;
            this.TextBox_Options.TextChanged += new System.EventHandler(this.TextBox_Options_TextChanged);
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_OK.Location = new System.Drawing.Point(12, 147);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 6;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Save
            // 
            this.Button_Save.Location = new System.Drawing.Point(347, 64);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(24, 20);
            this.Button_Save.TabIndex = 9;
            this.Button_Save.Text = "...";
            this.Button_Save.UseVisualStyleBackColor = true;
            this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // Label_Save
            // 
            this.Label_Save.AutoSize = true;
            this.Label_Save.Location = new System.Drawing.Point(12, 67);
            this.Label_Save.Name = "Label_Save";
            this.Label_Save.Size = new System.Drawing.Size(77, 13);
            this.Label_Save.TabIndex = 8;
            this.Label_Save.Text = "Save Directory";
            // 
            // TextBox_Save
            // 
            this.TextBox_Save.Location = new System.Drawing.Point(123, 64);
            this.TextBox_Save.Name = "TextBox_Save";
            this.TextBox_Save.Size = new System.Drawing.Size(218, 20);
            this.TextBox_Save.TabIndex = 7;
            this.TextBox_Save.TextChanged += new System.EventHandler(this.TextBox_Save_TextChanged);
            // 
            // CheckBox_DontShowBeginningWarning
            // 
            this.CheckBox_DontShowBeginningWarning.AutoCheck = false;
            this.CheckBox_DontShowBeginningWarning.AutoSize = true;
            this.CheckBox_DontShowBeginningWarning.Location = new System.Drawing.Point(12, 90);
            this.CheckBox_DontShowBeginningWarning.Name = "CheckBox_DontShowBeginningWarning";
            this.CheckBox_DontShowBeginningWarning.Size = new System.Drawing.Size(222, 17);
            this.CheckBox_DontShowBeginningWarning.TabIndex = 10;
            this.CheckBox_DontShowBeginningWarning.Text = "Don\'t Show Warning Message on Startup";
            this.CheckBox_DontShowBeginningWarning.UseVisualStyleBackColor = true;
            this.CheckBox_DontShowBeginningWarning.Click += new System.EventHandler(this.CheckBox_DontShowBeginningWarning_Click);
            // 
            // Form_Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 182);
            this.Controls.Add(this.CheckBox_DontShowBeginningWarning);
            this.Controls.Add(this.Button_Save);
            this.Controls.Add(this.Label_Save);
            this.Controls.Add(this.TextBox_Save);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Button_Options);
            this.Controls.Add(this.Label_Options);
            this.Controls.Add(this.TextBox_Options);
            this.Controls.Add(this.Button_Executable);
            this.Controls.Add(this.Label_Executable);
            this.Controls.Add(this.TextBox_Executable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Config";
            this.Text = "Config";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Config_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_Executable;
        private System.Windows.Forms.Label Label_Executable;
        private System.Windows.Forms.Button Button_Executable;
        private System.Windows.Forms.Button Button_Options;
        private System.Windows.Forms.Label Label_Options;
        private System.Windows.Forms.TextBox TextBox_Options;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Save;
        private System.Windows.Forms.Label Label_Save;
        private System.Windows.Forms.TextBox TextBox_Save;
        private System.Windows.Forms.CheckBox CheckBox_DontShowBeginningWarning;
    }
}