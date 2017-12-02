using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FakeQQ_Server
{
    public partial class Form1 : Form
    {
        private string DataSourceName = "C418";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input_ID = textBox1.Text.Trim();
            string input_PW = textBox2.Text.Trim();
            bool Correct = false;
            SqlConnection conn = new SqlConnection("Data Source=" + DataSourceName + ";Initial Catalog=JinNangIM_DB;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select Password from dbo.Administrator where AdministratorID='" + input_ID + "'", conn);
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
                    MessageBox.Show("错误！连接或查询数据库时出错");
                    this.Close();
                }
                finally
                {
                    conn.Close();
                }
            }
            if (Correct == true)
            {
                this.Close();
                new System.Threading.Thread(() =>
                {
                    Application.Run(new Form2());
                }).Start();
            }
            else
            {
                MessageBox.Show("用户名或密码错误！");
            }
        }
    }
}
