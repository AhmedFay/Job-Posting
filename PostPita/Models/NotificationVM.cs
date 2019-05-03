using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostPita.Models
{
    public class NotificationVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
