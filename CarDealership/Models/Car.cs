using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace CarDealership.Models
{

    public class Car : ModelBaseWithGuid
    {
        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        // CHANGED TO DECIMAL
        public decimal Price { get; set; }

        public int Horsepower { get; set; }

        public BodyStyles BodyStyle { get; set; }

        public TransmissionTypes TransmissionType { get; set; }

        //
        // ADITTION FOR MPG
        //

        public int MilesPerGallon { get; set; }

        public override string ToString()
        {
            return $"{Year} {Make} {Model}";
        }

        public Car() { }

        public Car(System.Guid guid) : base(guid)
        { }
    }
}