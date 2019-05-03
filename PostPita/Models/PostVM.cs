using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostPita.Models
{
    public class PostVM
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public State State { get; set; }

        [Display(Name = "Require Skills")]
        public string RequireSkills { get; set; }

        [Display(Name = "Contract Months")]
        public int ContractMonths { get; set; }

        [Required]
        [Display(Name = "HourType")]
        public HourType HourType { get; set; }

        [Display(Name = "Salary")]
        public int? Salary { get; set; }

        [Required(ErrorMessage = "Date Required")]
        [Display(Name = "DeadLine")]
        [DataType(DataType.Date)]
        public DateTime? DeadLine { get; set; }


        public PostVM()
        {
            
        }
        public PostVM(Post post)
        {
            Title = post.Title;
            Description = post.Description;
            RequireSkills = post.RequireSkills;
            ContractMonths = post.ContractMonths;
            HourType = post.HourType;
            Salary = post.Salary;
            DeadLine = post.DeadLine;
            City = post.City;
            State = post.State;
        }

    }
}
