using System;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.DTOs
{
    public class AddressDTO : BaseDTO
    {
        [Required]
        [StringLength(255)]
        public string Line1 { get; set; }

        [StringLength(255)]
        public string Line2 { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        [Required]
        [StringLength(255)]
        public string Country { get; set; }

        [Required]
        [StringLength(255)]
        public string State { get; set; }

        [Required]
        [StringLength(255)]
        public string Zipcode { get; set; }
    }
}
