using System.ComponentModel.DataAnnotations;

namespace PlayersService.Dtos
{
    public class PlayerCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Position { get; set; }
    }
}