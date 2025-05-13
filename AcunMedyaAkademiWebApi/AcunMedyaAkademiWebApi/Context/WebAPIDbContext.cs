using AcunMedyaAkademiWebApi.Entities;

using Microsoft.EntityFrameworkCore;

namespace AcunMedyaAkademiWebApi.Context
{
    public class WebAPIDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=NARIN\\SQLEXPRESS; initial catalog=DbAcunMedyaWebAPI; integrated Security=true;");

        }

        public DbSet<Product>Products { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<About>Abouts { get; set; }
        public DbSet<Feature>Features { get; set; }
        public DbSet<Blog>Blogs { get; set; }
        public DbSet<Subscribe>Subscribes { get; set; }

    }
}
