using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace best_reads_backend.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(55)]
        public string? Title { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "Title is required and cannot exceed 55 characters.")]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(25)]
        public string? Published { get; set; }

        public string? ImageURL { get; set; }

        [ForeignKey("AuthorId")]
        public virtual ICollection<Author>? Author { get; set; }
    }
}
