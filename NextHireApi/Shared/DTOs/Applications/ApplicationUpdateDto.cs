using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Applications
{
    public class ApplicationUpdateDto
    {
        public int Id { get; set; }

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
