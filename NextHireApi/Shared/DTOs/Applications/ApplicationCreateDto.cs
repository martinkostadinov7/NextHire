namespace Shared.DTOs.Applications
{
    public class ApplicationCreateDto
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Education { get; set; }

        public string CoverLetter { get; set; }

        public int CvId { get; set; }

        public DateTime SubmittedAt { get; set; }

        public int UserId { get; set; }

        public int OfferId { get; set; }
    }
}
