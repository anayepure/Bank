using Microsoft.AspNetCore.Mvc;
using SE_Bank.Models;
using SE_Bank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SE_Bank.Controllers
{
    public class LogInController : Controller
    {
        string logInResult = "";
        public IActionResult Index()
        {
            ViewBag.LogInResult = logInResult;
            return View("LogInPage");
        }

        public IActionResult ProcessLogin(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();
            UserModel myUser = securityService.IsValid(userModel);
            if (myUser!=null)
            {
                if (myUser.IsAdmin == 0)
                {
                    UserActionsController userActionsController = new UserActionsController();
                    return userActionsController.Index(myUser);
                    //return View("UserPage", myUser);
                }
                else {
                    AdminActionsController adminActionsController = new AdminActionsController();
                    //adminActionsController.User = myUser;
                    return adminActionsController.Index(myUser);
                    //return View("AdminPage", myUser);
                }
            }
            else
            {
                logInResult = "Failed to log in";
                return Index();
                //return View("LoginFailure", userModel);
                
            }
        }
    }
}
