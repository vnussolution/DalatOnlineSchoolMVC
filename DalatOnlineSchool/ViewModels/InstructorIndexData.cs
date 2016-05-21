using System.Collections;
using System.Collections.Generic;
using DalatOnlineSchool.Models;

namespace DalatOnlineSchool.ViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    
    }
}