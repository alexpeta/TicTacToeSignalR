using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR.Utility
{
    public static class CookieManager
    {
        public static string UserCookieName = ConfigurationManager.AppSettings.GetValues("ticTacToeUsername").SingleOrDefault();
        public static string AvatarCookieName = ConfigurationManager.AppSettings.GetValues("ticTacToeAvatar").SingleOrDefault();

        public static string CookieValue = string.Empty;

        public static bool CheckCookie(HttpContextBase context,string cookieName)
        {
            if (context == null) return false;

            if (string.IsNullOrEmpty(cookieName)) return false;

            if (context.Request == null) return false;

            try
            {
                if (context.Request.Cookies[cookieName] != null)
                {
                    CookieValue = context.Request.Cookies[cookieName].Value;
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

        public static void WriteCoockie(HttpContextBase context,string cookieName, string value)
        {
            if (context == null) return;

            if (context.Response == null) return;

            if (CheckCookie(context,cookieName))
            {
                context.Response.Cookies.Remove(cookieName);
            }

            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.HttpOnly = true;
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddMonths(12);

            context.Response.Cookies.Add(cookie);
        }
    }
}