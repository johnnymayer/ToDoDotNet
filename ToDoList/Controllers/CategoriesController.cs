using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
    public class CategoriesController : Controller
    {

        private ToDoListDbContext _db;
        public CategoriesController(ToDoListDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> model = _db.Categories.ToList();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            Category thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            return View(thisCategory);
        }

        public IActionResult Delete(int id)
        {
            var thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            return View(thisCategory);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            _db.Categories.Remove(thisCategory);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View(thisCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _db.Entry(category).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
