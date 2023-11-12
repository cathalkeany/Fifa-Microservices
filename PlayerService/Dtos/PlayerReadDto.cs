namespace PlayersService.Dtos
{
    public class PlayerReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public int ClubId { get; set; }
    }
}