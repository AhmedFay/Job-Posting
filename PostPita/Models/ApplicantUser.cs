using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostPita.Models
{
    public class ApplicantUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Specialization { get; set; }
        public string Skill { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gpa { get; set; }    
        public string CvPath { get; set; }  

        [ForeignKey("User")]
        public string UserId { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
    }
}
