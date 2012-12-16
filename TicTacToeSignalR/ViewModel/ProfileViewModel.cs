using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR.ViewModel
{
    public class ProfileViewModel
    {
        private List<string> _avatarsList = new List<string>();
        private string _nick;
        private string _avatar;

        public List<string> AvatarsList
        {
            get { return _avatarsList; }
            set { _avatarsList = value; }
        }

        public string Nick
        {
            get { return _nick; }
            set { _nick = value; }
        }

        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }


        #region Constructors
        public ProfileViewModel(): this(string.Empty,string.Empty,new List<string>())
        {
        }
        public ProfileViewModel(string nick,string avatar, List<string> avatarList)
        {
            _avatarsList = avatarList;
            Avatar = avatar;
            Nick = nick;
        }
        #endregion


        #region NullObject Pattern
        private static readonly ProfileViewModel _nullProfileViewModel = new ProfileViewModel(string.Empty,string.Empty,new List<string>());

        public static ProfileViewModel Null
        {
            get { return ProfileViewModel._nullProfileViewModel; }
        }
        #endregion





    }
}