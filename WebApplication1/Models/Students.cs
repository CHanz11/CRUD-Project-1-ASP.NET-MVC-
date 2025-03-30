using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Students
    {
        [Key] // This is our primary key
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, ErrorMessage = "Name Cannot Exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last name is Required")]
        [StringLength(50, ErrorMessage = "Last name Cannot Exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Grade is Required")]
        [Range(1, 100, ErrorMessage = "Grade must between 1 - 100.")]
        public int Grade { get; set; }

        // New fields to add
        public string LetterGrade { get; set; } = "N/A";

        [Precision(4, 2)]  // This specifies 4 total digits with 2 decimal places (e.g., 4.00)
        public decimal GPA { get; set; } = 0.0m;
    }
}
