﻿//此类实现：服务端在接收到数据包后做出不同操作

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Sockets;

namespace FakeQQ_Server
{
    class ServerOperation
    {
        private Form2 Form;
        private Socket server;
        public ServerOperation(Form2 form)
        {
            Form = form;
        }
        public bool StartServer()
        {
            bool success = false;
            try
            {
                //定义IP地址
                IPAddress local = IPAddress.Parse("127.0.0.1");
                int port = 8500;
                IPEndPoint iep = new IPEndPoint(local, port);
                //创建服务器的socket对象
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(iep);
                server.Listen(20);
                server.BeginAccept(new AsyncCallback(AcceptCallback), server);
                success = true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                success = false;
            }
            return success;
        }
        public bool CloseServer()
        {
            server.Close();
            return true;
        }
        private void AcceptCallback(IAsyncResult iar)
        {
            //还原传入的原始套接字
            Socket server = iar.AsyncState as Socket;
            //在原始套接字上调用EndAccept方法，返回新套接字
            Socket service = server.EndAccept(iar);
            DataPacketManager recieveData = new DataPacketManager();
            recieveData.service = service;
            service.BeginReceive(recieveData.buffer, 0, DataPacketManager.MAX_SIZE, SocketFlags.None,
                new AsyncCallback(RecieveCallback), recieveData);
            server.BeginAccept(new AsyncCallback(AcceptCallback), server);//重新开始监听
        }
        private void RecieveCallback(IAsyncResult iar)
        {
            DataPacketManager recieveData = iar.AsyncState as DataPacketManager;
            int bytes = recieveData.service.EndReceive(iar);
            if (bytes > 0)
            {
                DataPacket packet = new DataPacket(recieveData.buffer);
                //接下根据packet内的commandNo进行各种不同操作
                DataPacket responsePacket = new DataPacket();
                //根据接收到的数据包，产生响应的数据包
                responsePacket = Operate(packet);
                //把响应的数据包发给客户端（不一定是原客户端）
                switch (responsePacket.CommandNo)
                {
                    case 1:
                        {
                            Send(recieveData.service, responsePacket.PacketToBytes());
                            break;
                        }
                    case 2:
                        {
                            Send(recieveData.service, responsePacket.PacketToBytes());
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                recieveData.service.BeginReceive(recieveData.buffer, 0, DataPacketManager.MAX_SIZE, SocketFlags.None,
                new AsyncCallback(RecieveCallback), recieveData);
            }
        }
        private static void Send(Socket handler, byte[] buffer)
        {
            handler.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(SendCallback), handler);
        }
        private static void SendCallback(IAsyncResult iar)
        {
            try
            {
                //重新获取socket
                Socket handler = (Socket)iar.AsyncState;
                //完成发送字节数组动作
                int bytesSent = handler.EndSend(iar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public DataPacket Operate(DataPacket packet)
        {
            DataPacket responsePacket = new DataPacket();
            responsePacket.CommandNo = 255;//表示数据包里无有效信息
            responsePacket.ComputerName = "";
            responsePacket.NameLength = 0;
            responsePacket.FromIP = IPAddress.Parse("0.0.0.0");
            responsePacket.ToIP = IPAddress.Parse("0.0.0.0");
            responsePacket.Content = "";
            switch (packet.CommandNo)
            {
                case 1://客户端请求登录操作
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string input_ID = "null";
                        string input_PW = "null";
                        try
                        {
                            dynamic content = js.Deserialize<dynamic>(packet.Content.Replace("\0",""));//动态的反序列化，不删除Content后面的结束符的话无法反序列化
                            input_ID = content["UserID"];//动态反序列化的结果必须用索引取值
                            input_PW = content["PassWord"];
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        
                        bool Correct = false;
                        SqlConnection conn = new SqlConnection("Data Source=C418;Initial Catalog=JinNangIM_DB;Integrated Security=True");
                        SqlCommand cmd = new SqlCommand("select Password from dbo.Users where UserID='" + input_ID + "'", conn);
                        if (conn.State == ConnectionState.Closed)
                        {
                            try
                            {
                                conn.Open();
                                SqlDataReader DataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);//使用这种方式构造SqlDataReader类型的对象，能够保证在DataReader关闭后自动Close()对应的SqlConnection类型的对象
                                while (DataReader.Read())
                                {
                                    Console.WriteLine(DataReader["Password"].ToString());
                                    Console.WriteLine(input_PW);
                                    if (input_PW == DataReader["Password"].ToString().Trim())
                                    {
                                        Correct = true;
                                    }
                                }
                                DataReader.Close();
                            }
                            catch
                            {
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                        //构造要向客户端发送的数据包
                        if (Correct == true)
                        {
                            responsePacket.CommandNo = 1;
                            Console.WriteLine("a user login successful");
                        }
                        else
                        {
                            responsePacket.CommandNo = 2;
                            Console.WriteLine("a user login failed");
                        }
                        responsePacket.ToIP = packet.FromIP;
                        responsePacket.FromIP = packet.ToIP;
                        responsePacket.ComputerName = "Server";
                        responsePacket.NameLength = responsePacket.ComputerName.Length;
                        responsePacket.Content = "";
                        break;
                    }
                default:
                    break;
            }
            return responsePacket;
        }
    }
}