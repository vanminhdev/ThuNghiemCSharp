using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationTest.Entities
{
    public class Classroom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string? Name { get; set; }

        public List<Student> Students { get; } = new();
        public List<StudentClassroom> StudentClasses { get; } = new();
    }
}
