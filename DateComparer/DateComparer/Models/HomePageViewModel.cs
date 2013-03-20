using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Validation.DateComparer; 

namespace DateComparer.Models
{
    public class HomePageViewModel
    {
        [Display(Order=0, Name="Full Name")]
        [Required(ErrorMessage="Full Name is Required")]
        public string Name { get; set; }

        [Display(Order=1, Name="Birthday") ]
        [DateComparer(MaxDateAddDaysFromNow=0, MinDateSelector="PropertyDate")] //Must be less than Now and Greater than the sibling property PropertyDate
        public DateTime BirthDate { get; set; }

        [HiddenInput(DisplayValue=false)]
        public string PropertyDate {
            get { return new DateTime(2013, 1, 1).ToString("G"); }
        }
    }
}