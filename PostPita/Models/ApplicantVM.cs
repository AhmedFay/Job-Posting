using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PostPita.Models
{
    public class ApplicantVM
    {
        //public int Id { get; set; }
        public string FullName { get; set; }
        public int INumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //new
        public string City { get; set; }
        //new 2
        public int Gpa { get; set; }    

        public string Specialization { get; set; }
        public string Skill { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        //CV File
        //[FileExtensions(Extensions = "pdf")]
        public IFormFile Cv { get; set; }

    }
}
