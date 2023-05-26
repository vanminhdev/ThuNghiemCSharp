using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationTest.Entities
{
    [Table(nameof(EntityDependentLevel2))]
    public class EntityDependentLevel2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public int? EntityDependentId { get; set; }
        public EntityDependent? EntityDependent { get; set; }
    }
}
