﻿using Bulky.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name== obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("","Name and Display order cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Added successfully"!;
                return RedirectToAction("Index");
            }
            return View();
            
        }

        public IActionResult Edit(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("", "Name and Display order cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated successfully"!;
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
             _db.SaveChanges();
            TempData["success"] = "Category Deleted successfully"!;
             return RedirectToAction("Index");
            
          

        }

    }
}
