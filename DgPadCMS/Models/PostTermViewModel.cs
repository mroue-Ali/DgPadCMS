using System;
using System.Collections.Generic;

namespace DgPadCMS.Models
{
    public class PostTermViewModel
    {
        public Post Post { get; set; }  
        public List<Term> termsOfTaxonomies { get; set; }
        public List<PostTypeTaxonomy> postTypeTaxonomies { get; set; }
    }
}
