namespace Malevolence_Mod_Manager
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.InstalledMods = new System.Windows.Forms.ListView();
            this.Mod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Launch = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.setModFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setGameFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.installNewModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ModDirToolText = new System.Windows.Forms.ToolStripStatusLabel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.htmlDescription = new System.Windows.Forms.WebBrowser();
            this.ApplyProgressBar = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // InstalledMods
            // 
            this.InstalledMods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.InstalledMods.CheckBoxes = true;
            this.InstalledMods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Mod,
            this.Version,
            this.Author});
            this.InstalledMods.FullRowSelect = true;
            this.InstalledMods.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.InstalledMods.HideSelection = false;
            this.InstalledMods.Location = new System.Drawing.Point(15, 46);
            this.InstalledMods.Margin = new System.Windows.Forms.Padding(6);
            this.InstalledMods.MultiSelect = false;
            this.InstalledMods.Name = "InstalledMods";
            this.InstalledMods.Size = new System.Drawing.Size(664, 311);
            this.InstalledMods.TabIndex = 3;
            this.InstalledMods.UseCompatibleStateImageBehavior = false;
            this.InstalledMods.View = System.Windows.Forms.View.Details;
            this.InstalledMods.SelectedIndexChanged += new System.EventHandler(this.InstalledMods_SelectedIndexChanged);
            // 
            // Mod
            // 
            this.Mod.Text = "Mod";
            this.Mod.Width = 134;
            // 
            // Version
            // 
            this.Version.Text = "Version";
            this.Version.Width = 94;
            // 
            // Author
            // 
            this.Author.Text = "Author";
            this.Author.Width = 100;
            // 
            // Launch
            // 
            this.Launch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Launch.Location = new System.Drawing.Point(15, 369);
            this.Launch.Margin = new System.Windows.Forms.Padding(6);
            this.Launch.Name = "Launch";
            this.Launch.Size = new System.Drawing.Size(664, 67);
            this.Launch.TabIndex = 5;
            this.Launch.Text = "Apply Selected Mods";
            this.Launch.UseVisualStyleBackColor = true;
            this.Launch.Click += new System.EventHandler(this.Launch_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(974, 40);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setGameFolderToolStripMenuItem,
            this.setModFolderToolStripMenuItem,
            this.toolStripSeparator2,
            this.installNewModToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(64, 36);
            this.toolStripMenuItem1.Text = "File";
            // 
            // setModFolderToolStripMenuItem
            // 
            this.setModFolderToolStripMenuItem.Name = "setModFolderToolStripMenuItem";
            this.setModFolderToolStripMenuItem.Size = new System.Drawing.Size(280, 36);
            this.setModFolderToolStripMenuItem.Text = "Set Mod Folder";
            this.setModFolderToolStripMenuItem.Click += new System.EventHandler(this.setModFolderToolStripMenuItem_Click);
            // 
            // setGameFolderToolStripMenuItem
            // 
            this.setGameFolderToolStripMenuItem.Name = "setGameFolderToolStripMenuItem";
            this.setGameFolderToolStripMenuItem.Size = new System.Drawing.Size(280, 36);
            this.setGameFolderToolStripMenuItem.Text = "Set Game Folder";
            this.setGameFolderToolStripMenuItem.Click += new System.EventHandler(this.setGameFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(277, 6);
            // 
            // installNewModToolStripMenuItem
            // 
            this.installNewModToolStripMenuItem.Name = "installNewModToolStripMenuItem";
            this.installNewModToolStripMenuItem.Size = new System.Drawing.Size(280, 36);
            this.installNewModToolStripMenuItem.Text = "Open Mod Folder";
            this.installNewModToolStripMenuItem.Click += new System.EventHandler(this.installNewModToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(277, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(280, 36);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.ModDirToolText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 442);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(974, 37);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(139, 32);
            this.toolStripStatusLabel1.Text = "Mod Folder";
            // 
            // ModDirToolText
            // 
            this.ModDirToolText.Name = "ModDirToolText";
            this.ModDirToolText.Size = new System.Drawing.Size(30, 32);
            this.ModDirToolText.Text = "...";
            // 
            // htmlDescription
            // 
            this.htmlDescription.AllowWebBrowserDrop = false;
            this.htmlDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.htmlDescription.IsWebBrowserContextMenuEnabled = false;
            this.htmlDescription.Location = new System.Drawing.Point(688, 46);
            this.htmlDescription.MinimumSize = new System.Drawing.Size(0, 20);
            this.htmlDescription.Name = "htmlDescription";
            this.htmlDescription.Size = new System.Drawing.Size(274, 390);
            this.htmlDescription.TabIndex = 11;
            this.htmlDescription.Url = new System.Uri("http://s", System.UriKind.Absolute);
            this.htmlDescription.WebBrowserShortcutsEnabled = false;
            // 
            // ApplyProgressBar
            // 
            this.ApplyProgressBar.Location = new System.Drawing.Point(399, 12);
            this.ApplyProgressBar.Name = "ApplyProgressBar";
            this.ApplyProgressBar.Size = new System.Drawing.Size(280, 25);
            this.ApplyProgressBar.TabIndex = 12;
            this.ApplyProgressBar.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 479);
            this.Controls.Add(this.ApplyProgressBar);
            this.Controls.Add(this.htmlDescription);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Launch);
            this.Controls.Add(this.InstalledMods);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(750, 550);
            this.Name = "Form1";
            this.Text = "Malevolence Mod Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.ListView InstalledMods;
        private System.Windows.Forms.Button Launch;
        private System.Windows.Forms.ColumnHeader Mod;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.ColumnHeader Author;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setModFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installNewModToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel ModDirToolText;
        private System.Windows.Forms.WebBrowser htmlDescription;
        private System.Windows.Forms.ProgressBar ApplyProgressBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setGameFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

