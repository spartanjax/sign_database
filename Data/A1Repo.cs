using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using A1Database.Data;
using A1Database.Models;
using System.Timers;


namespace A1Database.Data
{
    public class A1Repo : IA1Repo
    {
        private readonly A1DbContext _dbContext;

        public A1Repo(A1DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public String GetVersion()
        {
            return "1.0.0 (Ngāruawāhia) by jfon971";
        }

        public string Logo()
        {
            string path = Directory.GetCurrentDirectory();
            string imgName = Path.Combine(path, "Logos/Logo.png");
            return imgName;
        }

        public IEnumerable<Sign> AllSigns()
        {
            IEnumerable<Sign> signs = _dbContext.Signs.ToList<Sign>();
            return signs;
        }
        
        public IEnumerable<Sign> Signs(string inp)
        {
            IEnumerable<Sign> signs = _dbContext.Signs.Where(s => s.Description.Contains(inp));
            return signs;
        }

        public string SignImage(string id)
        {
            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "SignsImages");
            return imgDir;
        }

        public Comment GetComment(int id)
        {
            Comment? c = _dbContext.Comments.FirstOrDefault(c => c.Id == id);
            return c;
        }

        public Comment WriteComment(Comment comment)
        {
            EntityEntry<Comment> e = _dbContext.Comments.Add(comment);
            Comment c = e.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public IEnumerable<Comment> Comments(int num)
        {
            IEnumerable<Comment> allComments = _dbContext.Comments.ToList<Comment>();
            if (num > allComments.Count()){num = allComments.Count();}
            IEnumerable<Comment> comments = allComments.TakeLast(num);

            /*int minimum;
            if (num > allComments.Count()){minimum = 0;}else{minimum = allComments.Count()-num;}
            for (int i = allComments.Count() - 1; i >= minimum; --i)
            {
                comments.Add(allComments.ElementAt(i));
            }*/
            return comments;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
