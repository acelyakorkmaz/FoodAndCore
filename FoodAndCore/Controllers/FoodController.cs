using FoodAndCore.Data.Models;
using FoodAndCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace FoodAndCore.Controllers
{
    public class FoodController : Controller
    {
        FoodRepository foodRespository = new FoodRepository();
        Context c = new Context();
        public IActionResult Index(int page=1)
        {
            
            return View(foodRespository.TList("Category").ToPagedList(page,3));
               
        }
        [HttpGet]
        public IActionResult AddFood()
        {
            List<SelectListItem> values = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            return View();
        }
        [HttpPost]
        public IActionResult AddFood(Food p)
        {
            foodRespository.TAdd(p);

            return RedirectToAction("Index");
            
        }
        public IActionResult DeleteFood(int id)
        {

            foodRespository.TDelete(new Food { FoodId = id });
            return RedirectToAction("Index");
        }
        public IActionResult FoodGet(int id)
        {
            List<SelectListItem> values = (from y in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.CategoryName,
                                               Value = y.CategoryID.ToString()
                                           }).ToList();
            ViewBag.v1 = values;

            var x = foodRespository.TGet(id);
            Food f = new Food()
            {
                CategoryID = x.CategoryID,
                Name=x.Name,
                Price=x.Price,
                Stock=x.Stock,
                Description=x.Description,
                ImageURL=x.ImageURL
                
                
            };
            return View(f);

        }
        [HttpPost]
        public IActionResult FoodUpdate(Food p)


        {

            //var x = foodRespository.TGet(p.FoodId);
            //x.CategoryID = p.CategoryID;
            //   x.Name = p.Name;
            //x.Stock = p.Stock;
            //x.Price = p.Price;
            //x.ImageURL=p.ImageURL;
            //x.Description = p.Description;

            //foodRespository.TUpdate(x);
            foodRespository.TUpdate(p);
            
            return RedirectToAction("Index");
        }
    }
}
