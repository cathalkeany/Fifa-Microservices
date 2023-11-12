using System.ComponentModel.DataAnnotations;

namespace ClubService.Models
{
    public class Club
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; } 

        [Required]
        public string League { get; set; }
    }
}