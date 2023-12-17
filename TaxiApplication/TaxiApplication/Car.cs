using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApplication
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public required string CarName { get; set; }

        public double Startprice { get; set; }

        public double PricePerKm { get; set; }

        public required string Type { get; set; }


    }
}
