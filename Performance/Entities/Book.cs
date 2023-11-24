using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Performance.Entities
{
    [Table(nameof(Book))]
    [Index(nameof(Deleted), nameof(Status), nameof(AuthorId), nameof(Title), IsUnique = false, Name = $"IX_{nameof(Book)}")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)]
        public required string Title { get; set; }

        [MaxLength(512)]
        public required string Description { get; set; }

        public int Status { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public int Deleted { get; set; }
    }
}
