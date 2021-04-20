using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class Dealership
    {
        [Required]
        public Address Address { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Car> Cars { get; set; }

        public Dealership()
        {
            Cars = new List<Car>();
        }
    }
}
