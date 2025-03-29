using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_CRUD_scaffolding.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }  // Primary Key
        public string CategoryName { get; set; }  // Name of the category
        public string Description { get; set; }  // Optional description

        // Navigation property for related Products
        public virtual ICollection<Product> Products { get; set; }
    }
}