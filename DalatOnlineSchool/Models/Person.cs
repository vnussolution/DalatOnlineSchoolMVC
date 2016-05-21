using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;


namespace DalatOnlineSchool.Models
{
    // implement inheritance
    public abstract class Person
    {
        public int ID { get; set; }

        [Required, StringLength(50), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, StringLength(50, ErrorMessage = "First name cannot be longer than 50c"), Column("FirstName"), Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }


    }
}