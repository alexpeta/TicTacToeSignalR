using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using TicTacToeSignalR.Core;
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

            if (CookieManager.CheckCookie(this.HttpContext,CookieManager.UserCookieName))
            {
                cookieNick = CookieManager.CookieValue;
            }

            if (CookieManager.CheckCookie(this.HttpContext, CookieManager.AvatarCookieName))
            {
                avatarNick = CookieManager.CookieValue;
            }


            DirectoryInfo avatarsDir = new DirectoryInfo(Server.MapPath("~/Content/avatars/"));
            List<string> avatarsList = avatarsDir.GetFiles()
                .OrderBy(f => 
                    {
                        int result = 0;
                        bool conversion = int.TryParse(f.Name.Replace("IDR_PROFILE_AVATAR_", "").Replace(".png", ""),out result);
                        return result;
                    })
                .Select(f => f.Name)
                //.Select(f => @"\Content\avatars\"+f.Name )
                .ToList();
            ProfileViewModel viewModel = new ProfileViewModel(cookieNick, avatarNick, avatarsList);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ProfileViewModel profile)
        {
            if (string.IsNullOrEmpty(profile.Nick) || string.IsNullOrEmpty(profile.Avatar))
            {
                return View();
            }
            else
            {
                //TODO : check if nick is already taken
                CookieManager.WriteCoockie(this.HttpContext, CookieManager.UserCookieName, profile.Nick);
                CookieManager.WriteCoockie(this.HttpContext, CookieManager.AvatarCookieName, profile.Avatar);
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
