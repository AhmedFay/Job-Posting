using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostPita.Models
{
    public class Post
    {
        public Post()
        {
        }

        public Post(PostVM postVm, CompanyUser company)
        {
            Title = postVm.Title;
            Description = postVm.Description;
            RequireSkills = postVm.RequireSkills;
            ContractMonths = postVm.ContractMonths;
            HourType = postVm.HourType;
            //Salary = postVm.Salary;
            Salary = postVm.Salary ?? default(int);

            //DeadLine = postVm.DeadLine;
            DeadLine = postVm.DeadLine ?? default(DateTime);

            Company = company;
            City = postVm.City;
            State = postVm.State;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string RequireSkills { get; set; }
        public int ContractMonths { get; set; }
        public HourType HourType { get; set; }
        public int Salary { get; set; }
        public DateTime PostingTime { get; set; }
        public DateTime DeadLine { get; set; }

        //new 6
        public PostStatus PostStatus { get; set; }

        [Required]
        public CompanyUser Company { get; set; }
        public List<Applicant> Applicants { get; set; }

        //new 2
        public PostModified Modified { get; set; }

        //new 3,4
        public string City { get; set; }
        public State State { get; set; }

        //to save the edit .. insert it in later version
        //public List<Modification> Modifications { get; set; }

    }
}
