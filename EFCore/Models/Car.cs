using System.ComponentModel.DataAnnotations;

namespace ADO.NET_EF_Test_Aplication.EFCore.Models
{public class Car
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
        
    public ICollection<User> Users { get; set; }
}}