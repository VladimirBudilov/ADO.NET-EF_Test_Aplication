using ADO.NET_EF_Test_Aplication.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ADO.NET_EF_Test_Aplication.EFCore
{
    public sealed class TestContext : DbContext
    {
        public TestContext() => Database.EnsureCreated();
        readonly StreamWriter _writer = new StreamWriter("log.txt");
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Car { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=./userEFsdata.db");
            optionsBuilder.LogTo(_writer.WriteLine, LogLevel.Information);
        }
            
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Car>(s => s.Car)
                .WithMany(g => g.Users)
                .HasForeignKey(s => s.CarId);
        }

        public override void Dispose()
        {
            base.Dispose();
            _writer.Dispose();
        }
    }
}
