using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    public static class InviteManager
    {
        private static ConcurrentBag<Invitation> _invitations = new ConcurrentBag<Invitation>();

        static InviteManager()
        {
        }

        public static Invitation GetInvitationByInvitationId(Guid invitationId)
        {
            if (invitationId == Guid.Empty) return null;

            //deep copy
            Invitation invitation = new Invitation(_invitations.Where(i => i.InviteId == invitationId).FirstOrDefault());

            return invitation;
        }

        public static InviteStatus ValidateAnswer(InviteAnswer answer)
        {
            if(answer == null) 
            {
               return new InviteStatus { Message = string.Format("Invalid invitation!"), StatusType = InviteStatusType.Invalid };
            }


            Invitation invitation = _invitations.SingleOrDefault(i => i.InviteId == answer.InviteId);
            if (invitation == null)
            {
                return new InviteStatus { Message = string.Format("Error getting getting invitation from in-memory list."), StatusType = InviteStatusType.Invalid };
            }
            
            //bool invDeletedFromMemory = _invitations.TryTake(out invitation);
            if (!answer.Accepted)
            {
                return new InviteStatus { Message = string.Format("{0} rejected your invitation.", invitation.To.Nick), StatusType = InviteStatusType.Rejected };
            }
            else
            {
                return new InviteStatus { Message = string.Format("{0} accepted!", invitation.To.Nick), StatusType = InviteStatusType.Accepted };
            }
        }

        public static Invitation ExtractInvite(Guid inviteId)
        {
            Invitation result = null;
            if (inviteId != Guid.Empty)
            {
                result = _invitations.Where(i => i.InviteId == inviteId).SingleOrDefault();
                if (result != null)
                {
                    _invitations.TryTake(out result);                  
                }
            }
            return result;
        }

        public static InviteStatus IsValidInvite(Invitation newInvitation)
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
        private static bool HasPendingInvites(Player player)
        {
            if (player == null) return false;

            return _invitations.Any(inv => inv.From.Equals(player));
        }

        private static bool HasPendingAnswers(Player player)
        {
            if (player == null) return false;

            return _invitations.Any(inv => inv.To.Equals(player));
        }
        #endregion


    }
}