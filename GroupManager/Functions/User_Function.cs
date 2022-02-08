using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupManager.Models;

namespace GroupManager.Functions
{
    public class User_Function
    {
        private Context db;
        public User_Function()
        {
            db = new Context();
        }
        public List<PostCategory> ListCategory()
        {
            return db.PostCategories.ToList();
        }
        public List<Role> ListRole()
        {
            return db.Roles.ToList();
        }
        public List<User_Role> ListUserRole(string order="asc")
        {
            if (order != "asc")
                return db.User_Role.ToList();
            else
                return db.User_Role.OrderByDescending(x => x.ID).ToList();
        }
        public int Login(string username, string password)
        {
            var o = db.Users.FirstOrDefault(x=>x.Username == username && x.Password == password);
            if (o != null)
            {
                return o.ID;
            }
            else
                return -1;
        }
        public List<User> ListAll(string order = "asc")
        {
            if (order != "asc")
            {
                return db.Users.OrderByDescending(x => x.ID).ToList();
            }
            return db.Users.ToList();
        }
        public User GetUser(int user_id)
        {
            var o = db.Users.FirstOrDefault(x=>x.ID == user_id);
            return o;
        }
        public User GetUser(string email)
        {
            var o = db.Users.FirstOrDefault(x => x.Email == email);
            return o;
        }
        public bool Delete(int id)
        {
            var o = db.Users.FirstOrDefault(x => x.ID == id);
            if (o != null)
            {
                db.Users.Remove(o);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool SetRole(int uid, int rid)
        {
            var o = db.User_Role.FirstOrDefault(x=>x.User_ID == uid);
            if (o != null)
            {
                o.Role_ID = rid;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public int Add(User e, int role_id=1)
        {
            if (GetUser(e.Email) == null)
            {
                db.Users.Add(e);        
                db.SaveChanges();
                if (e.ID > 0)
                {
                    User_Role u = new User_Role();
                    u.User_ID = e.ID;
                    u.Role_ID = role_id;
                    db.User_Role.Add(u);
                    db.SaveChanges();
                }               
                return e.ID;
            }
            else
                return 0;
        }
        public Role GetRoleInfo(int id)
        {
            return db.Roles.FirstOrDefault(x=>x.ID==id);
        }
        public bool CheckEmail(string e)
        {
            return db.Users.FirstOrDefault(x => x.Email == e) != null;
        }
        public string GetRole(int user_id)
        {
            string role = "User";
            var o = GetUser(user_id);
            if (o != null)
            {
                int role_id = (int)db.User_Role.FirstOrDefault(x=>x.User_ID==user_id).Role_ID;
                role = db.Roles.FirstOrDefault(x=>x.ID==role_id).Title;
            }
            return role;
        }
        public bool Edit(int user_id,string fullname, string email, string date, int gender, string password)
        {
            var o = GetUser(user_id);
            if (o != null)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    o.Password = password;
                }
                o.FullName = fullname;
                o.Email = email;
                o.Date = DateTime.Parse(date);
                if (gender == 0)
                {
                    o.Gender = false;
                }
                else
                    o.Gender = true;
                db.SaveChanges();
                return true;
                 
            }
            else
                return false;
        }
        /// <summary>
        /// List group mà user này đã tham gia
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<Group> ListGroup(int user_id, string order="asc")
        {
            List<Group> list = new List<Group>();
            var o = db.User_Group.Where(x=>x.User_ID==user_id).Distinct().ToList();
            var admin_list = db.Groups.Where(x => x.Created_By == user_id).ToList();
            foreach(var item in o)
            {
                var obj = new Group_Function().Get((int)item.Group_ID);
                if (obj != null)
                {
                    list.Add(obj);
                }
            }
            foreach(var item in admin_list)
            {
                var obj = new Group_Function().Get((int)item.ID);
                if (obj != null)
                {
                    list.Add(obj);
                }
            }
            if (order != "asc")
            {
                return list.OrderByDescending(x => x.ID).ToList();
            }
            else
                return list;
        }
        /// <summary>
        /// List group mà user này chưa tham gia
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<Group> ListGroupNotIn(int user_id, string order="asc")
        {
            List<Group> list = new List<Group>();
            var a = ListGroup(user_id).Select(x=>x.ID);
            var b = db.Groups.Select(x=>x.ID);
            var c = b.Except(a);
            foreach (var item in c)
            {
                var obj = new Group_Function().Get(item);
                if (obj != null)
                {
                    list.Add(obj);
                }
            }
            if (order != "asc")
            {
                return list.OrderByDescending(x => x.ID).ToList();
            }
            else
                return list;
        }
        /// <summary>
        /// List group mà user này là admin
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<Group> ListAdminGroup(int user_id)
        {
            return db.Groups.Where(x=>x.Created_By==user_id).ToList();
        }
        /// <summary>
        /// Danh sách bài viết mà user đã tạo ở tất cả group
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<Post> ListPost(int user_id)
        {
            return db.Posts.Where(x => x.Created_By == user_id).OrderByDescending(x=>x.ID).ToList();        
        }
        /// <summary>
        /// Danh sách bài viết mà user đã tạo ở group này
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<Post> ListPost(int user_id, int group_id)
        {
            return db.Posts.Where(x => x.Created_By == user_id && x.Group_ID==group_id).OrderByDescending(x=>x.ID).ToList();
        }
        /// <summary>
        /// Danh sách câu hỏi mà user đã tạo ở tất cả group
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<Question> ListQuestion(int user_id)
        {
            return db.Questions.Where(x => x.Created_By == user_id).OrderByDescending(x => x.ID).ToList();
        }
        /// <summary>
        /// Danh sách câu hỏi mà user đã tạo ở group này
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<Question> ListQuestion(int user_id, int group_id)
        {
            return db.Questions.Where(x => x.Created_By == user_id && x.Group_ID==group_id).OrderByDescending(x => x.ID).ToList();
        }
        /// <summary>
        /// Tất cả bình luận bài viết mà user đã tạo 
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<PostComment> ListComment(int user_id)
        {
            return db.PostComments.Where(x => x.Created_By == user_id).OrderByDescending(x => x.ID).ToList();
        }
        /// <summary>
        /// Danh sách bình luận của user ở post
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="post_id"></param>
        /// <returns></returns>
        public List<PostComment> ListComment(int user_id, int post_id)
        {
            return db.PostComments.Where(x => x.Created_By == user_id && x.Post_ID==post_id).OrderByDescending(x => x.ID).ToList();
        }
        /// <summary>
        /// Tất cả câu trả lời mà user đã tạo
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<QuestionComment> ListReply(int user_id)
        {
            return db.QuestionComments.Where(x => x.Created_By == user_id).OrderByDescending(x => x.ID).ToList();
        }
        public List<Quiz> ListQuiz(int user_id)
        {
            return db.Quizs.Where(x => x.Created_By == user_id).OrderByDescending(x => x.ID).ToList();
        }
        public List<Quiz> ListQuiz(int user_id, int group_id)
        {
            return db.Quizs.Where(x => x.Created_By == user_id && x.Group_ID==group_id).OrderByDescending(x => x.ID).ToList();
        }
        public List<Lesson> ListLesson(int user_id, int g)
        {
            return db.Lessons.Where(x => x.Created_By == user_id && x.Group_ID == g).OrderByDescending(x => x.ID).ToList();
        }
        public List<Lesson> ListLesson(int user_id)
        {
            return db.Lessons.Where(x => x.Created_By == user_id).OrderByDescending(x => x.ID).ToList();
        }
        /// <summary>
        /// Kiểm tra user này đã tham gia group chưa 
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        public bool IsInGroup(int user_id,int group_id)
        {
            var g = db.User_Group.FirstOrDefault(x=>x.User_ID==user_id && x.Group_ID==group_id);
            if(g!= null)
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
        public bool IsAdminGroup(int user_id,int group_id)
            {
                var g = new Group_Function().Get(group_id);
                if(g!= null)
                {
                    return g.Created_By == user_id;
                }
                return false;
            }
        public bool IsAdmin(int user_id)
        {
            return GetRole(user_id) == "Admin";
        }
        public bool TakeGroup(int user_id, int group_id)
        {
            var c = db.User_Group.FirstOrDefault(x=>x.User_ID==user_id && x.Group_ID==group_id);
            if (c==null)
            {
                db.User_Group.Add(new User_Group { Group_ID=group_id,User_ID=user_id,Created_Date=DateTime.Now});
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool OutGroup(int user_id, int group_id)
        {
            var g = db.Groups.FirstOrDefault(x => x.ID == group_id);
            var c = db.User_Group.FirstOrDefault(x => x.User_ID == user_id && x.Group_ID == group_id);
            if (g != null && c!=null)
            {
                db.User_Group.Remove(c);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool DeleteGroup(int user_id, int group_id)
        {
            var g = db.Groups.FirstOrDefault(x => x.ID == group_id && x.Created_By==user_id);
            if (g != null)
            {
                db.Groups.Remove(g);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

    }
}