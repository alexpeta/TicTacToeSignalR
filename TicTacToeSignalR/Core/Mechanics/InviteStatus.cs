using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    public class InviteStatus
    {
        public InviteStatusType StatusType { get; set; }
        public string Message { get; set; }

        public InviteStatus() : this(InviteStatusType.Valid,string.Empty)
        { 
        }
        public InviteStatus(InviteStatusType type,string message)
        {
            StatusType = type;
            Message = message;
        }
    }
}