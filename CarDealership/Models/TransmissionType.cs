using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealership.Models
{
    [Table("TransmissionType")]
    public class TransmissionType : LookupBase 
    {
        [NotMapped]
        public const string Manual = "Manual Transmission";

        [NotMapped]
        public const string Automatic = "Automatic Transmission";

        [NotMapped]
        public const string CVT = "Continuously Variable Transmission";

        [NotMapped]
        public const string SemiAutomatic = "Semi-Automatic Transmission";
    }

    public enum TransmissionTypes
    {
        [Display(Name = "Manual Transmission")]
        Manual = 0,

        [Display(Name = "Automatic Transmission")]
        Automatic = 1,

        [Display(Name = "Continuously Variable Transmission")]
        CVT = 2,

        [Display(Name = "Semi-Automatic Transmission")]
        SemiAutomatic = 4
    }
}
