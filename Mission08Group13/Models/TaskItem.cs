using System;
using System.ComponentModel.DataAnnotations;

namespace Mission08Group13.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime? DueDate { get; set; }
        [Required]
        [Range(1,4)]
        public int Quadrant { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool Completed { get; set; } = false;
    }
}
