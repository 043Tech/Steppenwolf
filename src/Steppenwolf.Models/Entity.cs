using System;
using System.ComponentModel.DataAnnotations;

namespace Steppenwolf.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}