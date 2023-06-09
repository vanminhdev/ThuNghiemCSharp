﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest.Entities
{
    public class Classroom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string? Name { get; set; }
    }
}
