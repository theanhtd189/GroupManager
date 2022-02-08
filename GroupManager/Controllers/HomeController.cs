using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupManager.Functions;
using GroupManager.Models;

namespace GroupManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string Msg="")
        {
            if (Session["login"] != null)
            {
                var dal = new User_Function();
                int user_id = (int)Session["user_id"];
                User u = dal.GetUser(user_id);
                ViewBag.CurrentUser = u;
                ViewBag.Msg = (string.IsNullOrEmpty(Msg)) ? "" : Msg;
                ViewBag.ListGroup = dal.ListGroup(user_id);
                ViewBag.ListComment = dal.ListComment(user_id);
                ViewBag.ListPost = dal.ListPost(user_id);
                ViewBag.ListReply = dal.ListReply(user_id);
                ViewBag.ListQuestion = dal.ListQuestion(user_id);

                    ViewBag.Title = "Home";
                    return View();     
            }
            else
                return RedirectToAction("Index","Login", new { area="" });
        }
        public ActionResult Post(int id, string Msg="")
        {
            ViewBag.Msg = (string.IsNullOrEmpty(Msg)) ? "" : Msg;
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                if (Session["login"] != null)
                {
                    var dal = new User_Function();
                    var dal_p = new Post_Function();
                    var p = dal_p.Get(id);
                    if (p != null)
                    {
                        int user_id = (int)Session["user_id"];
                            ViewBag.Title = p.Title;
                            ViewBag.Item = p;
                            ViewBag.ListCmt = dal_p.ListCmt(id);
                            return View();
                    }
                    else
                    {
                        return RedirectToAction("Group", new { Msg = "ID post không tồn tại" });
                    }
                    
                }
                else
                    return RedirectToAction("Index", "Login", new { area = "" });

            }
            else
            {
                return RedirectToAction("Group");
            }
        }
        public ActionResult Question(int id, string Msg="")
        {
            ViewBag.Msg = (string.IsNullOrEmpty(Msg)) ? "" : Msg;
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                if (Session["login"] != null)
                {
                    var dal = new User_Function();
                    var dal_p = new Question_Function();
                    var p = dal_p.Get(id);
                    if (p != null)
                    {
                        int user_id = (int)Session["user_id"];

                            ViewBag.Title = p.Title;
                            ViewBag.Item = p;
                            ViewBag.ListCmt = dal_p.ListCmt(id);
                            return View();
                    }
                    else
                    {
                        ViewBag.Msg = "ID post không tồn tại";
                        return RedirectToAction("Group");
                    }

                }
                else
                    return RedirectToAction("Index", "Login", new { area = "" });

            }
            else
            {
                return RedirectToAction("Group");
            }
        }
        public ActionResult DeleteQuestion(int id, string return_url = "Index")
        {
            ViewBag.Msg = "";
            var dal = new Question_Function();
            var g = dal.Get(id);
            if (g != null)
            {
                var stt = dal.Delete(id);
                if (stt)
                {
                    ViewBag.Msg = "Xóa thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Msg = "Lỗi!";
                    return RedirectToAction(return_url);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Lesson(int id, string act="no", string return_url = "Index", string Msg="")
        {
            ViewBag.Msg = (string.IsNullOrEmpty(Msg)) ? "" : Msg;
                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    if (Session["login"] != null)
                    {
                        var dal = new User_Function();
                        var dal_p = new Lesson_Function();
                        var p = dal_p.Get(id);
                        if (p != null)
                        {
                                if (act == "del")
                                {
                                    var stt = dal_p.Delete(id);
                                    if (stt)
                                    {
                                        return RedirectToAction("Index", new { Msg = "Xóa thành công!" });
                                    }
                                    else
                                    {
                                        return RedirectToAction("Index", new { Msg = "Lỗi!" });
                                    }
                                }
                                else
                                if (act == "edit")
                                {
                                return View("EditLesson");
                                }
                                else
                                {
                                    ViewBag.Title = p.Title;
                                    ViewBag.Item = p;
                                    ViewBag.ListCmt = dal_p.ListCmt(id);
                                    return View();
                                }                                  
                        }
                        else
                        {
                            return RedirectToAction("Group", new { Msg = "ID không tồn tại" });
                        }

                    }
                    else
                        return RedirectToAction("Index", "Login", new { area = "" });

                }
                else
                {
                    return RedirectToAction("Group");
                }
        }
        public ActionResult Group(string s, string Msg="")
        {
            if (Session["login"] != null)
            {
                var dal = new User_Function();
                int user_id = (int)Session["user_id"];
                ViewBag.Msg = (string.IsNullOrEmpty(Msg)) ? "" : Msg;
                ViewBag.ListGroup = dal.ListGroup(user_id);
                ViewBag.ListGroupNot = dal.ListGroupNotIn(user_id);
                    if (string.IsNullOrEmpty(s))
                    {
                        ViewBag.Title = "Group";
                        return View();
                    }
                    else
                    {
                        ViewBag.Title = "Tìm kiếm";
                        ViewBag.SearchString = s;
                        ViewBag.List = new Group_Function().Search(s);
                        return View("~/Views/Home/SearchGroup.cshtml");
                    }    
            }
            else
                return RedirectToAction("Index", "Login", new { area = "" });
        }
        public ActionResult GroupDetail(int id, string s="no", string act = "no", string type = "post", string Msg="")
        {
            ViewBag.Msg = (string.IsNullOrEmpty(Msg)) ? "" : Msg;
            var user_id = (int)Session["user_id"];
            string msg = "";
            var g = new Group_Function().Get(id);
            if (g != null)
            {
                var check = new User_Function().IsInGroup(user_id, id);
                if (check || g.Created_By==user_id || new User_Function().IsAdmin(user_id))
                {
                    var dal = new User_Function();
                    ViewBag.Group = g;
                    ViewBag.CurrentID = id;
                    ViewBag.Type = type;
                    ViewBag.Title = "Chi tiết nhóm";
                    ViewBag.SearchString = s;
                    if(type=="post" && s!="no")
                    {
                        var list_search = new Post_Function().Search(s);
                        ViewBag.list_search = list_search;
                        return View();
                    }
                    else
                    if (!string.IsNullOrEmpty(act))
                    {
                        if (act == "out")
                        {
                            var stt = dal.OutGroup(user_id, id);
                            if (stt)
                            {
                                return RedirectToAction("Group", new { Msg = "Out group successfully!" });
                            }
                            else
                            {
                                Response.Write("<script>alert('Error!')</script>");
                            }
                        }
                        else
                        if (act == "del")
                        {
                            var chck = dal.IsAdminGroup(user_id,id);                         
                            if (chck)
                            {
                                var stt = dal.DeleteGroup(user_id, id);
                                if (stt)
                                {
                                    msg = "Deleted successfully!";
                                }
                                else
                                    msg="Error!";

                                return RedirectToAction("Group", new { Msg = msg });
                            }
                            else
                            {
                                Response.Write("<script>alert('Forbbiden !')</script>");
                            }
                        }

                    }
                    return View("GroupDetail");
                }
                else
                {
                    return RedirectToAction("Group");
                }
                
            }
            else
                return RedirectToAction("Group");
        }
        public ActionResult GroupMember(int id,string act="no",int eid=0)
        {
            var dal = new Group_Function();
            if (dal.Get(id) != null)
            {
                var user_id = (int)Session["user_id"];
                var chck = dal.IsAdminGroup(user_id, id);
                if (chck)
                {
                    var g = dal.Get(id);
                    if (g != null)
                    {
                        ViewBag.Title = "Thành viên nhóm";
                        ViewBag.ID = id;
                        if (act == "del" && eid != 0)
                        {
                            var stt = dal.DeleteUser(id,user_id);
                            if (stt)
                            {
                                ViewBag.Msg = "Thành công";
                            }
                            else
                            {
                                ViewBag.Msg = "Lỗi xóa thành viên";
                            }
                            return View("GroupMember");
                        }
                        else
                        if (act == "add" && eid != 0)
                        {
                            var stt = dal.AddUser(id,user_id);
                            if (stt)
                            {
                                ViewBag.Msg = "Thành công";
                            }
                            else
                            {
                                ViewBag.Msg = "Lỗi thêm thành viên";
                            }
                            return View("GroupMember");
                        }
                        else
                        {                                                   
                            ViewBag.ListIn = dal.ListUser(id);
                            ViewBag.ListNotIn = dal.ListUserNotIn(id);
                            return View("GroupMember");
                        }                    
                    }
                    else
                        return RedirectToAction("Index");
                }
                else
                {
                    Response.Write("<script>alert('Bạn không có quyền xem nhóm!')</script>");
                    return RedirectToAction("Index");
                }
            }
            else
                return RedirectToAction("Group");
            
        }
        public ActionResult TakeInGroup(int gid)
        {
            ViewBag.Msg = "";
            var dal = new Group_Function();
            var dal_user = new User_Function();
            var user_id = (int)Session["user_id"];
            var g = dal.Get(gid);
            if (g != null)
                {
                var stt = dal_user.TakeGroup(user_id,gid);
                if (stt)
                {
                    return RedirectToAction("GroupDetail", new { id = gid, Msg = "Joined group successfully!" });
                }
                else
                {
                    return RedirectToAction("Group", new { Msg = "You joined this group already!" });
                }
                
            }
            return RedirectToAction("Group");
        }
    }
}