using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR.Utility
{
    public static class Extensions
    {
        public static bool HasCookie(this HttpContextBase helper, string cookieName)
        {
            if (string.IsNullOrEmpty(cookieName))
            {
                return false;
            }

            try
            {
                if (helper.Request.Cookies[cookieName] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static string GetCookieValue(this HttpContextBase helper, string cookieName)
        {
            if (helper.HasCookie(cookieName))
            {
                return helper.Request.Cookies[cookieName].Value;
            }

            return string.Empty;
        }
            
    }
}