using System;
using System.ComponentModel.DataAnnotations;

namespace Steppenwolf.Pages.Admin.Category
{
    public class CategoryModel
    {
        public Guid? Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Slug { get; set; }
    }
}