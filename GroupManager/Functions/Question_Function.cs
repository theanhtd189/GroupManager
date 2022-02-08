using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupManager.Models;
using GroupManager.Functions;

namespace GroupManager.Functions
{
    public class Question_Function
    {
        private Context db;
        public Question_Function()
        {
            db = new Context();
        }
        public List<Question> ListAll(string order = "asc")
        {
            if (order != "asc")
            {
                return db.Questions.OrderByDescending(x => x.ID).ToList();
            }
            return db.Questions.ToList();
        }
        public Question Get(int _id)
        {
            return db.Questions.FirstOrDefault(x => x.ID == _id);
        }
        public bool Delete(int id)
        {
            var o = db.Questions.FirstOrDefault(x=>x.ID==id);
            if (o != null)
            {
                db.Questions.Remove(o);
                db.SaveChanges();
                return false;
            }
            return false;
        }
        public int Add(Question e)
        {
            db.Questions.Add(e);
            int stt = db.SaveChanges();
            return e.ID;
        }
        public List<QuestionCategory> ListCategory()
        {
            return db.QuestionCategories.ToList();
        }
        public QuestionCategory GetCategory(int id)
        {
            return db.QuestionCategories.FirstOrDefault(x => x.ID == id);
        }
        public List<QuestionComment> ListCmt(int post_id)
        {
            return db.QuestionComments.Where(x => x.Question_ID == post_id).ToList();
        }
        public bool DeleteComment(int pid, int cid)
        {
            var o = db.QuestionComments.FirstOrDefault(x => x.ID == cid && x.Question_ID == pid);
            if (o != null)
            {
                db.QuestionComments.Remove(o);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool EditComment(int qid, int cid, string content)
        {
            var o = db.QuestionComments.FirstOrDefault(x => x.ID == cid && x.Question_ID == qid);
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
        public bool AddComment(int uid, int qid, string content)
        {
            var o = db.Questions.FirstOrDefault(x => x.ID == qid);
            if (o != null)
            {
                var e = new QuestionComment
                {
                    Question_ID = qid,
                    Created_By = uid,
                    Stt = true,
                    Created_Date = DateTime.Now,
                    Content = content
                };
                db.QuestionComments.Add(e);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool Edit(int pid, string title, int cate, string content)
        {
            var o = Get(pid);
            if (o != null)
            {
                if(o.Content!=content)
                    o.Content = content;
                if (o.Title != title)
                    o.Title = title;
                if(o.Category_ID!=cate)
                    o.Category_ID=cate;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}