using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PostPita.Models
{
    public class Constants
    {
        public static string[] PostStateColor = { "#0275d8", "#5cb85c", "#5bc0de", "#d9534f", "#f0ad4e", "#292b2c" };

        public static List<SelectListItem> GenderList = new List<SelectListItem>() {
            new SelectListItem()
            {
                Text = "Male",
                Value = "0"
            },
            new SelectListItem()
            {
                Text = "Female",
                Value = "1"
            }
        };

        public static List<SelectListItem> StateList = new List<SelectListItem>() {
            new SelectListItem()
            {
                Text = "Gaza",
                Value = "0"
            },
            new SelectListItem()
            {
                Text = "West Bank",
                Value = "1"
            }
        };

        public static List<SelectListItem> HourTypeList = new List<SelectListItem>() {
            new SelectListItem()
            {
                Text = "Full-Time",
                Value = "0"
            },
            new SelectListItem()
            {
                Text = "Part-Time",
                Value = "1"
            }
        };
    }

    public enum PostStatus
    {
        Wait = 0,
        Approved = 1,
        Modified = 2,
        Rejected = 3,
        Review = 4,
        Hidden = 5
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
    }

    public enum HourType
    {
        FullTime = 0,
        PartTime = 1,
    }

    public enum State
    {
        Gaza = 0,
        WestBank = 1,
    }

}
