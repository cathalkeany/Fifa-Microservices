using System.ComponentModel.DataAnnotations;

namespace ClubService.Dtos
{
    public class ClubCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; } 
        [Required]
        public string League { get; set; }
    }
}