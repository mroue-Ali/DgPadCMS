using System.Collections.Generic;

namespace DgPadCMS.Models
{
    public class PostType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public ICollection<PostTypeTaxonomy> postTypeTaxonomies { get; set; }
        public ICollection<Post> posts { get; set; }
    }
}
