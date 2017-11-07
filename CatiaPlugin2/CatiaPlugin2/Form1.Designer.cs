namespace CatiaPlugin2
{
    partial class folderSelection
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
            this.button1 = new System.Windows.Forms.Button();
            this.macrosPanel = new System.Windows.Forms.Panel();
            this.paramPanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1174, 98);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select Macro Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // macrosPanel
            // 
            this.macrosPanel.BackColor = System.Drawing.Color.White;
            this.macrosPanel.Location = new System.Drawing.Point(19, 136);
            this.macrosPanel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.macrosPanel.Name = "macrosPanel";
            this.macrosPanel.Size = new System.Drawing.Size(561, 1212);
            this.macrosPanel.TabIndex = 1;
            // 
            // paramPanel
            // 
            this.paramPanel.BackColor = System.Drawing.Color.White;
            this.paramPanel.Location = new System.Drawing.Point(632, 136);
            this.paramPanel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.paramPanel.Name = "paramPanel";
            this.paramPanel.Size = new System.Drawing.Size(561, 1156);
            this.paramPanel.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(19, 1356);
            this.button2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(1174, 98);
            this.button2.TabIndex = 1;
            this.button2.Text = "Run Macro";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1215, 18);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1269, 1418);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(12, 58);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1245, 1348);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(12, 58);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(176, 30);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // folderSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2486, 1735);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.paramPanel);
            this.Controls.Add(this.macrosPanel);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "folderSelection";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.folderSelection_Load);
            this.Resize += new System.EventHandler(this.folderSelection_Resize);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel macrosPanel;
        private System.Windows.Forms.Panel paramPanel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

