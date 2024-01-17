using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADO.NET_EF_Test_Aplication.EFCore.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
        
    [ForeignKey("Car")]
    public int CarId { get; set; }
        
    public Car Car { get; set; }
}

