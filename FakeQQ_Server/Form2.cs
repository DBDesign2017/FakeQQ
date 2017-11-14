using System;
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
        bool ServerIsRunning = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            if (ServerIsRunning == false)
            {//进行启动服务操作
                ServerOperation s = new ServerOperation(form);
                s.StartServer();
                button1.Text = "关闭服务";
                ServerIsRunning = true;
            }
            else
            {//进行关闭服务操作
                ServerOperation s = new ServerOperation(form);
                s.CloseServer();
                button1.Text = "启动服务";
                ServerIsRunning = false;
            }
        }
    }
}
