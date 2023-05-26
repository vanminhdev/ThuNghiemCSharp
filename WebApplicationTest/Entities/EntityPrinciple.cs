using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationTest.Entities
{
    [Table(nameof(EntityPrinciple))]
    public class EntityPrinciple
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public List<EntityDependent> EntityDependents { get; } = new();
        public List<EntityDependent2> EntityDependent2s { get; } = new();
    }
}
