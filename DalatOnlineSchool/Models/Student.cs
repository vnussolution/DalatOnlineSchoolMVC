using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DalatOnlineSchool.Models
{
    public class Student : Person
    {
        //public int ID { get; set; }
        //[Required]
        //[Display(Name = "Last Name")]
        //[StringLength(50)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "First character must be capitalized.")] // it requires first character is capital
        //public string LastName { get; set; }

        //[Display(Name = "First Name")]
        //[StringLength(50, ErrorMessage = "Firstname cannot be long than 50 charraters.")]
        //[Column("FirstName")]
        //public string FirstMidName { get; set; }

        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        //[Display(Name = "Full Name")]
        //public string FullName
        //{
        //    get { return LastName + ", " + FirstMidName; }
        //}


        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

       

        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}