using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicTacToeSignalR.Core.Enums;

namespace TicTacToeSignalR.Core
{
    public class UserNotification
    {
        private string _message;
        private string _type;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        #region Constructors
        public UserNotification():this(string.Empty,string.Empty)
        {   
        }
        public UserNotification(string message, string type)
        {
            Message = message;
            Type = type;
        }
        #endregion

    }
}