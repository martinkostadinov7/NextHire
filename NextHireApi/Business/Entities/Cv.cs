namespace Business.Entities
{
    public class Cv
    {
        public int Id { get; set; }

        public string Summary { get; set; }

        public string Experience { get; set; }

        public string Skills { get; set; }

        public int UserId { get; set; }
        
        public User User { get; set; }

        private Cv() { }

        public Cv(string summary, string experience, string skills, int userId)
        {
            Summary = summary;
            Experience = experience;
            Skills = skills;
            UserId = userId;
        }
    }
}
