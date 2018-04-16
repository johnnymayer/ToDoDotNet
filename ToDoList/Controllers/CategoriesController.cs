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
    }
}
