using System.ComponentModel.DataAnnotations;

namespace Mission08Group13.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }
}
