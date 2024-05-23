using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(180, ErrorMessage = "Title must be at most 50 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters")]
        [MaxLength(180, ErrorMessage = "Content must be at most 50 characters")]
        public string Content { get; set; } = string.Empty;
    }
}