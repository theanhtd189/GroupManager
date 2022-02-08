using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupManager.Models;

namespace GroupManager.Functions
{
    public class Post_Function
    {
        private Context db;
        public Post_Function()
        {
            db = new Context();
        }
        public List<Post> ListAll(string order = "asc")
        {
            if (order != "asc")
            {
                return db.Posts.OrderByDescending(x => x.ID).ToList();
            }
            return db.Posts.ToList();
        }
        public Post Get(int _id)
        {
            return db.Posts.FirstOrDefault(x => x.ID == _id);
        }
        public PostCategory GetCategory(int id)
        {
            return db.PostCategories.FirstOrDefault(x => x.ID == id);
        }
        public List<PostCategory> ListCategory()
        {
            return db.PostCategories.ToList();
        }
        public List<PostComment> ListCmt(int post_id)
        {
            return db.PostComments.Where(x=>x.Post_ID==post_id).ToList();
        }
        public bool Delete(int id)
        {
            var o = Get(id);
            if (o != null)
            {
                DeleteAllComment(id);
                db.Posts.Remove(o);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public int Add(Post e)
        {
            db.Posts.Add(e);
            int stt = db.SaveChanges();
            return e.ID;
        }
        public bool Edit(int pid,string title,int cate, string content)
        {
            var o = Get(pid);
            if (o != null)
            {
                if(o.Content!=content && !string.IsNullOrEmpty(content))
                o.Content = content;
                if(o.Title!=title && !string.IsNullOrEmpty(title))
                o.Title = title;
                if (o.Category_ID != cate && !string.IsNullOrEmpty(cate.ToString()))
                    o.Category_ID = cate;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public void DeleteAllComment(int pid)
        {
            var o = db.PostComments.Where(x=>x.Post_ID==pid);
            if (o != null)
            {
                db.PostComments.RemoveRange(o);
                db.SaveChanges();
            }
            
        }
        public bool DeleteComment(int pid, int cid)
        {
            var o = db.PostComments.FirstOrDefault(x => x.ID == cid && x.Post_ID == pid);
            if (o != null)
            {
                db.PostComments.Remove(o);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool EditComment(int pid, int cid, string content)
        {
            var o = db.PostComments.FirstOrDefault(x => x.ID == cid && x.Post_ID == pid);
            if (o != null)
            {
                o.Modified_Date = DateTime.Now;
                 o.Content = content;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool AddComment(int uid, int pid, string content)
        {
            var o = db.Posts.FirstOrDefault(x => x.ID == pid);
            if (o != null)
            {
                var e = new PostComment
                {
                    Post_ID = pid,
                    Created_By = uid,
                    Stt = true,
                    Created_Date = DateTime.Now,
                    Content = content
                };
                db.PostComments.Add(e);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Tìm kiếm post theo tên
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<Post> Search(string s)
        {
            return db.Posts.Where(x => x.Title.Contains(s)).ToList();
        }
    }
}