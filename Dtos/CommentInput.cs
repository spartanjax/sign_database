using System.ComponentModel.DataAnnotations;

namespace A1Database.Dtos
{
    public class CommentInput
    {
        [Required]
        public required string UserComment{get;set;}
        [Required]
        public required string Name{get;set;}
    }
}