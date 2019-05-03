using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PostPita.Models
{
    public enum UserType
    {
        Admin = 0,
        Company = 1,
        Employee = 2,
        //InWait = 3
    }

    public class ApplicationUser : IdentityUser
    {
        public UserType UserType { get; set; }

        public CompanyUser CompanyUser { get; set; }
        public ApplicantUser ApplicantUser { get; set; }

        public List<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
