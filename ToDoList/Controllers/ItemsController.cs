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
    public class ItemsController : Controller
    {
        private ToDoListDbContext _db;
        public ItemsController(ToDoListDbContext db)
        {
            _db = db;
        }

        public IActionResult Delete(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
            _db.Items.Remove(thisItem);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View(_db.Items.Include(items => items.Category).ToList());
        }

        public IActionResult Details(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Done(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);

            thisItem.Done = !thisItem.Done;
            _db.Entry(thisItem).State = EntityState.Modified;

            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}