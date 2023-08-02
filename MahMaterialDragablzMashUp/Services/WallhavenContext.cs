using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public WallhavenContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);

    }
}
