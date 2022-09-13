using DgPadCMS.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DgPadCMS.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int PostTypeId { get; set; }
        public  PostType postType { get; set; }  
        public DateTime CreationDate { get; set; }
        public string Detail { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }

        public string Media { get; set; }
        [NotMapped]
        [FileVideoExtension]
        public IFormFile MediaUpload { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
        public ICollection<PostTerm> postTerms { get; set; }

    }
}
