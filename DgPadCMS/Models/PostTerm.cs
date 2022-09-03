namespace DgPadCMS.Models
{
    public class PostTerm
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int TermId { get; set; }

        public Term Term { get; set; }
    }
}
