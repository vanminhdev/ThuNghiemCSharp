using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Performance.Entities
{
    [Table(nameof(Student))]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)]
        public required string Name { get; set; }

        public bool Deleted { get; set; }

        public List<Classroom> Classrooms { get; } = new();
    }
}
