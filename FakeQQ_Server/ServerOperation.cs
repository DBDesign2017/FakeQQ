//此类实现：服务端在接收到数据包后做出不同操作

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
using System.Collections;

namespace FakeQQ_Server
{
    class ServerOperation
    {
        private string DataSourceName = "C418";
        private Socket server;
        bool serverIsRunning = false;
        private ArrayList onlineList = new ArrayList();

        public delegate void CrossThreadCallControlHandler(object sender, EventArgs e);
        public static event CrossThreadCallControlHandler OneUserLogin;
        public static void ToOneUserLogin(object sender, EventArgs e)
        {
            Console.WriteLine("one user login");
            OneUserLogin?.Invoke(sender, e);
        }

        //启动服务
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
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                success = false;
            }
            serverIsRunning = true;
            return success;
        }

        //关闭服务
        public bool CloseServer()
        {
            server.Close();
            for(int i=0; i<onlineList.Count; i++)
            {
                ((UserIDAndSocket)onlineList[i]).Service.Close();
            }
            serverIsRunning = false;
            return true;
        }

        //发布系统消息
        public void ReleaseSystemMessage(string message)
        {
            //构造数据包
            DataPacket packet = new DataPacket();
            packet.CommandNo = 24;
            packet.Content = message;
            packet.ComputerName = "server";
            packet.NameLength = packet.ComputerName.Length;
            packet.FromIP = IPAddress.Parse("0.0.0.0");
            packet.ToIP = IPAddress.Parse("0.0.0.0");
            //将该数据包发送给所有在线用户
            for(int i=0; i<onlineList.Count; i++)
            {
                Send(((UserIDAndSocket)onlineList[i]).Service, packet.PacketToBytes());
            }
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
            int bytes = 0;
            try
            {
                bytes = recieveData.service.EndReceive(iar);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("a client doesnt send anything");
            }
            if (bytes > 0)
            {
                DataPacket packet = new DataPacket(recieveData.buffer);
                //接下根据packet内的commandNo进行各种不同操作
                DataPacket responsePacket = new DataPacket();
                //根据接收到的数据包，产生响应的数据包
                responsePacket = Operate(packet, recieveData.service);
                //把响应的数据包发给客户端（不一定是原客户端）
                switch (responsePacket.CommandNo)
                {
                    case 1://客户端登录成功
                        {
                            //向客户端发送响应数据包
                            Send(recieveData.service, responsePacket.PacketToBytes());
                            //在 在线用户表（在线的用户----对应的Socket）上面加上这条记录
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            dynamic content = js.Deserialize<dynamic>(packet.Content.Replace("\0", ""));//动态的反序列化，不删除Content后面的结束符的话无法反序列化
                            UserIDAndSocket line = new UserIDAndSocket();
                            line.UserID = content["UserID"];
                            line.Service = recieveData.service;
                            onlineList.Add(line);
                            Console.WriteLine("userIDAndSocketList added");
                            //发布OneUserLogin事件
                            ToOneUserLogin(null, line);
                            break;
                        }
                    case 2://客户端登录失败
                        {
                            Send(recieveData.service, responsePacket.PacketToBytes());
                            break;
                        }
                    case 3://客户端注册成功
                        {
                            Send(recieveData.service, responsePacket.PacketToBytes());
                            break;
                        }
                    case 4://客户端注册失败
                        {
                            Send(recieveData.service, responsePacket.PacketToBytes());
                            break;
                        }
                    case 12://客户端添加好友失败
                        {
                            Send(recieveData.service, responsePacket.PacketToBytes());
                            break;
                        }
                    case 17://客户端下载好友列表成功
                        {
                            Console.WriteLine("a client want to download friendlist, success");
                            Send(recieveData.service, responsePacket.PacketToBytes());
                            break;
                        }
                    case 18://客户端下载好友列表失败
                        {
                            Console.WriteLine("a client want to download friendlist, fail");
                            Send(recieveData.service, responsePacket.PacketToBytes());
                            break;
                        }
                    case 19://客户端请求添加好友，该请求合法，则服务器转发该请求给被申请添加好友的用户
                        {
                            Console.WriteLine("a client want to add a friend, legal, sending...");
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            dynamic content = js.Deserialize<dynamic>(packet.Content.Replace("\0", ""));
                            string FriendID = content["FriendID"];
                            //确定这个包要通过哪个socket转发给用户（这个用户必须在线）
                            Socket targetSocket = null;
                            for(int i=0; i<onlineList.Count; i++)
                            {
                                if(FriendID == ((UserIDAndSocket)onlineList[i]).UserID)
                                {
                                    targetSocket = ((UserIDAndSocket)onlineList[i]).Service;
                                }
                            }
                            Send(targetSocket, responsePacket.PacketToBytes());
                            break;
                        }
                    case 20://收到添加好友申请用户同意了好友申请，现在向发起好友申请的客户端发送消息，使之更新好友列表
                        {
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            dynamic content = js.Deserialize<dynamic>(responsePacket.Content.Replace("\0", ""));
                            for (int i = 0; i < onlineList.Count; i++)
                            {
                                if (((UserIDAndSocket)onlineList[i]).UserID == content["UserID"])
                                {
                                    Send(((UserIDAndSocket)onlineList[i]).Service, responsePacket.PacketToBytes());
                                }
                            }
                            break;
                        }
                    case 255:
                        {
                            Send(recieveData.service, responsePacket.PacketToBytes());
                            break;
                        }
                    default:
                        break;
                }
                DataPacketManager newRecieveData = new DataPacketManager();
                newRecieveData.service = recieveData.service;
                newRecieveData.service.BeginReceive(newRecieveData.buffer, 0, DataPacketManager.MAX_SIZE, SocketFlags.None,
                    new AsyncCallback(RecieveCallback), newRecieveData);
            }
            else
            {
                try
                {
                    recieveData.service.BeginReceive(recieveData.buffer, 0, DataPacketManager.MAX_SIZE, SocketFlags.None,
                new AsyncCallback(RecieveCallback), recieveData);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("a client may has been closed");
                }
            }
        }
        public DataPacket Operate(DataPacket packet, Socket service)
        {
            DataPacket responsePacket = new DataPacket();
            responsePacket.CommandNo = 255;//表示数据包里无有效信息
            responsePacket.ComputerName = "server";
            responsePacket.NameLength = responsePacket.ComputerName.Length;
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
                            dynamic content = js.Deserialize<dynamic>(packet.Content.Replace("\0", ""));//动态的反序列化，不删除Content后面的结束符的话无法反序列化
                            input_ID = content["UserID"];//动态反序列化的结果必须用索引取值
                            input_PW = content["Password"];
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                        bool Correct = false;
                        SqlConnection conn = new SqlConnection("Data Source=" + DataSourceName + ";Initial Catalog=JinNangIM_DB;Integrated Security=True");
                        SqlCommand cmd = new SqlCommand("select Password from dbo.Users where UserID='" + input_ID + "'", conn);
                        if (conn.State == ConnectionState.Closed)
                        {
                            try
                            {
                                conn.Open();
                                SqlDataReader DataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);//使用这种方式构造SqlDataReader类型的对象，能够保证在DataReader关闭后自动Close()对应的SqlConnection类型的对象
                                while (DataReader.Read())
                                {
                                    if (input_PW == DataReader["Password"].ToString().Trim())
                                    {
                                        Correct = true;
                                    }
                                }
                                DataReader.Close();
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.ToString());
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
                        }
                        else
                        {
                            responsePacket.CommandNo = 2;
                        }
                        responsePacket.Content = input_ID;
                        break;
                    }
                case 2://客户端请求注册操作
                    {
                        //先从数据库找出所有已经已存在的UserID，构造一个尚未存在的UserID
                        int UserID;
                        ArrayList ExistID = new ArrayList(10);

                        SqlConnection selectConnect = new SqlConnection("Data Source=" + DataSourceName + ";Initial Catalog=JinNangIM_DB;Integrated Security=True");
                        SqlCommand selectCmd = new SqlCommand("select UserID from dbo.Users", selectConnect);
                        if (selectConnect.State == ConnectionState.Closed)
                        {
                            try
                            {
                                selectConnect.Open();
                                SqlDataReader DataReader = selectCmd.ExecuteReader(CommandBehavior.CloseConnection);//使用这种方式构造SqlDataReader类型的对象，能够保证在DataReader关闭后自动Close()对应的SqlConnection类型的对象
                                while (DataReader.Read())
                                {
                                    //DataReader["UserID"]返回的数据的类型和数据库存储的类型一致，此处为int32
                                    ExistID.Add(DataReader["UserID"]);
                                }
                                DataReader.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            finally
                            {
                                selectConnect.Close();
                            }
                        }
                        ExistID.Sort();//所有ID都从小到大排序了
                        UserID = (int)ExistID[ExistID.Count - 1] + 1;

                        //将构造出的新ID和packet里面的密码存储到数据库
                        string PW = "";
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        try
                        {
                            dynamic content = js.Deserialize<dynamic>(packet.Content.Replace("\0", ""));//动态的反序列化，不删除Content后面的结束符的话无法反序列化
                            PW = content["Password"];//动态反序列化的结果必须用索引取值
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        SqlConnection insertConnect = new SqlConnection("Data Source=" + DataSourceName + ";Initial Catalog=JinNangIM_DB;Integrated Security=True");
                        SqlCommand insertCmd = new SqlCommand("insert into dbo.Users values('" + UserID.ToString() + "', null, '" + PW + "', null, null, null, null, null, null, null, null, null)", insertConnect);
                        bool registerSuccess = true;
                        if (insertConnect.State == ConnectionState.Closed)/*有问题，但是没出错*/
                        {
                            try
                            {
                                insertConnect.Open();
                                insertCmd.ExecuteNonQuery();//使用这种方式构造SqlDataReader类型的对象，能够保证在DataReader关闭后自动Close()对应的SqlConnection类型的对象
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                                registerSuccess = false;
                            }
                            finally
                            {
                                insertConnect.Close();
                            }
                        }
                        //构造要发送的数据包
                        if (registerSuccess == true)
                        {
                            responsePacket.CommandNo = 3;
                        }
                        else
                        {
                            responsePacket.CommandNo = 4;
                        }
                        responsePacket.Content = UserID.ToString();
                        break;
                    }
                case 6://客户端请求添加好友操作
                    {
                        Console.WriteLine("operate case 6");
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        dynamic content = js.Deserialize<dynamic>(packet.Content.Replace("\0", ""));
                        string FriendID = content["FriendID"];
                        string UserID = content["UserID"];
                        //在数据库中搜索FriendID，判断UserID是否已经加FriendID为好友，若是，则添加好友失败，只构造一个返回给UserID的包。
                        bool isFriendAlready = false;
                        SqlConnection conn = new SqlConnection("Data Source=" + DataSourceName + ";Initial Catalog=JinNangIM_DB;Integrated Security=True");
                        SqlCommand cmd = new SqlCommand("select FriendID from dbo.Friends where ID='" + UserID + "'", conn);
                        if (conn.State == ConnectionState.Closed)
                        {
                            try
                            {
                                conn.Open();
                                SqlDataReader DataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);//使用这种方式构造SqlDataReader类型的对象，能够保证在DataReader关闭后自动Close()对应的SqlConnection类型的对象
                                while (DataReader.Read())
                                {
                                    if (FriendID == DataReader["FriendID"].ToString().Trim())
                                    {
                                        isFriendAlready = true;
                                    }
                                }
                                DataReader.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                        if (isFriendAlready)
                        {
                            responsePacket.CommandNo = 12;
                            responsePacket.Content = "错误：你和该用户已经是好友了";
                            break;
                        }
                        //即使UserID和FriendID还不是好友，如果User表中不存在FriendID，则添加好友也失败，只构造一个返回给UserID的包。
                        bool friendIDExist = false;
                        SqlConnection friendIDExistConnect = new SqlConnection("Data Source=" + DataSourceName + ";Initial Catalog=JinNangIM_DB;Integrated Security=True");
                        SqlCommand friendIDExistCmd = new SqlCommand("select UserID from dbo.Users where UserID='" + FriendID + "'", friendIDExistConnect);
                        if (friendIDExistConnect.State == ConnectionState.Closed)
                        {
                            try
                            {
                                friendIDExistConnect.Open();
                                SqlDataReader DataReader = friendIDExistCmd.ExecuteReader(CommandBehavior.CloseConnection);//使用这种方式构造SqlDataReader类型的对象，能够保证在DataReader关闭后自动Close()对应的SqlConnection类型的对象
                                while (DataReader.Read())
                                {
                                    if (FriendID == DataReader["UserID"].ToString().Trim())
                                    {
                                        friendIDExist = true;
                                    }
                                }
                                DataReader.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            finally
                            {
                                friendIDExistConnect.Close();
                            }
                        }
                        if (!friendIDExist)
                        {
                            responsePacket.CommandNo = 12;
                            responsePacket.Content = "错误：不存在这样的用户";
                            break;
                        }
                        //即使允许加FriendID为好友，若FriendID不在线，则加好友失败。只构造一个返回给UserID的包。
                        bool friendIDIsOnline = false;
                        for (int i=0; i<onlineList.Count; i++)
                        {
                            if (((UserIDAndSocket)onlineList[i]).UserID == FriendID) { friendIDIsOnline = true; }
                        }
                        if(friendIDIsOnline == false)
                        {
                            responsePacket.CommandNo = 12;
                            responsePacket.Content = "错误：该用户现在不在线";
                            Console.WriteLine("FriendID不在线");
                            break;
                        }
                        //若FriendID在线，而且UserID可以加FriendID为好友，则构造一个发给FriendID的数据包，内容是UserID的请求信息。
                        responsePacket.CommandNo = 19;
                        responsePacket.Content = packet.Content;
                        break;
                    }
                case 9://客户端请求下载好友列表
                    {
                        Console.WriteLine("operate case 9");
                        //构造返回的数据包
                        responsePacket.ComputerName = "server";
                        responsePacket.NameLength = responsePacket.ComputerName.Length;
                        responsePacket.FromIP = IPAddress.Parse("0.0.0.0");
                        responsePacket.ToIP = IPAddress.Parse("0.0.0.0");
                        //在数据库的Friends表中搜索，得到这个用户的所有好友的ID
                        string UserID = packet.Content.Replace("\0", "");
                        ArrayList friendList = new ArrayList();
                        bool success = false;
                        try
                        {
                            SqlConnection selectConnect = new SqlConnection("Data Source=" + DataSourceName + ";Initial Catalog=JinNangIM_DB;Integrated Security=True");
                            SqlCommand selectCmd = new SqlCommand("select FriendID from dbo.Friends where ID='" + UserID + "'", selectConnect);
                            if (selectConnect.State == ConnectionState.Closed)
                            {
                                try
                                {
                                    selectConnect.Open();
                                    SqlDataReader DataReader = selectCmd.ExecuteReader(CommandBehavior.CloseConnection);//使用这种方式构造SqlDataReader类型的对象，能够保证在DataReader关闭后自动Close()对应的SqlConnection类型的对象
                                    while (DataReader.Read())
                                    {
                                        FriendListItem item = new FriendListItem();
                                        item.UserID = DataReader["FriendID"].ToString();
                                        item.IsOnline = false;
                                        for(int i=0; i<onlineList.Count; i++)
                                        {
                                            if(((UserIDAndSocket)onlineList[i]).UserID == item.UserID)
                                            {
                                                item.IsOnline = true;
                                                break;
                                            }
                                        }
                                        friendList.Add(item);
                                    }
                                    DataReader.Close();
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine(e.ToString());
                                }
                                finally
                                {
                                    selectConnect.Close();
                                }
                            }
                            success = true;
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        //处理返回的数据包的Content和CommandNO部分
                        if(success == true)
                        {
                            responsePacket.CommandNo = 17;
                        }
                        else
                        {
                            responsePacket.CommandNo = 18;
                        }
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        responsePacket.Content = js.Serialize(friendList);
                        break;
                    }
                case 10://收到添加好友申请用户同意了好友申请
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        dynamic content = js.Deserialize<dynamic>(packet.Content.Replace("\0", ""));
                        string UserID = content["UserID"];
                        string FriendID = content["FriendID"];
                        //添加好友关系到数据库（有两行：a和b是好友、b和a是好友）
                        SqlConnection insertConnect = new SqlConnection("Data Source=" + DataSourceName + ";Initial Catalog=JinNangIM_DB;Integrated Security=True");
                        SqlCommand insertCmd = new SqlCommand("insert into dbo.Friends values('" + UserID + "', '" + FriendID + "')", insertConnect);
                        SqlCommand reverseInsertCmd = new SqlCommand("insert into dbo.Friends values('" + FriendID + "', '" + UserID + "')", insertConnect);
                        if (insertConnect.State == ConnectionState.Closed)
                        {
                            try
                            {
                                insertConnect.Open();
                                insertCmd.ExecuteNonQuery();//使用这种方式构造SqlDataReader类型的对象，能够保证在DataReader关闭后自动Close()对应的SqlConnection类型的对象
                                reverseInsertCmd.ExecuteNonQuery();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            finally
                            {
                                insertConnect.Close();
                            }
                        }
                        //发包提示好友申请发起者，别人已经同意添加好友
                        //...
                        responsePacket.CommandNo = 20;
                        responsePacket.Content = packet.Content;
                        //...
                        break;
                    }
                case 255://客户端启动，请求连接服务端
                    {
                        responsePacket.ComputerName = "server";
                        responsePacket.NameLength = responsePacket.ComputerName.Length;
                        responsePacket.FromIP = IPAddress.Parse("0.0.0.0");
                        responsePacket.ToIP = IPAddress.Parse("0.0.0.0");
                        responsePacket.CommandNo = 255;
                        responsePacket.Content = "";
                        break;
                    }
                default:
                    break;
            }
            return responsePacket;
        }
        private void Send(Socket handler, byte[] buffer)
        {
            handler.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(SendCallback), handler);
        }
        private void SendCallback(IAsyncResult iar)
        {
            try
            {
                //重新获取socket
                Socket handler = (Socket)iar.AsyncState;
                /*service.BeginReceive(recieveData.buffer, 0, DataPacketManager.MAX_SIZE, SocketFlags.None,
                new AsyncCallback(RecieveCallback), recieveData);*/
                //完成发送字节数组动作
                int bytesSent = handler.EndSend(iar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public bool ServerIsRunning
        {
            get{ return serverIsRunning; }
        }
    }
}
