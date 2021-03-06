﻿using System.Collections.Generic;
using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Api.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public Grade? Grade { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public IEnumerable<Instructor> Instructors { get; set; }

        public Department Department { get; set; }
    }
}
