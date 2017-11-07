//此类实现功能：服务端在接收到数据包后做出不同操作

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

namespace FakeQQ_Server
{
    class ServerOperation
    {
        private Form2 Form;
        public ServerOperation(Form2 form)
        {
            Form = form;
        }
        public bool Operate(DataPacket packet)
        {
            switch (packet.CommandNo)
            {
                case 1://客户端请求登录操作
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        dynamic content = js.Deserialize<dynamic>(packet.Content);//动态的反序列化
                        string input_ID = content["UserID"];//动态反序列化的结果必须用索引取值
                        string input_PW = content["PassWord"];
                        bool Correct = false;
                        SqlConnection conn = new SqlConnection("Data Source=C418;Initial Catalog=FakeQQ;Integrated Security=True");
                        SqlCommand cmd = new SqlCommand("select PassWord from dbo.Adminstrator where AdminstratorID='" + input_ID + "'", conn);
                        if (conn.State == ConnectionState.Closed)
                        {
                            try
                            {
                                conn.Open();
                                SqlDataReader DataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);//使用这种方式构造SqlDataReader类型的对象，能够保证在DataReader关闭后自动Close()对应的SqlConnection类型的对象
                                while (DataReader.Read())
                                {
                                    Console.WriteLine(DataReader["PassWord"].ToString());
                                    Console.WriteLine(input_PW);
                                    if (input_PW == DataReader["PassWord"].ToString().Trim())
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
                        if (Correct == true)
                        {
                            //向客户端发送登录成功信息，在在线用户表里面添加一条
                        }
                        else
                        {
                            //向客户端发送登录失败信息，（关闭socket？）
                        }
                        break;
                    }
            }
            return false;
        }
    }
}
