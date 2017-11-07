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
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //定义IP地址
            IPAddress local = IPAddress.Parse("127.0.0.1");
            IPEndPoint iep = new IPEndPoint(local, 13000);
            //创建服务器的socket对象
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(iep);
            server.Listen(20);
            server.BeginAccept(new AsyncCallback(Accept), server);
        }

        private void Accept(IAsyncResult iar)
        {
            //还原传入的原始套接字
            Socket server = iar.AsyncState as Socket;
            //在原始套接字上调用EndAccept方法，返回新套接字
            Socket service = server.EndAccept(iar);
            DataPacketManager recieveData = new DataPacketManager();
            recieveData.service = service;
            service.BeginReceive(recieveData.buffer, 0, DataPacketManager.MAX_SIZE, SocketFlags.None,
                new AsyncCallback(Recieve), recieveData);
            server.BeginAccept(new AsyncCallback(Accept), server);
        }

        private void Recieve(IAsyncResult iar)
        {
            DataPacketManager recieveData = iar.AsyncState as DataPacketManager;
            int bytes = recieveData.service.EndReceive(iar);
            if(bytes > 0)
            {
                DataPacket packet = new DataPacket(recieveData.buffer);
                //接下根据packet内的commandNo进行各种不同操作
                ServerOperation Operation = new ServerOperation(this);
                Operation.Operate(packet);
            }
            else
            {
                recieveData.service.BeginReceive(recieveData.buffer, 0, DataPacketManager.MAX_SIZE, SocketFlags.None,
                new AsyncCallback(Recieve), recieveData);
            }
        }
    }
}
