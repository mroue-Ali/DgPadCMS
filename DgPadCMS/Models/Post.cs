using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DgPadCMS.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int PostTypeId { get; set; }
        public virtual PostType postType { get; set; }  
        public DateTime CreationDate { get; set; }
        public string Detail { get; set; }
        public string Summary { get; set; }
        public ICollection<PostTerm> postTerms { get; set; }

    }
}
