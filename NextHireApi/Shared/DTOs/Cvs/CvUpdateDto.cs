using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Cvs
{
    public class CvUpdateDto
    {
        public int UserId { get; set; }
        public string Summary { get; set; }

        public string Experience { get; set; }

        public string Skills { get; set; }
    }
}
