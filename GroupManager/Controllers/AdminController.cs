using GroupManager.Functions;
using GroupManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GroupManager.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["login"] != null)
            {
                ViewBag.HideLeft = true;
                int user_id = (int)Session["user_id"];
                User u = new User_Function().GetUser(user_id);
                ViewBag.CurrentUser = u;
                ViewBag.Title = "Admin - Tổng quan";
                if (Session["role"].ToString() == "User")
                {
                    return RedirectToAction("", "Home", new { area = "" });
                }
                else
                    return View();
            }
            else
                //return RedirectToAction("Login");
                return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult UserManager()
        {
            var dal = new User_Function();
            ViewBag.List = dal.ListAll("desc");
            return View();
        }
        public ActionResult GroupManager()
        {
            var dal = new Group_Function();
            ViewBag.List = dal.ListAll("desc");
            return View();
        }
        public ActionResult CommentManager()
        {
            var dal = new Group_Function();
            ViewBag.List = dal.ListAll("desc");
            return View();
        }
        public ActionResult LessonManager()
        {
            var dal = new Lesson_Function();
            ViewBag.List = dal.ListAll("desc");
            return View();
        }
        public ActionResult PostManager()
        {
            var dal = new Post_Function();
            ViewBag.List = dal.ListAll("desc");
            return View();
        }
        public ActionResult QuestionManager()
        {
            var dal = new Question_Function();
            ViewBag.List = dal.ListAll("desc");
            return View();
        }
        public ActionResult RoleManager()
        {
            return View();
        }
    }
}