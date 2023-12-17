using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApplication
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        public string? PickupTime { get; set; }

        public string? StartLocation { get; set; }
        public string? Destination { get; set; }

        public required string CarName { get; set; }

        public required string User {  get; set; }


    }
}
