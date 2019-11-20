using MVCApplication.Infrastructure;
using MVCApplication.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MVCApplication.Controllers
{
    public class HomeController : Controller
    {
        //Home/Today
        public string Today()
        {
            return DateTime.Now.ToString();
        }
        //Home/Details
        public JsonResult Details()
        {
            var obj = new { id = 12345, Name = "Canarys", Location = "Bangalore" };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        CategoryRepository categoryRepository = new CategoryRepository();
        NorthWindContext productDB = new NorthWindContext();
        //Home/Index
        [HandleError(ExceptionType = typeof(Exception), View = "Error")]//to handle all the exceptions we need to add this
        public ActionResult Index()
        {
            var model = new CategoryProductViewModel
            {
                Categories = categoryRepository.GetAll().ToList(),
                Products = productDB.Products.ToList(),
                SelectedCategoryId = 0,
                SelectedCategoryName = string.Empty
            };
            return View(model);
        }

        [HttpPost]

        
        public ActionResult Index(FormCollection collection)
        {
            throw new Exception("Something went wrong");
           /* var id = Convert.ToInt32("0" + collection["SelectedCategoryId"]);
            var products = productDB.Products.ToList();
            var categories = categoryRepository.GetAll();
            var selectedCategory = categories.FirstOrDefault(c => c.CategoryID == id);
            var matchingProducts = products.Where(c => c.CategoryID == selectedCategory.CategoryID);
            var model = new CategoryProductViewModel
            {
                Categories = categories.ToList(),
                Products = matchingProducts.ToList(),
                SelectedCategoryId = selectedCategory.CategoryID,
                SelectedCategoryName = selectedCategory.CategoryName
            };
            return View(model);*/
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("home/customers/country/{country}")]
        public ActionResult GetCustomersByCountry(string country)
        {
            ViewBag.Message = "Customers By Country Page" + country;

            return View("contact");
        }

        [Route("home/about-us/{option}")]

        public ActionResult AboutUs(int option)
        {
            
           
            ViewData["ViewDataMessage"] = "This is from AboutUs Action-->ViewDataMessage";
            ViewBag.ViewBagMessage = "This is from About-us Action-->ViewBagMessage";
            TempData["TempDataMessage"] = "This is from About-Us Action-->TempDataMessage";
            Session["SessionMessage"] = "This is from About-Us Action-->SessionMessage";
            if (option == 0)
            {
                return View();
            }
            else

                return RedirectToAction("AnotherAboutUs");

        }

            [Route("home/another-about-us")]

        public ActionResult AnotherAboutUs()
        {
            return View();
        }
    }
}

