using MVCApplication.Infrastructure;
using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApplication.Controllers
{
    //[Authorize]//can be used on the controller or action methods
    public class CustomerController : Controller
    {

        IRepository<Customer, string> repository;

        public CustomerController()
        {
            repository = new CustomerRepository();
        }
        // GET: Customer
        public ActionResult Index()
        {
            var model = repository.GetAll();//Get all the customers and store it to model
            return View(model: model);//pass the model to the view
        }
        public ActionResult Details(string id)
        {
            var model = repository.GetDetails(identity: id);
            if (model != null)
                return View(model);
            else
                return RedirectToAction("Index");
        }
        [Authorize]//ask for the login when clicked on the edit (Customers)
        public ActionResult Edit(string id)
        {
            var model = repository.GetDetails(identity: id);
            if (model != null)
            {
                return View(model: model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        //this method is httpPost
        [HttpPost]
        //public ActionResult Edit()
        //public ActionResult Edit(FormCollection theForm)
        public ActionResult Edit(Customer Model)
        {
            /*string id = Request.Form["CustomerId"];
            string company= Request.Form["CompanyName"];
            string contact = Request.Form["ContactName"];
            string city = Request.Form["City"];
            string country = Request.Form["Country"];
            */

            //string id = theForm["CustomerId"];
            //string company = theForm["CompanyName"];
            //string contact = theForm["ContactName"];
            //string city = theForm["City"];
            //string country = theForm["Country"];
            ////Validate the Data
            //Customer obj = new Customer()
            //{
            //    CustomerID = id,
            //    CompanyName = company,
            //    ContactName = contact,
            //    City = city,
            //    Country = country
            //};

            repository.Update(Model);//throws NotImplementedException currently
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            var model = new Customer();
            return View(model: model);
        }
        [HttpPost]

        public ActionResult Create(Customer Model)
        {
            repository.CreateNew(Model);
            return RedirectToAction("Index");
            
        }

        public ActionResult Delete(string id)
        {
            var model = repository.GetDetails(identity: id);
            return View(model: model);
        }

        [HttpPost]

        public ActionResult DeleteConfirm(string id)
        {
            repository.delete(id);
            return RedirectToAction("Index");
        }

       
    }
}