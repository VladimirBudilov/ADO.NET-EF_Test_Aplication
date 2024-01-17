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

    


}