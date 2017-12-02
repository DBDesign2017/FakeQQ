namespace FakeQQ_Server
{
    partial class Form2
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.releaseSystemMessageWarning = new System.Windows.Forms.Label();
            this.systemMessageButton = new System.Windows.Forms.Button();
            this.systemMessageTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(509, 436);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(501, 410);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "服务";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(135, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(339, 352);
            this.listBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "启动服务";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.releaseSystemMessageWarning);
            this.tabPage2.Controls.Add(this.systemMessageButton);
            this.tabPage2.Controls.Add(this.systemMessageTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(501, 410);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "发布系统消息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // releaseSystemMessageWarning
            // 
            this.releaseSystemMessageWarning.AutoSize = true;
            this.releaseSystemMessageWarning.Location = new System.Drawing.Point(7, 12);
            this.releaseSystemMessageWarning.Name = "releaseSystemMessageWarning";
            this.releaseSystemMessageWarning.Size = new System.Drawing.Size(359, 12);
            this.releaseSystemMessageWarning.TabIndex = 2;
            this.releaseSystemMessageWarning.Text = "注意：此消息将被发送给所有在线用户，而且您最多能输入100个字";
            // 
            // systemMessageButton
            // 
            this.systemMessageButton.Location = new System.Drawing.Point(420, 129);
            this.systemMessageButton.Name = "systemMessageButton";
            this.systemMessageButton.Size = new System.Drawing.Size(75, 23);
            this.systemMessageButton.TabIndex = 1;
            this.systemMessageButton.Text = "发布";
            this.systemMessageButton.UseVisualStyleBackColor = true;
            this.systemMessageButton.Click += new System.EventHandler(this.systemMessageButton_Click);
            // 
            // systemMessageTextBox
            // 
            this.systemMessageTextBox.Location = new System.Drawing.Point(7, 30);
            this.systemMessageTextBox.MaxLength = 100;
            this.systemMessageTextBox.Multiline = true;
            this.systemMessageTextBox.Name = "systemMessageTextBox";
            this.systemMessageTextBox.Size = new System.Drawing.Size(488, 83);
            this.systemMessageTextBox.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 461);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form2";
            this.Text = "FakeQQ服务端";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button systemMessageButton;
        private System.Windows.Forms.TextBox systemMessageTextBox;
        private System.Windows.Forms.Label releaseSystemMessageWarning;
    }
}