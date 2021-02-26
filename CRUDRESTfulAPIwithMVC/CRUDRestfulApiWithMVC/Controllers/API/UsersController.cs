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
    public class UsersController : ApiController
    {
        CustomerDBEntities _db = new CustomerDBEntities();
        UserGateway _userGateway = new UserGateway();

        [HttpGet]
        public JsonResult<List<User>> GetAllUser()

        {
            List<User> userList = new List<User>();

            userList = _userGateway.GetAllUser();

            return Json(userList);
        }
        [HttpPost]
        public string SaveUser(UserViewModel userViewModel)
        {
            string msg = _userGateway.SaveUser(userViewModel);
            return msg;
        }

        [HttpPut]
        public User UpdateUser(UserViewModel userViewModel)
        {
            if (userViewModel.UserName == null)
            {
                return _userGateway.GetUser(userViewModel.Id);
            }


            return _userGateway.UpdateUser(userViewModel);
        }


        [HttpDelete]
        public string Delete(int id)
        {

            string msg = _userGateway.Delete(id);
            return msg;
        }
    }
}
