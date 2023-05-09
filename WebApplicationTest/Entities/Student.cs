using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationTest.Entities
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string? Name { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public List<Classroom> Classrooms { get; } = new();
        public List<StudentClassroom> StudentClassrooms { get; } = new();
        public List<Hobby> Hobbies { get; } = new();
    }
}
