using CRUDRestfulApiWithMVC.Models;
using CRUDRestfulApiWithMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUDRestfulApiWithMVC.DAL
{
    public class UserGateway
    {
        private readonly CustomerDBEntities _db = new CustomerDBEntities();


        public List<User> GetAllUser()
        {
            return _db.Users.ToList();
        }
        public string SaveUser(UserViewModel userViewModel)
        {
            User user = new User()
            {
                UserName = userViewModel.UserName,
                EmailAddress = userViewModel.EmailAddress,
                Password = userViewModel.Password,
                ConfirmPassword = userViewModel.ConfirmPassword
            };
            if (user != null)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return "Sucessfully Registration";
            }
            return "Failed Registration";


        }
        public User GetUser(int id)
        {
            var user = _db.Users.Where(u => u.Id == id).FirstOrDefault();
            return user;


        }
        public User UpdateUser(UserViewModel userViewModel)
        {

            User user = _db.Users.Where(u => u.Id == userViewModel.Id).FirstOrDefault();
            if (user != null)
            {
                user.UserName = userViewModel.UserName;
                user.EmailAddress = userViewModel.EmailAddress;
                user.Password = userViewModel.Password;
                user.ConfirmPassword = userViewModel.ConfirmPassword;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();


            }



            return user;
        }
        public string Delete(int id)
        {
            string msg = "";
            if (id > 0)
            {
                var user = _db.Users
                   .Where(u => u.Id == id)
                   .FirstOrDefault();

                _db.Entry(user).State = EntityState.Deleted;
                _db.SaveChanges();
                return msg = "Delete success";
            }


            return msg = "Delete failed";
        }
        public string Login(LoginViewModel loginViewModel)
        {
            string login = "failed";
            var data = _db.Users.Where(x => x.UserName == loginViewModel.UserName && x.Password == loginViewModel.Password);
            if (data != null)
            {
                return login = "Success";
            }
            return login;

        }
    }
}