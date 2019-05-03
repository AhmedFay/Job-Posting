using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostPita.Models
{
    public class RatingVM
    {
        public string empName { get; set; }
        public int INumber { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int Fq { get; set; }
        public int Aq { get; set; }
        public int Pq { get; set; } 
        public int Cq { get; set; }
        
        public string Desc { get; set; }    

    }
}
