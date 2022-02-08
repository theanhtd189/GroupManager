using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupManager.Models;
using GroupManager.Functions;

namespace GroupManager.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private User_Function dal = new User_Function();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Delete(int id, string return_url)
        {
            string msg = "";
            var stt = dal.Delete(id);
            if (stt)
            {
                msg = "Deleted successfully!";
            }
            else
                msg = "Error!";
            return Redirect(return_url + "?Msg=" + msg);
        }
        public ActionResult Create(int gid = -1, string return_url = "")
        {
            var list = dal.ListCategory();
            ViewBag.List = list;
            ViewBag.GID = gid;
            ViewBag.return_url = return_url;
            if (gid == -1)
            {
                return View();
            }
            if (new Group_Function().Get(gid) != null)
            {

                return View();
            }
            else
                return RedirectToAction("Group", "Home");
        }
        [HttpPost]
        [ValidateInput(false)]
        public int CreateReq(string fullname, int gender, string date, string username, string password, string email, string img = "")
        {
            bool g = false;
            if(gender==0)
            {
                g = false;
            }   
            else
            {
                g = true;
            }
            var check = dal.CheckEmail(email);
            if (check)
            {
                return -1;
            }
            else
            {
                var o = new User
                {
                    FullName = fullname,
                    Gender = g,
                    Date = DateTime.Parse(date),
                    Img = img,
                    Username = username,
                    Password = password,
                    Email = email,
                    Stt = true
                };
                return dal.Add(o);
            }    
            
        }
        [HttpPost]
        public bool SetRole(int uid, int rid)
        {
            return dal.SetRole(uid,rid);
        }
    }
}