using System.ComponentModel.DataAnnotations;

namespace CarDealership.DTOs
{
    public class LookupDTO
    {
        [Required]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }
    }
}
