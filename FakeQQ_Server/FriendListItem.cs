using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeQQ_Server
{
    class FriendListItem
    {
        private string userID;
        private bool isOnline;
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public bool IsOnline
        {
            get { return isOnline; }
            set { isOnline = value; }
        }
    }
}
