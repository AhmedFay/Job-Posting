using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostPita.Models
{
    public class PostModified
    {
        public int Id { get; set; } 

        [ForeignKey("OldPost")]
        public int OldPostId { get; set; }
        [Required]
        public Post OldPost { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequireSkills { get; set; }
        public int ContractMonths { get; set; }
        public HourType HourType { get; set; }
        public int Salary { get; set; }

        public DateTime DeadLine { get; set; }

        public PostModified()
        {
            
        }
        public PostModified(PostVM post,Post p)
        {
            //Id = p.Id;
            OldPost = p;

            Title = post.Title;
            Description = post.Description;
            RequireSkills = post.RequireSkills;
            ContractMonths = post.ContractMonths;
            HourType = post.HourType;
            //Salary = post.Salary;
            Salary = post.Salary ?? default(int);

            //DeadLine = post.DeadLine;
            DeadLine = post.DeadLine ?? default(DateTime);

        }

    }
}
