using CRUDRestfulApiWithMVC.DAL;
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
    public class UsersController : Controller
    {
        // GET: Users
        ServiceRepository serviceObj = new ServiceRepository();
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            try

            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/users/getalluser?id=" + id);
                response.EnsureSuccessStatusCode();
                List<UserViewModel> userViewModels = response.Content.ReadAsAsync<List<UserViewModel>>().Result;
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
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Id = id;

            HttpResponseMessage response = serviceObj.PutResponse("api/users/UpdateUser", userViewModel);
            response.EnsureSuccessStatusCode();
            User user = response.Content.ReadAsAsync<User>().Result;

            return View(user);
        }

        [HttpPost]
        //[HttpPost]  
        public ActionResult Edit(UserViewModel userViewModel)
        {
            int id = 0;
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PutResponse("api/users/UpdateUser", userViewModel);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                string admin = "admin", password = "admin";
                if (loginViewModel.UserName == admin)
                {
                    loginViewModel.UserName = admin;
                    loginViewModel.Password = password;
                    UserGateway userGateway = new UserGateway();


                    var data = userGateway.Login(loginViewModel);
                    if (data == "Success")
                    {
                        //add session
                        Session["Admin"] = loginViewModel.UserName;
                        Session["Password"] = loginViewModel.Password;

                        return RedirectToAction("Index", "Products");
                    }
                    else
                    {
                        ViewBag.error = "Login failed";
                        return RedirectToAction("Login", "Users");
                    }
                }
                else
                {
                    UserGateway userGateway = new UserGateway();


                    var data = userGateway.Login(loginViewModel);
                    if (data == "Success")
                    {
                        //add session
                        Session["UserName"] = loginViewModel.UserName;
                        Session["Password"] = loginViewModel.Password;

                        return RedirectToAction("Index", "Products");
                    }
                    else
                    {
                        ViewBag.error = "Login failed";
                        return RedirectToAction("Login", "Users");
                    }
                }
            }
           
            
           
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {

                HttpResponseMessage response = serviceObj.PostResponse("api/users/SaveUser", userViewModel);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");

            }


            return View(userViewModel);
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = serviceObj.DeleteResponse("api/users/Delete?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
    }
}