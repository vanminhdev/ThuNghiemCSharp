using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Performance.Entities
{
    [Table(nameof(Classroom))]
    //[Index(nameof(MaxStudent), /*nameof(Name),*/ IsUnique = false, Name = $"IX_{nameof(Classroom)}")]
    public class Classroom : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)]
        public required string Name { get; set; }

        public int MaxStudent { get; set; }

        public int Status { get; set; }

        public bool Deleted { get; set; }

        public List<Student> Students { get; } = new();
    }
}
