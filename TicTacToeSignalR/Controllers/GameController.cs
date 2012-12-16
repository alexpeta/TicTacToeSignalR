using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicTacToeSignalR.Core.Mechanics;
using TicTacToeSignalR.Models;
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
                profile = ProfileViewModel.Null;
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
