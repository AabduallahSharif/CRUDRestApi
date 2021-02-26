using CRUDRestfulApiWithMVC.DAL;
using CRUDRestfulApiWithMVC.Models;
using CRUDRestfulApiWithMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace CRUDRestfulApiWithMVC.Controllers.API
{
    public class ProductsController : ApiController
    {
        CustomerDBEntities _db = new CustomerDBEntities();
       ProductGateway productGateway = new ProductGateway();

        [HttpGet]
        public JsonResult<List<Product>> GetAllProduct()

        {
            List<Product> products = new List<Product>();

            products = productGateway.GetAllProduct();

            return Json(products);
        }
        [HttpPost]
        public string SaveProduct(ProductViewModel productViewModel)
        {
            string msg = productGateway.SaveProduct(productViewModel);
            return msg;
        }

        [HttpPut]
        public Product UpdateProduct(ProductViewModel productViewModel)
        {
            if (productViewModel.Name == null)
            {
                return productGateway.GetProduct(productViewModel.Id);
            }


            return productGateway.UpdateProduct(productViewModel);
        }


        [HttpDelete]
        public string Delete(int id)
        {

            string msg = productGateway.Delete(id);
            return msg;
        }
    }
}
