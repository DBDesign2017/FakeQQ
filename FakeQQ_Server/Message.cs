using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeQQ_Server
{
    class Message
    {
        private string sourceUserID;
        private string targetUserID;
        private string text;
        private DateTime time;
        public Message(string text, string sourceUserID, string targetUserID, DateTime time)
        {
            this.text = text;
            this.sourceUserID = sourceUserID;
            this.targetUserID = targetUserID;
            this.time = time;
        }
        public string SourceUserID
        {
            set { sourceUserID = value; }
            get { return sourceUserID; }
        }
        public string TargetUserID
        {
            set { targetUserID = value; }
            get { return targetUserID; }
        }
        public string Text
        {
            set { text = value; }
            get { return text; }
        }
        public DateTime Time
        {
            set { time = value; }
            get { return time; }
        }
    }
}
