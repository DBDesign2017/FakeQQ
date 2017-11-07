using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FakeQQ_Server
{
    class DataPacket
    {
        //数据包的详细信息
        private byte commandNo;//命令号，1字节
        private IPAddress fromIP = null;//发送端IP，4字节
        private int nameLength;//用户名的长度，4字节
        private string computerName = null;//用户名
        private IPAddress toIP = null;//接收端IP，4字节
        private string content = null;//内容

        //构造函数
        public DataPacket() { }
        public DataPacket(byte[] bytes)//构造函数，解析数据包中详细信息
        {
            this.commandNo = bytes[0];
            byte[] ipb = new byte[4];
            Array.Copy(bytes, 1, ipb, 0, 4);
            this.fromIP = IPAddress.Parse(ipByteToString(ipb));
            this.nameLength = BitConverter.ToInt32(bytes, 5);
            this.computerName = Encoding.UTF8.GetString(bytes, 9, nameLength);
            Array.Copy(bytes, 9 + nameLength, ipb, 0, 4);
            this.toIP = IPAddress.Parse(ipByteToString(ipb));
            this.content = Encoding.UTF8.GetString(bytes, 13 + nameLength, bytes.Length - 13 - nameLength);
        }

        //将详细信息转换成字节数组类型数据包
        public byte[] PacketToBytes()
        {
            byte[] commandb = new byte[1];
            commandb[0] = commandNo;//将命令号转换为byte数组
            byte[] fromipb = fromIP.GetAddressBytes();//将发送端IP转换为byte数组
            byte[] nameb = Encoding.UTF8.GetBytes(/*computerName*/"");//将计算机名转换为byte数组
            byte[] lengthb = BitConverter.GetBytes(nameb.Length);//将计算机名的长度转换为byte数组
            byte[] toipb = toIP.GetAddressBytes();//将接收端IP转换为byte数组
            byte[] contentb = Encoding.UTF8.GetBytes(content);//将内容转换为byte数组
            byte[] buffer = new byte[13 + nameb.Length + contentb.Length];
            int index = 0;
            commandb.CopyTo(buffer, index);
            index += commandb.Length;
            fromipb.CopyTo(buffer, index);
            index += fromipb.Length;
            lengthb.CopyTo(buffer, index);
            index += lengthb.Length;
            nameb.CopyTo(buffer, index);
            index += nameb.Length;
            toipb.CopyTo(buffer, index);
            index += toipb.Length;
            contentb.CopyTo(buffer, index);
            return buffer;
        }

        //将byte[]表示的IP地址转换成IPAddress类型
        private string ipByteToString(byte[] ipbt)
        {
            string strip = string.Empty;
            strip = (int)ipbt[0] + "." + (int)ipbt[1] + "." + (int)ipbt[2] + "." + (int)ipbt[3];
            return strip;
        }

        //定义属性
        public byte CommandNo
        {
            get { return commandNo; }
            set { commandNo = value; }
        }
        public IPAddress FromIP
        {
            get { return fromIP; }
            set { fromIP = value; }
        }
        public int NameLength
        {
            get { return nameLength; }
            set { nameLength = value; }
        }
        public string ComputerName
        {
            get { return computerName; }
            set { computerName = value; }
        }
        public IPAddress ToIP
        {
            get { return toIP; }
            set { toIP = value; }
        }
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
