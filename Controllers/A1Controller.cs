using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using A1Database.Models;
using A1Database.Data;
using A1Database.Dtos;
//using A1Database.Dtos;

namespace A1Database.Controllers
{
    [Route("webapi")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly IA1Repo _repository;

        public CustomersController(IA1Repo repository)
        {
            _repository = repository;
        }

        //ENDPOINT 1: GET version of Web API
        [HttpGet("GetVersion")]
        public ActionResult<String> GetVersion()
        {
            string c = _repository.GetVersion();
            return Ok(c);
        }

        //ENDPOINT 2: GET project logo PNG
        [HttpGet("Logo")]
        public ActionResult Logo()
        {
            string name = _repository.Logo();
            return PhysicalFile(name, "image/png");
        }

        //ENDPOINT 3: GET list of all signs
        [HttpGet("AllSigns")]
        public ActionResult<IEnumerable<Sign>> AllSigns()
        {
            IEnumerable<Sign> signs = _repository.AllSigns();
            return Ok(signs);
        }

        //ENDPOINT 4: 
        [HttpGet("Signs/{term}")]
        public ActionResult<IEnumerable<Sign>> Signs(string term)
        {
            IEnumerable<Sign> signs = _repository.Signs(term);
            return Ok(signs);
        }

        //ENDPOINT 5: GET the image of a given sign
        [HttpGet("SignImage/{id}")]
        public ActionResult SignImage(string id)
        {
            string dir = _repository.SignImage(id);
            string fileName1 = Path.Combine(dir, id + ".png");
            string fileName2 = Path.Combine(dir, id + ".jpg");
            string fileName3 = Path.Combine(dir, id + ".gif");
            string fileName = Path.Combine(dir, "default.png");
            string respHeader = "image/png";
            
            if(System.IO.File.Exists(fileName1))
            {
                fileName = fileName1;
            } 
            else if (System.IO.File.Exists(fileName2))
            {
                fileName = fileName2;
                respHeader = "image/jpg";
            }
            else if (System.IO.File.Exists(fileName3))
            {
                fileName = fileName3;
                respHeader = "image/gif";
            }

            return PhysicalFile(fileName, respHeader);
        }

        //ENDPOINT 6: GET a comment with a given ID
        [HttpGet("GetComment/{id}")]
        public ActionResult GetComment(int id)
        {
            Comment comment = _repository.GetComment(id);
            Console.WriteLine($"Test {comment}");
            if (comment == null)
            {
                return BadRequest($"Comment {id} does not exist.");
            }

            return Ok(comment);
        }

        //ENDPOINT 7: POST a comment
        [HttpPost("WriteComment")]
        public ActionResult<Comment> WriteComment(CommentInput comment)
        {
            Comment c = new Comment{UserComment = comment.UserComment, Name = comment.Name, Time = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ"), IP =  Request.HttpContext.Connection.RemoteIpAddress.ToString()};
            Comment addedComment = _repository.WriteComment(c);
            return CreatedAtAction(nameof(GetComment), new {id = addedComment.Id}, addedComment);
        }

        //ENDPOINT 8: GET and display a given # of comments
        [HttpGet("Comments/{num}")]
        public ActionResult<IEnumerable<Comment>> Comments(int num=5)
        {
            IEnumerable<Comment> comments = _repository.Comments(num).Reverse();
            return Ok(comments);
        }
    }
}
