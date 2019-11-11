using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Models
{
    public class Reviewer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage = "First Name cannot be more than 100 Characters")]
        public string FirstName { get; set; }
        [MaxLength(100, ErrorMessage = "Last Name cannot be more than 100 Characters")]
        public string LastName { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
