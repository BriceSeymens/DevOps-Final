using System.ComponentModel.DataAnnotations;

namespace TaxiApplication
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
    }
}

