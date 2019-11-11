using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "HeadLine must be between 10 and 200 Characters")]
        public string HeadLine { get; set; }
        [Required]
        [StringLength(2000, MinimumLength = 200, ErrorMessage = "ReviewText must be between 200 and 2000 Characters")]
        public string ReviewText { get; set; }
        public int  Rating { get; set; }

        public virtual Reviewer Reviewer { get; set; }
        public virtual Book Book { get; set; }
    }
}
