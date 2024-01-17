using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ADO.NET_EF_Test_Aplication.EFCore
{
    static public class EF
    {
        static public void EfCreateDatabase()
        {
            using (var context = new TestContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var toyota = new Car() { Name = "Toyota" };
                context.Users.Add(new User() { Name = "John", Car = toyota });
                context.Users.Add(new User() { Name = "Jane", Car =  toyota });
                context.SaveChanges();
                foreach (var user in context.Users.Include(user => user.Car))
                {
                    Console.WriteLine($"{user.Id}: {user.Name}, {user.Car.Name}");
                }
                Console.WriteLine("");
                foreach (var car in context.Car)
                {
                    Console.WriteLine($"{car.Users.Count}: {car.Name}");
                }
            }

            Console.ReadKey();
        }
    }

    public class TestContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=./usersdata.db");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Car { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Car>(s => s.Car)
                .WithMany(g => g.Users)
                .HasForeignKey(s => s.CarId);
        }
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        [ForeignKey("Car")]
        public int CarId { get; set; }
        
        public Car Car { get; set; }
    }

    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<User> Users { get; set; }
    }
}