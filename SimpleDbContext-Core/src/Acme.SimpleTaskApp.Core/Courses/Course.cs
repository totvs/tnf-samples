using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Domain.Entities;

namespace Acme.SimpleTaskApp.Courses
{
    [Table("Courses")] 
    public class Course : Entity
    {
        public virtual string CourseName { get; set; }

        public Course(){ }

        public Course(string courseName)
        {
            CourseName = courseName;
        }
    }
}
