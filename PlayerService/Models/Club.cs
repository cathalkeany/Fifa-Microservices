using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PlayersService.Models
{
    public class Club
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ExternalID { get; set; } 
        [Required]
        public string Name { get; set; }
        public IEnumerable<Player> Players { get; set; } = new List<Player>();
    }
}