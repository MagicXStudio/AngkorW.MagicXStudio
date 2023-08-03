using ImageStudio.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageStudio.Services
{
    /// <summary>
    ///https://wallhaven.cc/ Total Wallpapers 1,094,750
    /// http://behoimi.org/
    /// http://iqdb.org/
    /// </summary>
    public class WallhavenContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Detail> Details { get; set; }

        public WallhavenContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data Source=Awesome.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Ignore<Entity>();

            modelBuilder.Entity<Tag>(tag =>
            {
                tag.ToTable("Tag");
                tag.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Category>().ToTable("Category").HasKey(x => x.Id);

            modelBuilder.Entity<Detail>().ToTable("Detail")
                .Ignore(x => x.Colour)
                .Ignore(x => x.thumbs)
                .Ignore(x => x.IsBlocked)
                .Ignore(x => x.colors)
                .HasKey(x => x.id);
        }
    }
}
