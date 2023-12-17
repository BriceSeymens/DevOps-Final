using System.ComponentModel.DataAnnotations;

namespace TaxiApplication
{
    public class LoggedInUser
    {
        [Key]
        public int LoginId { get; set; }
        public string? Name { get; set; }
    }
}