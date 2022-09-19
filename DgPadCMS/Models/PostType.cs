using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DgPadCMS.Models
{
    public class PostType
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public ICollection<PostTypeTaxonomy> postTypeTaxonomies { get; set; }
        public ICollection<Post> posts { get; set; }
        public bool MediaChecked { get; set; }
    }
}
