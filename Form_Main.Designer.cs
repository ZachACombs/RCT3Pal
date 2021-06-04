
namespace RCT3Pal
{
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.Button_RCT3 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status_Main = new System.Windows.Forms.ToolStripStatusLabel();
            this.Timer_Progress = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuItem_Config = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.process1 = new System.Diagnostics.Process();
            this.Button_OtherOptions = new System.Windows.Forms.Button();
            this.Button_UpdateOptions = new System.Windows.Forms.Button();
            this.Button_ExportOptions = new System.Windows.Forms.Button();
            this.GroupBox_Options = new System.Windows.Forms.GroupBox();
            this.Panel_Options_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.Panel_Options_Right = new System.Windows.Forms.Panel();
            this.GroupBox_CustomContent = new System.Windows.Forms.GroupBox();
            this.Panel_RCT3 = new System.Windows.Forms.Panel();
            this.Panel_CustomContent = new System.Windows.Forms.Panel();
            this.Panel_Options = new System.Windows.Forms.Panel();
            this.Button_CreateCustom = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.GroupBox_Options.SuspendLayout();
            this.Panel_Options_Right.SuspendLayout();
            this.GroupBox_CustomContent.SuspendLayout();
            this.Panel_RCT3.SuspendLayout();
            this.Panel_CustomContent.SuspendLayout();
            this.Panel_Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_RCT3
            // 
            this.Button_RCT3.Location = new System.Drawing.Point(3, 3);
            this.Button_RCT3.Name = "Button_RCT3";
            this.Button_RCT3.Size = new System.Drawing.Size(75, 23);
            this.Button_RCT3.TabIndex = 0;
            this.Button_RCT3.Text = "Play RCT3";
            this.Button_RCT3.UseVisualStyleBackColor = true;
            this.Button_RCT3.Click += new System.EventHandler(this.Button_RCT3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status_Main});
            this.statusStrip1.Location = new System.Drawing.Point(0, 419);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(624, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Status_Main
            // 
            this.Status_Main.Name = "Status_Main";
            this.Status_Main.Size = new System.Drawing.Size(34, 17);
            this.Status_Main.Text = "RCT3";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Config,
            this.MenuItem_About});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuItem_Config
            // 
            this.MenuItem_Config.Name = "MenuItem_Config";
            this.MenuItem_Config.Size = new System.Drawing.Size(55, 20);
            this.MenuItem_Config.Text = "Config";
            this.MenuItem_Config.Click += new System.EventHandler(this.MenuItem_Config_Click);
            // 
            // MenuItem_About
            // 
            this.MenuItem_About.Name = "MenuItem_About";
            this.MenuItem_About.Size = new System.Drawing.Size(52, 20);
            this.MenuItem_About.Text = "About";
            this.MenuItem_About.Click += new System.EventHandler(this.MenuItem_About_Click);
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // Button_OtherOptions
            // 
            this.Button_OtherOptions.Location = new System.Drawing.Point(3, 0);
            this.Button_OtherOptions.Name = "Button_OtherOptions";
            this.Button_OtherOptions.Size = new System.Drawing.Size(100, 23);
            this.Button_OtherOptions.TabIndex = 4;
            this.Button_OtherOptions.Text = "Other Options";
            this.Button_OtherOptions.UseVisualStyleBackColor = true;
            this.Button_OtherOptions.Click += new System.EventHandler(this.Button_OtherOptions_Click);
            // 
            // Button_UpdateOptions
            // 
            this.Button_UpdateOptions.Location = new System.Drawing.Point(3, 29);
            this.Button_UpdateOptions.Name = "Button_UpdateOptions";
            this.Button_UpdateOptions.Size = new System.Drawing.Size(100, 23);
            this.Button_UpdateOptions.TabIndex = 5;
            this.Button_UpdateOptions.Text = "Save Options";
            this.Button_UpdateOptions.UseVisualStyleBackColor = true;
            this.Button_UpdateOptions.Click += new System.EventHandler(this.Button_UpdateOptions_Click);
            // 
            // Button_ExportOptions
            // 
            this.Button_ExportOptions.Location = new System.Drawing.Point(3, 58);
            this.Button_ExportOptions.Name = "Button_ExportOptions";
            this.Button_ExportOptions.Size = new System.Drawing.Size(100, 23);
            this.Button_ExportOptions.TabIndex = 6;
            this.Button_ExportOptions.Text = "Export Options";
            this.Button_ExportOptions.UseVisualStyleBackColor = true;
            this.Button_ExportOptions.Click += new System.EventHandler(this.Button_ExportOptions_Click);
            // 
            // GroupBox_Options
            // 
            this.GroupBox_Options.Controls.Add(this.Panel_Options_Main);
            this.GroupBox_Options.Controls.Add(this.Panel_Options_Right);
            this.GroupBox_Options.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox_Options.Location = new System.Drawing.Point(12, 3);
            this.GroupBox_Options.Name = "GroupBox_Options";
            this.GroupBox_Options.Size = new System.Drawing.Size(600, 293);
            this.GroupBox_Options.TabIndex = 7;
            this.GroupBox_Options.TabStop = false;
            this.GroupBox_Options.Text = "Options";
            // 
            // Panel_Options_Main
            // 
            this.Panel_Options_Main.AutoScroll = true;
            this.Panel_Options_Main.BackColor = System.Drawing.Color.SlateGray;
            this.Panel_Options_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Options_Main.Location = new System.Drawing.Point(3, 16);
            this.Panel_Options_Main.Name = "Panel_Options_Main";
            this.Panel_Options_Main.Size = new System.Drawing.Size(487, 274);
            this.Panel_Options_Main.TabIndex = 7;
            this.Panel_Options_Main.Resize += new System.EventHandler(this.Panel_Options_Main_Resize);
            // 
            // Panel_Options_Right
            // 
            this.Panel_Options_Right.Controls.Add(this.Button_OtherOptions);
            this.Panel_Options_Right.Controls.Add(this.Button_UpdateOptions);
            this.Panel_Options_Right.Controls.Add(this.Button_ExportOptions);
            this.Panel_Options_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.Panel_Options_Right.Location = new System.Drawing.Point(490, 16);
            this.Panel_Options_Right.Name = "Panel_Options_Right";
            this.Panel_Options_Right.Size = new System.Drawing.Size(107, 274);
            this.Panel_Options_Right.TabIndex = 8;
            // 
            // GroupBox_CustomContent
            // 
            this.GroupBox_CustomContent.Controls.Add(this.Button_CreateCustom);
            this.GroupBox_CustomContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox_CustomContent.Location = new System.Drawing.Point(12, 3);
            this.GroupBox_CustomContent.Name = "GroupBox_CustomContent";
            this.GroupBox_CustomContent.Size = new System.Drawing.Size(600, 53);
            this.GroupBox_CustomContent.TabIndex = 8;
            this.GroupBox_CustomContent.TabStop = false;
            this.GroupBox_CustomContent.Text = "Custom Content (Coming Soon)";
            // 
            // Panel_RCT3
            // 
            this.Panel_RCT3.Controls.Add(this.Button_RCT3);
            this.Panel_RCT3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_RCT3.Location = new System.Drawing.Point(0, 382);
            this.Panel_RCT3.Name = "Panel_RCT3";
            this.Panel_RCT3.Size = new System.Drawing.Size(624, 37);
            this.Panel_RCT3.TabIndex = 9;
            // 
            // Panel_CustomContent
            // 
            this.Panel_CustomContent.Controls.Add(this.GroupBox_CustomContent);
            this.Panel_CustomContent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_CustomContent.Location = new System.Drawing.Point(0, 323);
            this.Panel_CustomContent.Name = "Panel_CustomContent";
            this.Panel_CustomContent.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.Panel_CustomContent.Size = new System.Drawing.Size(624, 59);
            this.Panel_CustomContent.TabIndex = 10;
            // 
            // Panel_Options
            // 
            this.Panel_Options.Controls.Add(this.GroupBox_Options);
            this.Panel_Options.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Options.Location = new System.Drawing.Point(0, 24);
            this.Panel_Options.Name = "Panel_Options";
            this.Panel_Options.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.Panel_Options.Size = new System.Drawing.Size(624, 299);
            this.Panel_Options.TabIndex = 11;
            // 
            // Button_CreateCustom
            // 
            this.Button_CreateCustom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_CreateCustom.Location = new System.Drawing.Point(472, 10);
            this.Button_CreateCustom.Name = "Button_CreateCustom";
            this.Button_CreateCustom.Size = new System.Drawing.Size(125, 23);
            this.Button_CreateCustom.TabIndex = 1;
            this.Button_CreateCustom.Text = "Create Custom Folders";
            this.Button_CreateCustom.UseVisualStyleBackColor = true;
            this.Button_CreateCustom.Click += new System.EventHandler(this.Button_CreateCustom_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.Panel_Options);
            this.Controls.Add(this.Panel_CustomContent);
            this.Controls.Add(this.Panel_RCT3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_Main";
            this.Text = "RCT3 Pal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.GroupBox_Options.ResumeLayout(false);
            this.Panel_Options_Right.ResumeLayout(false);
            this.GroupBox_CustomContent.ResumeLayout(false);
            this.Panel_RCT3.ResumeLayout(false);
            this.Panel_CustomContent.ResumeLayout(false);
            this.Panel_Options.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_RCT3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status_Main;
        private System.Windows.Forms.Timer Timer_Progress;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Config;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.Button Button_OtherOptions;
        private System.Windows.Forms.Button Button_UpdateOptions;
        private System.Windows.Forms.Button Button_ExportOptions;
        private System.Windows.Forms.GroupBox GroupBox_Options;
        private System.Windows.Forms.FlowLayoutPanel Panel_Options_Main;
        private System.Windows.Forms.GroupBox GroupBox_CustomContent;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_About;
        private System.Windows.Forms.Panel Panel_RCT3;
        private System.Windows.Forms.Panel Panel_Options;
        private System.Windows.Forms.Panel Panel_CustomContent;
        private System.Windows.Forms.Panel Panel_Options_Right;
        private System.Windows.Forms.Button Button_CreateCustom;
    }
}

