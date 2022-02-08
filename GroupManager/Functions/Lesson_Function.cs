using GroupManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupManager.Functions
{
    public class Lesson_Function
    {
        private Context db;
        public Lesson_Function()
        {
            db = new Context();
        }
        public List<Lesson> ListAll(string order = "asc")
        {
            if (order != "asc")
            {
                return db.Lessons.OrderByDescending(x => x.ID).ToList();
            }
            return db.Lessons.ToList();
        }
        public Lesson Get(int _id)
        {
            return db.Lessons.FirstOrDefault(x => x.ID == _id);
        }
        public int Add(Lesson e)
        {
            db.Lessons.Add(e);
            int stt = db.SaveChanges();
            return e.ID;
        }
        public List<LessonComment> ListCmt(int Lesson_id)
        {
            return db.LessonComments.Where(x => x.Lesson_ID == Lesson_id).ToList();
        }
        public bool Delete(int id)
        {
            var o = Get(id);
            if (o != null)
            {
                db.Lessons.Remove(o);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool DeleteComment(int pid, int cid)
        {
            var o = db.LessonComments.FirstOrDefault(x => x.ID == cid && x.Lesson_ID == pid);
            if (o != null)
            {
                db.LessonComments.Remove(o);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool EditComment(int lid, int cid, string content)
        {
            var o = db.LessonComments.FirstOrDefault(x => x.ID == cid && x.Lesson_ID == lid);
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
        public bool AddComment(int uid, int lid, string content)
        {
            var o = db.Lessons.FirstOrDefault(x => x.ID == lid);
            if (o != null)
            {
                var e = new LessonComment
                {
                    Lesson_ID = lid,
                    Created_By = uid,
                    Stt = true,
                    Created_Date = DateTime.Now,
                    Content = content
                };
                db.LessonComments.Add(e);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool Edit(int pid, string title, string content)
        {
            var o = Get(pid);
            if (o != null)
            {
                o.Content = content;
                o.Title = title;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}