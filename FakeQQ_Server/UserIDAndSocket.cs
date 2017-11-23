using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FakeQQ_Server
{
    class UserIDAndSocket : EventArgs
    {
        private string userID;
        private Socket service;

        public string UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }
        public Socket Service
        {
            get
            {
                return service;
            }
            set
            {
                service = value;
            }
        }
    }
}
