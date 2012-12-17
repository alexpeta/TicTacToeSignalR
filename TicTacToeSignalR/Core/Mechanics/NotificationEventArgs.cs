using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    public class NotificationEventArgs<T> : EventArgs 
        where T : class
    {
        private string _clientId;
        private string _message;
        private T _value;

        public string ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }
        
        public NotificationEventArgs(): this(string.Empty,string.Empty, default(T))
        {
        }
        public NotificationEventArgs(string clientId, string message, T value)
        {
            ClientId = clientId;
            Message = message;
            Value = value;
        }


        private static NotificationEventArgs<Movement> _nullNotificationEventArgs = new NotificationEventArgs<Movement>();
        public static NotificationEventArgs<Movement> Null
        {
            get
            {
                return _nullNotificationEventArgs;
            }
        }
    }
}