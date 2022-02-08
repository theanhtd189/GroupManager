using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupManager.Controllers
{
    public  class MiddlewareController : Controller
    {
        // GET: Middleware
        public ActionResult Index()
        {
            return View();
        }
        public void CheckLogin()
        {
            if (Session["login"] != null)
            {

            }
            else
               RedirectToAction("Index", "Login", new { area = "" });
        }
    }
}