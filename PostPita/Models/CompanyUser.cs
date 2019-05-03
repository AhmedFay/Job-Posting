using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostPita.Models
{
    public class CompanyUser
    {
        public int Id { get; set; }
        public string CoName { get; set; }
        public string Logo { get; set; }
        public string Location { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }    
        [Required]
        public ApplicationUser User { get; set; }   

        public List<Post> Posts { get; set; }
        public List<Rating> Ratings { get; set; }
        //public List<ApplicantUser> Employees { get; set; }

        public bool IsAccepted { get; set; }
    }
}
