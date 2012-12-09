using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR
{
    public class Invitation : IEquatable<Invitation>
    {
        public readonly int MinutesToExpire = 5;

        //public Guid InviteId { get; set; }
        public Player From { get; set; }
        public Player To { get; set; }
        public DateTime SentDate { get; set; }

        public bool Equals(Invitation other)
        {
            if (other == null) return false;

            return this.From.Equals(other.From) &&
                this.To.Equals(other.To);
        }

        //public override bool Equals(object obj)
        //{

        //    Invitation temp = obj as Invitation;
        //    if ((object)temp == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return this.From == temp.From &&
        //           this.To == temp.To ;
        //    }

        //}

        //public static bool operator ==(Invitation lhs, Invitation rhs)
        //{
        //    if (System.Object.ReferenceEquals(lhs, rhs))
        //    {
        //        return true;
        //    }

        //    if (lhs == null || rhs == null)
        //    {
        //        return false;
        //    }

        //    return lhs.From == rhs.From &&
        //        lhs.To == rhs.To &&
        //        lhs.Accepted == rhs.Accepted &&
        //        lhs.SentDate == rhs.SentDate;
        //}

        //public static bool operator !=(Invitation lhs, Invitation rhs)
        //{
        //    return !(lhs == rhs);
        //}

        //TODO : override get hash code

        public bool IsValidInvitation(Invitation olderInvitation)
        {
            if (olderInvitation == null) return false;

            if (this.SentDate <= olderInvitation.SentDate ) return false;

            return (this.SentDate - olderInvitation.SentDate) > TimeSpan.FromMinutes(MinutesToExpire);
        }

    }
}