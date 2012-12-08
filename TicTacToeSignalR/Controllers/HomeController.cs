using System.Web.Mvc;
using TicTacToeSignalR.Core;
using TicTacToeSignalR.Core.Mechanics;
using TicTacToeSignalR.Models;
using TicTacToeSignalR.Utility;

namespace TicTacToeSignalR.Controllers
{
    public class HomeController : Controller
    {
        #region Private Members
        private IPlayerRepository _playerRepository;
        private GameContext _gameContext;
        #endregion

        #region Constructors
        public HomeController() : this(new PlayerRepository())
        {
            _gameContext = new GameContext();
        }
        public HomeController(IPlayerRepository playerRepo)
        {
            _playerRepository = playerRepo;
        }
        #endregion Constructors

        public ActionResult Index()
        {
            string cookieNick = string.Empty;
            if (CookieManager.CheckUserCookie(this.HttpContext))
            {
                cookieNick = CookieManager.CookieValue;
            }
            ViewBag.Nickname = cookieNick;
            TempData["nickname"] = cookieNick;
           // _gameContext.AddPlayer(cookieNick);

            return View();
        }

        [HttpPost]
        public ActionResult Index(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return View();
            }
            else
            {
                //TODO : check if nick is already taken
                CookieManager.WriteUserCoockie(this.HttpContext, nickname);
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
