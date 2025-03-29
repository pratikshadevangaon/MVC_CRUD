using MVC_CRUD_scaffolding.Data;
using MVC_CRUD_scaffolding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CRUD_scaffolding.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index(int page = 1, int pageSize = 2)
        {
            // Get total count of products
            int totalRecords = db.Products.Count();

            // Calculate total pages
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            // Ensure page number is within bounds
            page = Math.Max(1, Math.Min(page, totalPages));
             

            var products = db.Products.Include("Category").OrderBy(p => p.ProductId)  // Order by ID for consistent paging
            .Skip((page - 1) * pageSize)  // Skip previous pages' records
            .Take(pageSize).ToList();

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            return View(products);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = db.Products.Find(product.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.CategoryId = product.CategoryId;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var product = db.Products.Include("Category").FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}