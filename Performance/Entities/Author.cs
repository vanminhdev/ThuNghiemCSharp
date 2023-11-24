using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Performance.Entities
{
    [Table(nameof(Author))]
    [Index(nameof(Deleted), nameof(Name), IsUnique = false, Name = $"IX_{nameof(Author)}")]
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)]
        public required string Name { get; set; }

        public bool Deleted { get; set; }

        public List<Book> Books { get; } = new();
    }
}
