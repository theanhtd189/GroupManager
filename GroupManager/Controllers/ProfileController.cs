using GroupManager.Functions;
using GroupManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupManager.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            if (Session["login"] != null)
            {
                ViewBag.Title = "Trang cá nhân";
                int user_id = (int)Session["user_id"];
                User u = new User_Function().GetUser(user_id);
                ViewBag.CurrentUser = u;
               /* if (Session["role"].ToString() == "Admin")
                {
                    return RedirectToAction("", "Admin", new { area = "" });
                }
                else*/
                    return View();
            }
            else
            {
                return RedirectToAction("","Login");
            }             
        }
        [HttpPost]
        public bool Edit(int user_id, string fullname, string email, int gender, string date, string password)
        {
            if (Session["login"] != null)
            {
                return new User_Function().Edit(user_id,fullname,email,date,gender,password); 
            }
            else
                return false;
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("", "Login");
        }
    }
}