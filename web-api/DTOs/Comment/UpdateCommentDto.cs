using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_api.DTOs.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters long")]
        [MaxLength(50, ErrorMessage = "Content must be at most 50 characters long")]
        public string Content { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters long")]
        [MaxLength(50, ErrorMessage = "Title must be at most 50 characters long")]
        public string Title { get; set; }
    }
}