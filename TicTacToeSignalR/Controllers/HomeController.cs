using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using TicTacToeSignalR.Core;
using TicTacToeSignalR.Core.Enums;
using TicTacToeSignalR.Core.Mechanics;
using TicTacToeSignalR.Models;
using TicTacToeSignalR.Utility;
using TicTacToeSignalR.ViewModel;

namespace TicTacToeSignalR.Controllers
{
    public class HomeController : Controller
    {
        #region Private Members
        private IPlayerRepository _playerRepository;

        private List<string> GetAvatarList()
        {
            DirectoryInfo avatarsDir = new DirectoryInfo(Server.MapPath("~/Content/avatars/"));
            List<string> avatarsList = avatarsDir.GetFiles()
                .OrderBy(f =>
                {
                    int result = 0;
                    bool conversion = int.TryParse(f.Name.Replace("IDR_PROFILE_AVATAR_", "").Replace(".png", ""), out result);
                    return result;
                })
                .Select(f => f.Name.Replace(".png", ""))
                //.Select(f => @"\Content\avatars\"+f.Name )
                .ToList();
            return avatarsList;
        }
        #endregion

        #region Constructors
        public HomeController() : this(new PlayerRepository())
        {
        }
        public HomeController(IPlayerRepository playerRepo)
        {
            _playerRepository = playerRepo;
        }
        #endregion Constructors

        public ActionResult Index()
        {
            string cookieNick = string.Empty;
            string avatarNick = string.Empty;

            if (this.HttpContext.HasCookie(CookieManager.UserCookieName))
            {
                cookieNick = this.HttpContext.GetCookieValue(CookieManager.UserCookieName);
            }
            else
            {
                cookieNick = NicknamesHelper.GetRandomNick();
            }

            if (this.HttpContext.HasCookie(CookieManager.AvatarCookieName))
            {
                avatarNick = this.HttpContext.GetCookieValue(CookieManager.AvatarCookieName);
            }
            else
            {
                avatarNick = AvatarsHelper.GetRandomAvatar();
            }

            FormsAuthentication.SetAuthCookie(cookieNick, true);
            ProfileViewModel viewModel = new ProfileViewModel(cookieNick, avatarNick, this.GetAvatarList());

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ProfileViewModel profile)
        {
            if (string.IsNullOrEmpty(profile.Nick) || string.IsNullOrEmpty(profile.Avatar) || GameHub.NickIsInUse(profile.Nick))
            {
                profile.AvatarsList = this.GetAvatarList();
                profile.Nick = NicknamesHelper.GetRandomNick();
                profile.Avatar = AvatarsHelper.GetRandomAvatar();
                return View(profile);
            }
            else
            {
                CookieManager.WriteCookie(this.HttpContext, CookieManager.UserCookieName, profile.Nick);
                CookieManager.WriteCookie(this.HttpContext, CookieManager.AvatarCookieName, profile.Avatar);
                TempData["profileData"] = profile;
                return RedirectToAction("Index", "Game");
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
    }
}
