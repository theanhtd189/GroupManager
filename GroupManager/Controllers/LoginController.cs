using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupManager.Functions;

namespace GroupManager.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            //if (string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
            if (Session["login"] == null && Request.UrlReferrer!=null)
            {
                Response.Write("<script>alert('Bạn đã đăng xuất!')</script>");
            }
            return View();

        }
        [HttpPost]
        public bool Login(string username, string password)
        {
            var dal = new User_Function();
            var o = dal.Login(username,password);
            if (o!=-1)
            {
                Session["login"] = true;
                Session["user_id"] = o;
                Session["role"] = dal.GetRole(o);
                return true;
            }
            return false;
        }

    }
}