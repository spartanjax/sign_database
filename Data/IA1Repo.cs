using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A1Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace A1Database.Data
{
    public interface IA1Repo
    {
        string GetVersion();
        string Logo();
        IEnumerable<Sign> AllSigns();
        IEnumerable<Sign> Signs(string inp);
        string SignImage(string id);
        Comment GetComment(int id);
        Comment WriteComment(Comment c);
        IEnumerable<Comment> Comments(int num);
        void SaveChanges();
    }
}

