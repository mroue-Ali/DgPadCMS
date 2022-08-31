using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DgPadCMS.Models
{
    public class Term
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


        public int taxonomyId { get; set; }
        public virtual Taxonomy taxonomy { get; set; }

        public ICollection<PostTerm> postTerms { get; set; }
    }
}
