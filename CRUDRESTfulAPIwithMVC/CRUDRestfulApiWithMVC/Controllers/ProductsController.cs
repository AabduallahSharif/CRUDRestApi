using CRUDRestfulApiWithMVC.Models;
using CRUDRestfulApiWithMVC.Models.ViewModels;
using CRUDRestfulApiWithMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CRUDRestfulApiWithMVC.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Users
        ServiceRepository serviceObj = new ServiceRepository();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult List(int id = 0)
        {
            try

            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/products/getallproduct?id=" + id);
                response.EnsureSuccessStatusCode();
                List<ProductViewModel> userViewModels = response.Content.ReadAsAsync<List<ProductViewModel>>().Result;
                ViewBag.Title = "All Products";
                return View(userViewModels);
            }
            catch (Exception)
            {
                throw;
            }


        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Id = id;

            HttpResponseMessage response = serviceObj.PutResponse("api/products/UpdateProduct", productViewModel);
            response.EnsureSuccessStatusCode();
            Product product = response.Content.ReadAsAsync<Product>().Result;

            return View(product);
        }

        [HttpPost]
        //[HttpPost]  
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            int id = 0;
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PutResponse("api/products/UpdateProduct", productViewModel);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("List");
        }
       

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {

                HttpResponseMessage response = serviceObj.PostResponse("api/products/SaveProduct", productViewModel);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("List");

            }




            return View(productViewModel);
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = serviceObj.DeleteResponse("api/products/Delete?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("List");
        }
    }
}