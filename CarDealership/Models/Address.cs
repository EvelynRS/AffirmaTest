using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealership.Models
{
    public class Address : ModelBaseWithGuid
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

        public override string ToString()
        {
            return $"{Line1}, {City}, {State} {Zipcode}";
        }
    }
}
