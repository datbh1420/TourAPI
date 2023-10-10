using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TourAPI.Models.Domains;

namespace TourAPI.Data
{
    public class TourDBContext : IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);

            // Bỏ tiền tố AspNet của các bảng
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (!string.IsNullOrEmpty(tableName) && tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            var seedRoles = new List<IdentityRole>{
                new IdentityRole{ Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin"},
                new IdentityRole{ Name = "User", ConcurrencyStamp = "2", NormalizedName = "User"},
                new IdentityRole{ Name = "HR", ConcurrencyStamp = "3", NormalizedName = "HumanResources"}
            };

            modelBuilder.Entity<IdentityRole>().HasData(seedRoles);
        }
        public TourDBContext()
        {
        }
        public TourDBContext(DbContextOptions<TourDBContext> options) : base(options) { }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Location> Locations { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(GetConnectionString());
        //        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        //    }
        //}

        //private string GetConnectionString()
        //{
        //    IConfiguration config = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", true, true)
        //        .Build();

        //    var strConn = config["ConnectionStrings:TourDBConnectionString"];
        //    return strConn;
        //}
    }
}
