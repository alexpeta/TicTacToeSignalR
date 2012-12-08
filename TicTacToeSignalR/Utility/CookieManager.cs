using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR.Utility
{
    public static class CookieManager
    {
        private static string _cookieName = ConfigurationManager.AppSettings.GetValues("ticTacToeUsername").SingleOrDefault();
        private static string _cookieValue = string.Empty;

        #region Public properties
        public static string CookieValue
        {
            get { return CookieManager._cookieValue; }
            set { CookieManager._cookieValue = value; }
        }
        public static string CookieName
        {
            get { return _cookieName; }
            set { _cookieName = value; }
        }
        #endregion

        public static bool CheckUserCookie(HttpContextBase context)
        {
            if (context == null) return false;

            if (context.Request == null) return false;

            try
            {
                if (context.Request.Cookies[_cookieName] != null)
                {
                    _cookieValue = context.Request.Cookies[_cookieName].Value;
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

        public static void WriteUserCoockie(HttpContextBase context, string username)
        {
            if (context == null) return;

            if (context.Response == null) return;

            if (CheckUserCookie(context))
            {
                context.Response.Cookies.Remove(_cookieName);
            }

            HttpCookie cookie = new HttpCookie(_cookieName);
            cookie.Value = username;
            cookie.Expires = DateTime.Now.AddMonths(12);

            context.Response.Cookies.Add(cookie);
        }
    }
}