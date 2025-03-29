using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CRUD_scaffolding.Models
{
    public class Product
    {
        public int ProductId { get; set; }  // Primary Key
        public string ProductName { get; set; }  // Name of the product
        public int CategoryId { get; set; }  // Foreign Key

        // Navigation property
        public virtual Category Category { get; set; }
    }
}