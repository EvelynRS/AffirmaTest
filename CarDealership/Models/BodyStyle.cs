using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealership.Models
{
    [Table("BodyStyle")]
    public class BodyStyle : LookupBase
    {
        [NotMapped]
        public const string SUV = "SUV";

        [NotMapped]
        public const string Sedan = "Sedan";

        [NotMapped]
        public const string PickupTruck = "Pickup Truck";

        [NotMapped]
        public const string Coupe = "Coupe";

        [NotMapped]
        public const string Hatchback = "Hatchback";

        [NotMapped]
        public const string Convertible = "Convertible";

        [NotMapped]
        public const string Wagon = "Wagon";

        [NotMapped]
        public const string Minivan = "Minivan";
    }

    public enum BodyStyles
    {
        [Display(Name = "SUV")]
        SUV = 0,

        [Display(Name = "Sedan")]
        Sedan = 1,

        [Display(Name = "Pickup Truck")]
        PickupTruck = 2,

        [Display(Name = "Coupe")]
        Coupe = 4,

        [Display(Name = "Hatchback")]
        Hatchback = 5,

        [Display(Name = "Convertible")]
        Convertible = 6,

        [Display(Name = "Wagon")]
        Wagon = 7,

        [Display(Name = "Minivan")]
        Minivan = 8,
    }
}
