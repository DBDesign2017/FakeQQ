using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakeQQ_Server
{
    public partial class Form2 : Form
    {
        private ArrayList onlineUserID = new ArrayList();
        private ServerOperation s;
        public Form2(string AdministratorID)
        {
            InitializeComponent();
            s = new ServerOperation(AdministratorID);
            ServerOperation.OneUserLogin += new ServerOperation.CrossThreadCallControlHandler(OneUserLogin);
            ServerOperation.AdministratorModifyPassword += new ServerOperation.CrossThreadCallControlHandler(AdministratorModifyPassword);
        }

        private delegate void ChangeControl(object sender, EventArgs e);

        private void button1_Click(object sender, EventArgs e)
        {
            if (s.ServerIsRunning == false)
            {//进行启动服务操作
                s.StartServer();
                button1.Text = "关闭服务";
            }
            else
            {//进行关闭服务操作
                //ServerOperation s = new ServerOperation();
                s.CloseServer();
                button1.Text = "启动服务";
            }
        }

        private void systemMessageButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要发布吗？", "发布系统消息", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                s.ReleaseSystemMessage(systemMessageTextBox.Text.Trim());
            }
        }

        private void changePasswdButton_Click(object sender, EventArgs e)
        {
            changePasswdWarningLabel.ForeColor = Color.Red;
            if (newPasswdTextBox1.Text.Trim() == newPasswdTextBox2.Text.Trim())
            {
                if (newPasswdTextBox1.Text.Trim().Length >= 8)
                {
                    if (!newPasswdTextBox1.Text.Trim().Contains<char>('\'') && !newPasswdTextBox1.Text.Trim().Contains<char>('\"'))
                    {
                        s.ChangeAdministratorPassword(oldPasswdTextBox.Text.Trim(), newPasswdTextBox1.Text.Trim());
                    }
                    else
                    {
                        changePasswdWarningLabel.Text = "错误：密码中不能包含双引号或者单引号";
                        return;
                    }
                }
                else
                {
                    changePasswdWarningLabel.Text = "错误：新密码的长度不足八位";
                    return;
                }
            }
            else
            {
                changePasswdWarningLabel.Text = "错误：两次输入的新密码不一致";
                return;
            }
        }

        /// <summary>
        /// 以下为逻辑层修改界面层的函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OneUserLogin(object sender, EventArgs e)
        {
            if (listBox1.InvokeRequired)
            {
                ChangeControl CC = new ChangeControl(OneUserLogin);
                this.Invoke(CC, sender, e);
            }
            else
            {
                UserIDAndSocket uid = (UserIDAndSocket)e;
                onlineUserID.Add(uid.UserID);
                listBox1.Items.Clear();
                for (int i = 0; i < onlineUserID.Count; i++)
                {
                    listBox1.Items.Add("用户" + onlineUserID[i].ToString() + "在线");
                }
            }
        }
        private void AdministratorModifyPassword(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                ChangeControl CC = new ChangeControl(AdministratorModifyPassword);
                this.Invoke(CC, sender, e);
            }
            else
            {
                DataPacket temp = (DataPacket)e;
                changePasswdWarningLabel.Text = temp.Content;
                oldPasswdTextBox.Text = "";
                newPasswdTextBox1.Text = "";
                newPasswdTextBox2.Text = "";
            }
        }
    }
}
