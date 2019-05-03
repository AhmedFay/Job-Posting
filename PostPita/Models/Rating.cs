using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostPita.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public string empName { get; set; }
        public int INumber { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int Fq { get; set; }
        public int Aq { get; set; }
        public int Pq { get; set; }
        public int Cq { get; set; }

        public string Desc { get; set; }

        public int Avg => ((Fq + Aq + Pq + Cq) / 4);

        [Required]
        public CompanyUser Company { get; set; }

        public Rating()
        {
        }

        public Rating(RatingVM ratingVm, CompanyUser company)
        {
            empName = ratingVm.empName;
            INumber = ratingVm.INumber;
            Start = ratingVm.Start;
            End = ratingVm.End;
            Fq = ratingVm.Fq;
            Aq = ratingVm.Aq;
            Pq = ratingVm.Pq;
            Cq = ratingVm.Cq;
            Desc = ratingVm.Desc;

            Company = company;
        }
    }
}
