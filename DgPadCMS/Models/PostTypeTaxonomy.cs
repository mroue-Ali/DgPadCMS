namespace DgPadCMS.Models
{
    public class PostTypeTaxonomy
    {
        public int postTypeId { get; set; }
        public int taxonomyId { get; set; }
        public PostType PostType { get; set; }
        public Taxonomy Taxonomy  { get; set; }
    }
}
