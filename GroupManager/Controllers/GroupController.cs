using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupManager.Models;
using GroupManager.Functions;

namespace GroupManager.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        private Group_Function dal = new Group_Function();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int gid, string return_url="no")
        {
            var o = dal.Get(gid);
            ViewBag.Object = o;
            var list = dal.ListCategory();
            ViewBag.GID = gid;
            ViewBag.List = list;
            ViewBag.return_url = return_url;
            if (return_url != "no")
                ViewBag.return_url = return_url;
            if (o != null)
            {
                return View();
            }
            else
                return RedirectToAction("Group","Home");
        }
        public ActionResult Create()
        {
            var list = dal.ListCategory();
            ViewBag.List = list;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public int CreateReq(string title, string desc, int cate)
        {
            var o = new Group { 
                Title=title,
                Desc=desc,
                Category_ID=cate,
                Created_By=(int)Session["user_id"],
                Created_Date=DateTime.Now,
                Stt=true
            };
            return dal.Add(o);
        }
        [HttpPost]
        [ValidateInput(false)]
        public bool Edit(int gid, string title, string desc, int cate)
        {
            var o = dal.Get(gid);
            o.Title = title;
            o.Desc = desc;
            o.Category_ID = cate;
            return dal.Edit(o);
        }
        [HttpPost]
        [ValidateInput(false)]
        public bool AddMember(int gid, int uid)
        {
            return dal.AddUser(gid, uid);
        }

        public ActionResult DeleteMember(int gid, int uid,string return_url)
        {
            if(dal.DeleteUser(gid, uid))
                return Redirect(return_url+"?Msg=Successful!");
            else
                return Redirect(return_url + "?Msg=Error!");

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
    }
}