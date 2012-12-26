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
                CookieManager.WriteCookie(this.HttpContext, CookieManager.UserCookieName, newProfile.Nick);
                CookieManager.WriteCookie(this.HttpContext, CookieManager.AvatarCookieName, newProfile.Avatar);
                return View(newProfile);
                //if (this.HttpContext.HasCookie(CookieManager.UserCookieName))
                //{
                //    ProfileViewModel vm = new ProfileViewModel();
                //    vm.Nick = this.HttpContext.GetCookieValue(CookieManager.UserCookieName);

                //    if (this.HttpContext.HasCookie(CookieManager.AvatarCookieName))
                //    {   
                //        vm.Avatar = this.HttpContext.GetCookieValue(CookieManager.AvatarCookieName);
                //        return View(vm);
                //    }
                //    else
                //    {
                //        vm.Avatar = AvatarsHelper.GetRandomAvatar();
                //        return View(vm);
                //    }
                //}
                //else
                //{
                //    //redirect to index page
                //    return RedirectToAction("Index", "Home", null); 
                //}
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
