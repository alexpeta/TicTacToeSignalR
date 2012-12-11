using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    public class InviteAnswer
    {
        public Guid InviteId { get; set; }
        public bool Accepted { get; set; }

        #region Constructors
        public InviteAnswer():this(string.Empty,false)
        {
        }
        public InviteAnswer(string inviteId,bool accepted)
        {
            try
            {
                InviteId = Guid.Parse(inviteId);
            }
            catch
            {
                InviteId = Guid.Empty;
            }
            Accepted = accepted;
        }
        public InviteAnswer(Guid inviteId, bool accepted)
        {
            InviteId = inviteId;
            Accepted = accepted;
        }
        #endregion
    }
}