using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSetter.Models
{
    public class webContent
    {
        public string messageId { get; set; }
        public string businessNumber { get; set; }
        public string time { get; set; }
        public string sessionId { get; set; }
        public string msisdn { get; set; }
        public string operatorCode { get; set; }
        public string keyword { get; set; }
        public string command { get; set; }
        public string content { get; set; }
    }
    public class notification
    {
        public string messageStatus { get; set; }
        public string messageStatusText { get; set; }
        public string messageId { get; set; }
        public string messageRef { get; set; }
        public string businessNumber { get; set; }
        public string time { get; set; }
        public string sessionId { get; set; }
        public string msisdn { get; set; }
        public string command { get; set; }

        public string toString()
        {
            return "notification[" + messageStatus + "," + messageStatusText + "," + messageId + "," + messageRef + "," + businessNumber + "," + time + "," + sessionId + "," +  msisdn + "," + command + "]";
        }

    }
}