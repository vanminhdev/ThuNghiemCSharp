using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationTest.Entities
{
    [Table(nameof(EntityDependent))]
    public class EntityDependent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public int? EntityPrincipleId { get; set; }
        public EntityPrinciple? EntityPrinciple { get; set; }
    }
}
