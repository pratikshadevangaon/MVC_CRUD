using MVC_CRUD_scaffolding.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_CRUD_scaffolding.Data
{
    public class ApplicationDbContext :DbContext
    {
        
        public ApplicationDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
    
}