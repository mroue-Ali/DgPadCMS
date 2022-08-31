using System.Collections.Generic;

namespace DgPadCMS.Models
{
    public class PostTypeViewModel
    {
        public List<Taxonomy> taxonomies { get; set; }
        public PostType PostType { get; set; }
    }
}
