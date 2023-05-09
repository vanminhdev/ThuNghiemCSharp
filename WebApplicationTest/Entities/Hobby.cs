using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationTest.Entities
{
    [Table("Hobby")]
    public class Hobby
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public Student? Student { get; set; }

        [Required]
        [MaxLength(512)]
        public string Name { get; set; } = null!;
    }
}
