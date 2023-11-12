using System.ComponentModel.DataAnnotations;

namespace PlayersService.Models
{
    public class Player
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}