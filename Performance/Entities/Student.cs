using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Performance.Entities
{
    [Table(nameof(Student))]
    public class Student : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)]
        public required string Name { get; set; }

        [MaxLength(128)]
        [Unicode(false)]
        public string? StudentCode { get; set; }

        [MaxLength(128)]
        [Unicode(false)]
        public string? Phone { get; set; }

        [MaxLength(128)]
        [Unicode(false)]
        public string? Email { get; set; }

        /// <summary>
        /// Mã chuyên ngành học
        /// </summary>
        [MaxLength(50)]
        [Unicode(false)]
        public string? MajorCode { get; set; }

        /// <summary>
        /// Mã ngành học
        /// </summary>
        [MaxLength(50)]
        [Unicode(false)]
        public string? IndustryCode { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Deleted { get; set; }

        public List<Classroom> Classrooms { get; } = new();

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, StudentCode: {StudentCode}, Phone: {Phone}, Email: {Email}, " +
               $"MajorCode: {MajorCode}, IndustryCode: {IndustryCode}, DateOfBirth: {DateOfBirth}, " +
               $"CreatedDate: {CreatedDate}, Deleted: {Deleted}";
        }
    }
}
