using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FirstMidName { get; set; }
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        public String Email { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }

        public string Name
        {
            get { return FirstMidName + ' ' + LastName; }
        }

    }


}
