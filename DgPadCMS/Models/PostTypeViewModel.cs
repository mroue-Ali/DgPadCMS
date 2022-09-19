using System.Collections.Generic;

namespace DgPadCMS.Models
{
    public class PostTypeViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public bool MediaChecked { get; set; }    
        public List<Taxonomy> availabletaxonomies { get; set; }
    }
}
