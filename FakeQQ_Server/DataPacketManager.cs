using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FakeQQ_Server
{
    class DataPacketManager:EventArgs
    {
        public Socket service = null;
        public const int MAX_SIZE = 8096;
        public byte[] buffer = new byte[MAX_SIZE];
    }
}
