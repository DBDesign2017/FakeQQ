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
        public Form2()
        {
            InitializeComponent();
            s = new ServerOperation();
            ServerOperation.OneUserLogin += new ServerOperation.CrossThreadCallControlHandler(OneUserLogin);
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
                ServerOperation s = new ServerOperation();
                s.CloseServer();
                button1.Text = "启动服务";
            }
        }

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
                for(int i=0; i<onlineUserID.Count; i++)
                {
                    listBox1.Items.Add("用户" + onlineUserID[i].ToString() + "在线");
                }
            }
        }

        private void systemMessageButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要发布吗？", "发布系统消息", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                s.ReleaseSystemMessage(systemMessageTextBox.Text.Trim());
            }
        }
    }
}
