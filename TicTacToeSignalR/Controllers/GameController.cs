using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicTacToeSignalR.Core.Enums;
using TicTacToeSignalR.Core.Mechanics;
using TicTacToeSignalR.Models;
using TicTacToeSignalR.Utility;
using TicTacToeSignalR.ViewModel;


namespace TicTacToeSignalR.Controllers
{
    public class GameController : Controller
    {
        #region Constructors
        public GameController()
        {
        }
        #endregion

        // GET: /Game/
        public ActionResult Index(int? id)
        {
            ProfileViewModel profile = TempData["profileData"] as ProfileViewModel;
            if (profile == null)
            {
                ProfileViewModel newProfile = new ProfileViewModel();
                newProfile.Nick = NicknamesHelper.GetRandomNick();
                newProfile.Avatar = AvatarsHelper.GetRandomAvatar();
                CookieManager.WriteCoockie(this.HttpContext, CookieManager.UserCookieName, newProfile.Nick);
                CookieManager.WriteCoockie(this.HttpContext, CookieManager.AvatarCookieName, newProfile.Avatar);
                return View(newProfile);
            }
            return View(profile);
        }

        [HttpPost]
        public string Index()
        {
            return "POST not allowed";
        }

    }
}
