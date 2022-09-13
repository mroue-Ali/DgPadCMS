using DgPadCMS.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DgPadCMS.Models
{
    public class PostTermViewModel
    {
        public Post Post { get; set; }  

        public List<Term> termsOfTaxonomies { get; set; }
        public List<PostTypeTaxonomy> postTypeTaxonomies { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; } 
        [NotMapped]
        [FileExtension]
        public IFormFile MediaUpload { get; set; }
    }
}
