namespace FileCompare
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            lblAppName = new Label();
            btnCopyFromLeft = new Button();
            btnLeftDir = new Button();
            btnCopyFromRight = new Button();
            btnRightDir = new Button();
            lvwLeftDir = new ListView();
            lvwRightDir = new ListView();
            txtLeftDir = new TextBox();
            txtRightDir = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(20, 20);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel3);
            splitContainer1.Panel1.Controls.Add(panel2);
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel6);
            splitContainer1.Panel2.Controls.Add(panel5);
            splitContainer1.Panel2.Controls.Add(panel4);
            splitContainer1.Size = new Size(802, 407);
            splitContainer1.SplitterDistance = 403;
            splitContainer1.SplitterWidth = 10;
            splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCopyFromLeft);
            panel1.Controls.Add(lblAppName);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(403, 60);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(txtLeftDir);
            panel2.Controls.Add(btnLeftDir);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 60);
            panel2.Name = "panel2";
            panel2.Size = new Size(403, 57);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(lvwLeftDir);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 117);
            panel3.Name = "panel3";
            panel3.Size = new Size(403, 290);
            panel3.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnCopyFromRight);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(389, 60);
            panel4.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.Controls.Add(txtRightDir);
            panel5.Controls.Add(btnRightDir);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 60);
            panel5.Name = "panel5";
            panel5.Size = new Size(389, 57);
            panel5.TabIndex = 1;
            // 
            // panel6
            // 
            panel6.Controls.Add(lvwRightDir);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 117);
            panel6.Name = "panel6";
            panel6.Size = new Size(389, 290);
            panel6.TabIndex = 2;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("맑은 고딕", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblAppName.ForeColor = SystemColors.Highlight;
            lblAppName.Location = new Point(3, 9);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(186, 37);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "File Compare";
            // 
            // btnCopyFromLeft
            // 
            btnCopyFromLeft.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCopyFromLeft.Location = new Point(318, 17);
            btnCopyFromLeft.Name = "btnCopyFromLeft";
            btnCopyFromLeft.Size = new Size(75, 23);
            btnCopyFromLeft.TabIndex = 1;
            btnCopyFromLeft.Text = ">>>";
            btnCopyFromLeft.UseVisualStyleBackColor = true;
            // 
            // btnLeftDir
            // 
            btnLeftDir.Location = new Point(318, 19);
            btnLeftDir.Name = "btnLeftDir";
            btnLeftDir.Size = new Size(75, 23);
            btnLeftDir.TabIndex = 2;
            btnLeftDir.Text = "폴더선택";
            btnLeftDir.UseVisualStyleBackColor = true;
            btnLeftDir.Click += button2_Click;
            // 
            // btnCopyFromRight
            // 
            btnCopyFromRight.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCopyFromRight.Location = new Point(12, 17);
            btnCopyFromRight.Name = "btnCopyFromRight";
            btnCopyFromRight.Size = new Size(75, 23);
            btnCopyFromRight.TabIndex = 0;
            btnCopyFromRight.Text = "<<<";
            btnCopyFromRight.UseVisualStyleBackColor = true;
            // 
            // btnRightDir
            // 
            btnRightDir.Location = new Point(300, 19);
            btnRightDir.Name = "btnRightDir";
            btnRightDir.Size = new Size(75, 23);
            btnRightDir.TabIndex = 1;
            btnRightDir.Text = "폴더선택";
            btnRightDir.UseVisualStyleBackColor = true;
            // 
            // lvwLeftDir
            // 
            lvwLeftDir.Location = new Point(0, 0);
            lvwLeftDir.Name = "lvwLeftDir";
            lvwLeftDir.Size = new Size(403, 290);
            lvwLeftDir.TabIndex = 0;
            lvwLeftDir.UseCompatibleStateImageBehavior = false;
            // 
            // lvwRightDir
            // 
            lvwRightDir.Location = new Point(0, 0);
            lvwRightDir.Name = "lvwRightDir";
            lvwRightDir.Size = new Size(389, 290);
            lvwRightDir.TabIndex = 0;
            lvwRightDir.UseCompatibleStateImageBehavior = false;
            // 
            // txtLeftDir
            // 
            txtLeftDir.Location = new Point(14, 20);
            txtLeftDir.Name = "txtLeftDir";
            txtLeftDir.Size = new Size(298, 23);
            txtLeftDir.TabIndex = 3;
            // 
            // txtRightDir
            // 
            txtRightDir.Location = new Point(12, 20);
            txtRightDir.Name = "txtRightDir";
            txtRightDir.Size = new Size(282, 23);
            txtRightDir.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(842, 447);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Padding = new Padding(20);
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel6.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel panel3;
        private Panel panel2;
        private Panel panel1;
        private Panel panel6;
        private Panel panel5;
        private Panel panel4;
        private Label lblAppName;
        private ListView lvwLeftDir;
        private Button btnLeftDir;
        private Button btnCopyFromLeft;
        private Button btnRightDir;
        private Button btnCopyFromRight;
        private TextBox txtLeftDir;
        private ListView lvwRightDir;
        private TextBox txtRightDir;
    }
}
