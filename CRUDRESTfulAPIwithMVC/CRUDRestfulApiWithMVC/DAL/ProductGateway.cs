using CRUDRestfulApiWithMVC.Models;
using CRUDRestfulApiWithMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUDRestfulApiWithMVC.DAL
{
    public class ProductGateway
    {
        private readonly CustomerDBEntities _db = new CustomerDBEntities();


        public List<Product> GetAllProduct()
        {
            return _db.Products.ToList();
        }
        public string SaveProduct(ProductViewModel productViewModel)
        {
            Product product = new Product()
            {
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                Quantity = productViewModel.Quantity,

            };
            if (product != null)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return " Save Sucessfully ";
            }
            return "Failed ";


        }
        public Product GetProduct(int id)
        {
            var product = _db.Products.Where(p => p.Id == id).FirstOrDefault();
            return product;


        }
        public Product UpdateProduct( ProductViewModel productViewModel)
        {

            Product product = _db.Products.Where(u => u.Id == productViewModel.Id).FirstOrDefault();
            if (product != null)
            {
                product.Name = productViewModel.Name;
                product.Price = productViewModel.Price;
                product.Quantity = productViewModel.Quantity;

                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();


            }



            return product;
        }
        public string Delete(int id)
        {
            string msg = "";
            if (id > 0)
            {
                var product = _db.Products
                   .Where(u => u.Id == id)
                   .FirstOrDefault();

                _db.Entry(product).State = EntityState.Deleted;
                _db.SaveChanges();
                return msg = "Delete success";
            }


            return msg = "Delete failed";
        }
    }
}