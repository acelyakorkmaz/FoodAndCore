using FoodAndCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodAndCore.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FoodAndCore.Controllers
{
  [Authorize]
    public class CategoryController : Controller
    {
        CategoryRepository categoryRespository = new CategoryRepository();

        //[]
        public IActionResult Index()
        {
            return View(categoryRespository.TList());
        }
        [HttpGet]
        public IActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryAdd(Category p)
        {
            if (!ModelState.IsValid)
            {
                return View("CategoryAdd");
            }
            categoryRespository.TAdd(p);

            return RedirectToAction("Index");
        }
        public IActionResult CategoryGet(int id)
        {
            var x = categoryRespository.TGet(id);
            Category ct = new Category()
            {
                CategoryName = x.CategoryName,
                CategoryDescription=x.CategoryDescription,
                CategoryID=x.CategoryID


            };
            return View(ct);
           
        }
        [HttpPost]
        public IActionResult CategoryUpdate(Category p)
        {
            var x = categoryRespository.TGet(p.CategoryID);
            x.CategoryName = p.CategoryName;
            x.CategoryDescription = p.CategoryDescription;
            x.Status = true;
            categoryRespository.TUpdate(x);
            return RedirectToAction("Index");
        }
        public IActionResult CategoryDelete(int id)
        {
            var x = categoryRespository.TGet(id);
            x.Status = false;
            categoryRespository.TUpdate(x);
            return RedirectToAction("Index");
        }
    }
}
