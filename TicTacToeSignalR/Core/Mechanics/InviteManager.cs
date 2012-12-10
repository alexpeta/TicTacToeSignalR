using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    public class InviteManager
    {
        private static ConcurrentBag<Invitation> _invitations = new ConcurrentBag<Invitation>();

        public InviteManager()
        {
        }


        public InviteStatus IsValidInvite(Invitation newInvitation)
        {
            if (HasPendingAnswers(newInvitation.From))
            {
                return new InviteStatus { Message = "You can not send another invite until you answer all your pending invites or they expire.", StatusType = InviteStatusType.Invalid };
            }

            if (HasPendingInvites(newInvitation.From))
            {
                return new InviteStatus { Message = "You can not send another invite until all your pending invites send are answered or expire.", StatusType = InviteStatusType.Invalid };
            }


            if (!_invitations.Contains(newInvitation))
            {
                newInvitation.SentDate = DateTime.Now;
                _invitations.Add(newInvitation);
                return new InviteStatus { Message = "Invite was added successfully.", StatusType = InviteStatusType.Valid };
            }
            else
            {
                Invitation first = null;
                _invitations.TryTake(out first);
                if (first != null && newInvitation.IsValidInvitation(first))
                {
                    _invitations.Add(newInvitation);
                    return new InviteStatus { Message = "Invite was added successfully.", StatusType = InviteStatusType.Valid };
                }
                else
                {
                    _invitations.Add(first);
                    return new InviteStatus { Message = "Invite already sent. You must wait 5 minutes before sending another one.", StatusType = InviteStatusType.Invalid }; ;
                }
            }
        }

        #region Validation Methods
        private bool HasPendingInvites(Player player)
        {
            if (player == null) return false;

            return _invitations.Where(inv => inv.From.Equals(player)).Any();
        }

        private bool HasPendingAnswers(Player player)
        {
            if (player == null) return false;

            return _invitations.Where(inv => inv.To.Equals(player)).Any();
        }
        #endregion


    }
}