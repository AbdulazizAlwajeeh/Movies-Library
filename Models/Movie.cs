using System.ComponentModel.DataAnnotations;

namespace MyMVCApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        [MinLength(100,ErrorMessage ="Must be at least 100 characters.")]
        public string? Description { get; set; }
        [Required]
        public string? Image { get; set; }
    }
}
