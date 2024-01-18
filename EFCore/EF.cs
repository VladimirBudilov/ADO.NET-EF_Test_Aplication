using ADO.NET_EF_Test_Aplication.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ADO.NET_EF_Test_Aplication.EFCore
{
    static public class EF
    {
        static public void EfCreateDatabase()
        {
            using (var context = new TestContext())
            {
                if(!context.Database.CanConnect()) return;
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                //Create
                var toyota = new Car() { Name = "Toyota" };
                context.Users.Add(new User() { Name = "John", Car = toyota });
                context.Users.Add(new User() { Name = "Jane", Car =  toyota });
                context.SaveChanges();
                foreach (var user in context.Users.Include(user => user.Car))
                {
                    Console.WriteLine($"{user.Id}: {user.Name}, {user.Car.Name}");
                }
                Console.WriteLine("");
                //Update
                var ford = new Car() {Name = "Ford"};
                context.Users.Add(new User() { Name = "John", Car = ford });
                context.SaveChanges();
                var lastUser = context.Users.ToList().Last();
                lastUser.Name = "Jons";
                context.Users.Update(lastUser);
                context.SaveChanges();
                //Delete
                context.Users.Add(new User()
                {
                    Name = "Jane", Car = ford,
                });
                context.SaveChanges();
                context.Users.Remove(context.Users.ToList().Last());
                context.SaveChanges();
                //Read
                foreach (var car in context.Car)
                {
                    Console.WriteLine($"{car.Users.Count}: {car.Name}");
                }
            }

            Console.ReadKey();
        }
    }

    


}