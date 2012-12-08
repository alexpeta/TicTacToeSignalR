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
        #region
        //private r
        #endregion

        #region Constructors
        public GameController()
        {
        }
        #endregion

        //
        // GET: /Game/{7974E9CA-AC9D-403F-8D31-BB8DB58CD8FD}
        public ActionResult Index(Guid? id)
        {
            if (id == null)
            {
                RedirectToAction("Index", "Home");
            }

            GameViewModel vm = new GameViewModel();
            return View(vm);
        }

        [HttpPost]
        public string Invite(string id)
        {
           
            return "all good chief!";
        }



        [HttpPost]
        public string Index()
        {
            return "POST not allowed";
        }

    }
}
