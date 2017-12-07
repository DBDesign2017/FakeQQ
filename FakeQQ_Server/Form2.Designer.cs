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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.changePasswdGroupBox = new System.Windows.Forms.GroupBox();
            this.changePasswdWarningLabel = new System.Windows.Forms.Label();
            this.changePasswdButton = new System.Windows.Forms.Button();
            this.newPasswdTextBox2 = new System.Windows.Forms.TextBox();
            this.newPasswdTextBox1 = new System.Windows.Forms.TextBox();
            this.oldPasswdTextBox = new System.Windows.Forms.TextBox();
            this.newPasswdLabel = new System.Windows.Forms.Label();
            this.newPasswdLabel1 = new System.Windows.Forms.Label();
            this.oldPasswdLabel = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.searchUserDataGridView = new System.Windows.Forms.DataGridView();
            this.searchUserTextBox = new System.Windows.Forms.TextBox();
            this.searchUserButton = new System.Windows.Forms.Button();
            this.searchUserComboBox = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.changePasswdGroupBox.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchUserDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.changePasswdGroupBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(501, 410);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "管理员账户";
            // 
            // changePasswdGroupBox
            // 
            this.changePasswdGroupBox.Controls.Add(this.changePasswdWarningLabel);
            this.changePasswdGroupBox.Controls.Add(this.changePasswdButton);
            this.changePasswdGroupBox.Controls.Add(this.newPasswdTextBox2);
            this.changePasswdGroupBox.Controls.Add(this.newPasswdTextBox1);
            this.changePasswdGroupBox.Controls.Add(this.oldPasswdTextBox);
            this.changePasswdGroupBox.Controls.Add(this.newPasswdLabel);
            this.changePasswdGroupBox.Controls.Add(this.newPasswdLabel1);
            this.changePasswdGroupBox.Controls.Add(this.oldPasswdLabel);
            this.changePasswdGroupBox.Location = new System.Drawing.Point(3, 3);
            this.changePasswdGroupBox.Name = "changePasswdGroupBox";
            this.changePasswdGroupBox.Size = new System.Drawing.Size(495, 187);
            this.changePasswdGroupBox.TabIndex = 0;
            this.changePasswdGroupBox.TabStop = false;
            this.changePasswdGroupBox.Text = "修改密码";
            // 
            // changePasswdWarningLabel
            // 
            this.changePasswdWarningLabel.AutoSize = true;
            this.changePasswdWarningLabel.Location = new System.Drawing.Point(72, 140);
            this.changePasswdWarningLabel.Name = "changePasswdWarningLabel";
            this.changePasswdWarningLabel.Size = new System.Drawing.Size(0, 12);
            this.changePasswdWarningLabel.TabIndex = 7;
            // 
            // changePasswdButton
            // 
            this.changePasswdButton.Location = new System.Drawing.Point(280, 130);
            this.changePasswdButton.Name = "changePasswdButton";
            this.changePasswdButton.Size = new System.Drawing.Size(75, 23);
            this.changePasswdButton.TabIndex = 6;
            this.changePasswdButton.Text = "确认";
            this.changePasswdButton.UseVisualStyleBackColor = true;
            this.changePasswdButton.Click += new System.EventHandler(this.changePasswdButton_Click);
            // 
            // newPasswdTextBox2
            // 
            this.newPasswdTextBox2.Location = new System.Drawing.Point(255, 90);
            this.newPasswdTextBox2.Name = "newPasswdTextBox2";
            this.newPasswdTextBox2.PasswordChar = '*';
            this.newPasswdTextBox2.Size = new System.Drawing.Size(100, 21);
            this.newPasswdTextBox2.TabIndex = 5;
            // 
            // newPasswdTextBox1
            // 
            this.newPasswdTextBox1.Location = new System.Drawing.Point(256, 53);
            this.newPasswdTextBox1.Name = "newPasswdTextBox1";
            this.newPasswdTextBox1.PasswordChar = '*';
            this.newPasswdTextBox1.Size = new System.Drawing.Size(100, 21);
            this.newPasswdTextBox1.TabIndex = 4;
            // 
            // oldPasswdTextBox
            // 
            this.oldPasswdTextBox.Location = new System.Drawing.Point(256, 20);
            this.oldPasswdTextBox.Name = "oldPasswdTextBox";
            this.oldPasswdTextBox.PasswordChar = '*';
            this.oldPasswdTextBox.Size = new System.Drawing.Size(100, 21);
            this.oldPasswdTextBox.TabIndex = 3;
            // 
            // newPasswdLabel
            // 
            this.newPasswdLabel.AutoSize = true;
            this.newPasswdLabel.Location = new System.Drawing.Point(122, 96);
            this.newPasswdLabel.Name = "newPasswdLabel";
            this.newPasswdLabel.Size = new System.Drawing.Size(113, 12);
            this.newPasswdLabel.TabIndex = 2;
            this.newPasswdLabel.Text = "请再次输入新密码：";
            // 
            // newPasswdLabel1
            // 
            this.newPasswdLabel1.AutoSize = true;
            this.newPasswdLabel1.Location = new System.Drawing.Point(146, 58);
            this.newPasswdLabel1.Name = "newPasswdLabel1";
            this.newPasswdLabel1.Size = new System.Drawing.Size(89, 12);
            this.newPasswdLabel1.TabIndex = 1;
            this.newPasswdLabel1.Text = "请输入新密码：";
            // 
            // oldPasswdLabel
            // 
            this.oldPasswdLabel.AutoSize = true;
            this.oldPasswdLabel.Location = new System.Drawing.Point(146, 23);
            this.oldPasswdLabel.Name = "oldPasswdLabel";
            this.oldPasswdLabel.Size = new System.Drawing.Size(89, 12);
            this.oldPasswdLabel.TabIndex = 0;
            this.oldPasswdLabel.Text = "请输入原密码：";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.searchUserComboBox);
            this.tabPage4.Controls.Add(this.searchUserButton);
            this.tabPage4.Controls.Add(this.searchUserTextBox);
            this.tabPage4.Controls.Add(this.searchUserDataGridView);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(501, 410);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "用户管理";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // searchUserDataGridView
            // 
            this.searchUserDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.searchUserDataGridView.Location = new System.Drawing.Point(3, 53);
            this.searchUserDataGridView.Name = "searchUserDataGridView";
            this.searchUserDataGridView.RowTemplate.Height = 23;
            this.searchUserDataGridView.Size = new System.Drawing.Size(495, 150);
            this.searchUserDataGridView.TabIndex = 0;
            // 
            // searchUserTextBox
            // 
            this.searchUserTextBox.Location = new System.Drawing.Point(228, 16);
            this.searchUserTextBox.Name = "searchUserTextBox";
            this.searchUserTextBox.Size = new System.Drawing.Size(100, 21);
            this.searchUserTextBox.TabIndex = 1;
            // 
            // searchUserButton
            // 
            this.searchUserButton.Location = new System.Drawing.Point(369, 16);
            this.searchUserButton.Name = "searchUserButton";
            this.searchUserButton.Size = new System.Drawing.Size(75, 23);
            this.searchUserButton.TabIndex = 2;
            this.searchUserButton.Text = "button2";
            this.searchUserButton.UseVisualStyleBackColor = true;
            // 
            // searchUserComboBox
            // 
            this.searchUserComboBox.FormattingEnabled = true;
            this.searchUserComboBox.Location = new System.Drawing.Point(72, 16);
            this.searchUserComboBox.Name = "searchUserComboBox";
            this.searchUserComboBox.Size = new System.Drawing.Size(121, 20);
            this.searchUserComboBox.TabIndex = 3;
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
            this.tabPage3.ResumeLayout(false);
            this.changePasswdGroupBox.ResumeLayout(false);
            this.changePasswdGroupBox.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchUserDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button systemMessageButton;
        private System.Windows.Forms.TextBox systemMessageTextBox;
        private System.Windows.Forms.Label releaseSystemMessageWarning;
        public System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox changePasswdGroupBox;
        private System.Windows.Forms.Label newPasswdLabel;
        private System.Windows.Forms.Label newPasswdLabel1;
        private System.Windows.Forms.Label oldPasswdLabel;
        private System.Windows.Forms.Button changePasswdButton;
        private System.Windows.Forms.TextBox newPasswdTextBox2;
        private System.Windows.Forms.TextBox newPasswdTextBox1;
        private System.Windows.Forms.TextBox oldPasswdTextBox;
        private System.Windows.Forms.Label changePasswdWarningLabel;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox searchUserComboBox;
        private System.Windows.Forms.Button searchUserButton;
        private System.Windows.Forms.TextBox searchUserTextBox;
        private System.Windows.Forms.DataGridView searchUserDataGridView;
    }
}