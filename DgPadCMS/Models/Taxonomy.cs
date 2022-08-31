using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DgPadCMS.Models
{
    public class Taxonomy
    {
        
        public int Id { get; set; }
        [NotMapped]
        public bool isCheked { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<Term> terms { get; set; }
        public ICollection<PostTypeTaxonomy> postTypeTaxonomies { get; set; }

    }
}
