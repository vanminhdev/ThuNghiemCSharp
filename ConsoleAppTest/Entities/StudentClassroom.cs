﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest.Entities
{
    public class StudentClassroom
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassroomId { get; set; }

        //public Student Student { get; set; } = null!;
        //public Classroom Classroom { get; set; } = null!;
    }
}
