using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Performance.Entities
{
    [Table(nameof(StudentClassroom))]
    [Index(nameof(StudentId), nameof(ClassroomId), IsDescending = [false, false], IsUnique = false, Name = $"IX_{nameof(StudentClassroom)}")]
    public class StudentClassroom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; } = null!;
    }
}
