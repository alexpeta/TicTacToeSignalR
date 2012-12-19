using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR.Core.Enums
{
    public static class AvatarsHelper
    {
        private static List<string> Values = new List<string>()
        {
            "IDR_PROFILE_AVATAR_0",
            "IDR_PROFILE_AVATAR_1",
            "IDR_PROFILE_AVATAR_2",
            "IDR_PROFILE_AVATAR_3",
            "IDR_PROFILE_AVATAR_4",
            "IDR_PROFILE_AVATAR_5",
            "IDR_PROFILE_AVATAR_6",
            "IDR_PROFILE_AVATAR_7",
            "IDR_PROFILE_AVATAR_8",
            "IDR_PROFILE_AVATAR_9",
            "IDR_PROFILE_AVATAR_10",
            "IDR_PROFILE_AVATAR_11",
            "IDR_PROFILE_AVATAR_12",
            "IDR_PROFILE_AVATAR_13",
            "IDR_PROFILE_AVATAR_14",
            "IDR_PROFILE_AVATAR_15",
            "IDR_PROFILE_AVATAR_16",
            "IDR_PROFILE_AVATAR_17",
            "IDR_PROFILE_AVATAR_18",
            "IDR_PROFILE_AVATAR_19",
            "IDR_PROFILE_AVATAR_20",
            "IDR_PROFILE_AVATAR_21",
            "IDR_PROFILE_AVATAR_22",
            "IDR_PROFILE_AVATAR_23",
            "IDR_PROFILE_AVATAR_24",
            "IDR_PROFILE_AVATAR_25"
        };

        public static string GetRandomAvatar()
        {
            Random r = new Random();
            return Values[r.Next(0, Values.Count - 1)];
        }
    }
}