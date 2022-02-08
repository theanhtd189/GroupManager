using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupManager.Models;
using GroupManager.Functions;

namespace GroupManager.Controllers
{
    public class LessonController : Controller
    {
        Lesson_Function dal = new Lesson_Function();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int id, string return_url="no")
        {
            var o = dal.Get(id);
            ViewBag.Object = o;
            if (return_url != "no")
                ViewBag.return_url = return_url;
            if (o != null)
            {
                return View();
            }
            else
                return RedirectToAction("Group", "Home");
        }
        [HttpPost]
        [ValidateInput(false)]
        public bool EditLesson(int id, string title, string content)
        {
            //
            if (dal.Get(id) != null)
            {
                return dal.Edit(id,title, content);
            }
            else
                return false;
        }
        [HttpPost]
        public bool AddCmt(int pid, int uid, string content)
        {
            //
            return dal.AddComment(uid, pid, content);
        }
        public ActionResult DeleteComment(int pid, int cmt_id, string return_url = "")
        {
            //
            var stt = dal.DeleteComment(pid, cmt_id);
            if (!stt)
            {
                return Redirect(return_url);
            }
            return Redirect(return_url);
        }
        public ActionResult DeleteLesson(int id, int gid)
        {
            //
            var g = dal.Get(id);
            if (g != null)
            {
                var stt = dal.Delete(id);
                if(stt)
                {
                    return RedirectToAction("GroupDetail", "Home", new { id = gid, Msg = "Xóa thành công!" });
                }
                else
                {
                    return RedirectToAction("GroupDetail", "Home", new { id = gid, Msg = "Lỗi!" });
                }
            }
            return RedirectToAction("Index");
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
        [HttpPost]
        [ValidateInput(false)]
        public int CreateReq(int gid, string title, string desc, string content)
        {
            var o = new Lesson
            {
                Title = title,
                Group_ID = gid,
                Created_By = (int)Session["user_id"],
                Created_Date = DateTime.Now,
                Content = content,
                Stt = true
            };
            return dal.Add(o);
        }
        public ActionResult Create(int gid=-1)
        {
            ViewBag.GID = gid;
            if (gid == -1)
            {
                return View();
            }
            else
            if (new Group_Function().Get(gid) != null)
            {
                return View();
            }
            else
                return RedirectToAction("Group", "Home");
        }

    }
}