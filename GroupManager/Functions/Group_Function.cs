using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupManager.Models;

namespace GroupManager.Functions
{
    public class Group_Function
    {
        private Context db;
        public Group_Function()
        {
            db = new Context();
        }
        public List<Group> ListAll(string order="asc")
        {
            if (order != "asc")
            {
                return db.Groups.OrderByDescending(x=>x.ID).ToList();
            }
            return db.Groups.ToList();
        }
        public Group Get(int _id)
        {
            return db.Groups.FirstOrDefault(x=>x.ID == _id);
        }
        public Group Get(string title)
        {
            return db.Groups.FirstOrDefault(x => x.Title == title);
        }
        public GroupCategory GetCategory(int cid)
        {
            return db.GroupCategories.FirstOrDefault(x=>x.ID==cid);
        }
        public int Add(Group e)
        {
                    db.Groups.Add(e);
                    db.SaveChanges();
                    return e.ID;
        }

        public int CountMember(int gid)
        {
            return db.User_Group.Count(x=>x.Group_ID==gid);
        }

        public bool Edit(Group e) //entity paramater
        {
            var o = Get(e.ID); //original object
            if (o != null)
            {
                if (e.Title != o.Title)
                    o.Title = e.Title;
                if (e.Category_ID != o.Category_ID)
                    o.Category_ID = e.Category_ID;
                if (e.Desc != o.Desc)
                    o.Desc = e.Desc;
                if (e.Created_By != o.Created_By)
                    o.Created_By = e.Created_By;
                if (e.Stt != e.Stt)
                    o.Stt = e.Stt;
                if (e.Created_Date != o.Created_Date)
                    o.Created_Date = e.Created_Date;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Tìm kiếm group theo tên
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<Group> Search(string s)
        {
            return db.Groups.Where(x => x.Title.Contains(s)).ToList();
        }
        /// <summary>
        /// Danh sách post của group 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Post> ListPost(int id)
        {
            return db.Posts.Where(x => x.Group_ID == id).ToList();
        }
        /// <summary>
        /// Danh sách quiz của group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Quiz> ListQuiz(int id)
        {
            return db.Quizs.Where(x => x.Group_ID == id).ToList();
        }
        /// <summary>
        /// Danh sách question của group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Question> ListQuestion(int id)
        {
            return db.Questions.Where(x => x.Group_ID == id).ToList();
        }
        /// <summary>
        /// Danh sách bài giảng đc đăng trong nhóm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Lesson> ListLesson(int id)
        {
            return db.Lessons.Where(x => x.Group_ID == id).ToList();
        }
        public List<GroupCategory> ListCategory()
        {
            return db.GroupCategories.ToList();
        }
        public List<User> ListUser(int id)
        {
            var lid = db.User_Group.Where(x => x.Group_ID == id).Select(x => x.User_ID).ToList();
            List<User> list = new List<User>();
            foreach(var item in lid)
            {
                var u = new User_Function().GetUser((int)item);
                if (u!=null)
                list.Add(u);
            }
            list.Add(new User_Function().GetUser((int)Get(id).Created_By));
            return list;
        }
        public List<User> ListUserNotIn(int id)
        {
            var listall = db.Users.Select(x => x.ID).ToList();
            var l3 = listall.Except(ListUser(id).Select(x=>x.ID).ToList()).ToList();
            List<User> list = new List<User>();
            foreach (var item in l3)
            {
                var u = new User_Function().GetUser((int)item);
                if (u != null)
                    list.Add(u);
            }
            return list;
        }
        public string TimeIn(int gid, int uid)
        {
            string t = "Chưa cập nhật";
            var id = db.User_Group.FirstOrDefault(x => x.Group_ID == gid && x.User_ID==uid);
            if (id != null)
            {
                if (id.Created_Date != null)
                    return id.Created_Date.Value.ToString("dd/MM/yyyy");
                else
                    return t;
            }
            
            return t;
        }
        public bool IsInGroup(int user_id, int group_id)
        {
            var g = db.User_Group.FirstOrDefault(x => x.User_ID == user_id && x.Group_ID == group_id);
            if (g != null)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Kiểm tra xem user có phải admin group
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        public bool IsAdminGroup(int user_id, int group_id)
        {
            var g = new Group_Function().Get(group_id);
            if (g != null)
            {
                return g.Created_By == user_id;
            }
            return false;
        }
        public bool AddUser(int gid, int uid)
        {
            var o = db.User_Group.FirstOrDefault(x=>x.User_ID==uid && x.Group_ID == gid);
            if (o == null)
            {
                db.User_Group.Add(new User_Group { User_ID = uid, Group_ID = gid, Created_Date = DateTime.Now });
                db.SaveChanges();
                return true;
            }
            else
            if (Get(gid).Created_By == uid)
            {
                return true;
            }
            else
                return false;
        }
        public bool DeleteUser(int gid, int uid)
        {
            var o = db.User_Group.FirstOrDefault(x => x.User_ID == uid && x.Group_ID == gid);
            if (o != null)
            {
                db.User_Group.Remove(o);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool Delete(int gid)
        {
            try
            {
                var a = Get(gid);
                if (a != null)
                {
                    db.Groups.Remove(a);
                    var b = db.Posts.Where(x => x.Group_ID == gid);
                    if (b != null)
                    {
                        db.Posts.RemoveRange(b);
                    }
                    var c = db.Lessons.Where(x => x.Group_ID == gid);
                    if (c != null)
                    {
                        db.Lessons.RemoveRange(c);
                    }
                    var d = db.Questions.Where(x => x.Group_ID == gid);
                    if (d != null)
                    {
                        db.Questions.RemoveRange(d);
                    }
                    var e = db.Quizs.Where(x => x.Group_ID == gid);
                    if (e != null)
                    {
                        db.Quizs.RemoveRange(e);
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}