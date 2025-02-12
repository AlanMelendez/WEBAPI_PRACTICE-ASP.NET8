using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api.DTOs.Comment
{
    public class UpdateCommentDto
    {
        public string Content { get; set; }
        public string Title { get; set; }
    }
}