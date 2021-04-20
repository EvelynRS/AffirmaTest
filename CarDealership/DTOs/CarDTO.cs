using System.ComponentModel.DataAnnotations;

namespace CarDealership.DTOs
{
    public class CarDTO : BaseDTO
    {
        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Horsepower { get; set; }

        public int BodyStyle { get; set; }

        // Read-only
        public string BodyStyleName { get; set; }

        public int TransmissionType { get; set; }

        // Read-only
        public string TransmissionTypeName { get; set; }

        //
        // ADDITION
        //
        public int MilesPerGallon { get; set; }
    }
}
