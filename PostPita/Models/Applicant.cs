using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PostPita.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //new
        public string City { get; set; }

        public string Specialization { get; set; }
        public string Skill { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        //new 2
        public int Gpa { get; set; }    

        //new 3
        public string CvPath { get; set; }  

        [Required]
        public Post post { get; set; }

        public bool isAccepted { get; set; }
        public bool isInBlackList { get; set; }

        public Applicant()
        {
        }
        public Applicant(ApplicantVM applicantVm, Post postX)
        {
            FullName = applicantVm.FullName;
            IdNumber = applicantVm.INumber;
            PhoneNumber = applicantVm.PhoneNumber;
            Email = applicantVm.Email;
            City = applicantVm.City;
            Specialization = applicantVm.Specialization;
            Skill = applicantVm.Skill;
            Gender = applicantVm.Gender;
            DateOfBirth = applicantVm.DateOfBirth;
            post = postX;
            Gpa = applicantVm.Gpa;
        }
    }
}
