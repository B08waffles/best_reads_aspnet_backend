using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace best_reads_backend.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(55)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(55)]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string? DOB { get; set; }

        public string? ImageUrl { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        [ForeignKey("BookId")]
        public virtual ICollection<Book>? Book { get; set; }

    }
}
