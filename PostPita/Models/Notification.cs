using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostPita.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        [Required]
        public ApplicationUser User { get; set; }


        public bool IsReaded { get; set; }
        public string RedirectPath { get; set; }
    }
}
