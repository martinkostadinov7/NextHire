namespace Business.Entities
{
    public class Cv
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public User User { get; set; }

        private Cv() { }

        public Cv(int userId)
        {
            UserId = userId;
        }
    }
}
