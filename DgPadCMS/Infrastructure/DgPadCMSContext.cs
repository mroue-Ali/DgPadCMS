using DgPadCMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DgPadCMS.Infrastructure
{
    public class DgPadCMSContext :IdentityDbContext<AppUser>
    {
        public DgPadCMSContext(DbContextOptions<DgPadCMSContext> options)
            : base(options)
        {   }

        public DbSet<Taxonomy> taxonomies { get; set; }
        public DbSet<Term> terms { get; set; }
        public DbSet<PostType> postTypes { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<PostTerm> postTerms { get; set; }
        public DbSet<PostTypeTaxonomy> postTypeTaxonomies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Term>().HasOne(x => x.taxonomy).WithMany(x => x.terms);
            modelBuilder.Entity<Post>().HasOne(x => x.postType).WithMany(x => x.posts);
            modelBuilder.Entity<PostTerm>().HasKey(x => new {x.PostId, x.TermId});
            modelBuilder.Entity<PostTerm>().HasOne(x => x.Post).WithMany(x => x.postTerms);
            modelBuilder.Entity<PostTerm>().HasOne(x => x.Term).WithMany(x => x.postTerms);
            modelBuilder.Entity<PostTypeTaxonomy>().HasKey(x => new { x.postTypeId, x.taxonomyId });
            modelBuilder.Entity<PostTypeTaxonomy>().HasOne(x => x.PostType).WithMany(x => x.postTypeTaxonomies);
            modelBuilder.Entity<PostTypeTaxonomy>().HasOne(x => x.Taxonomy).WithMany(x => x.postTypeTaxonomies);
        }


    }
}
