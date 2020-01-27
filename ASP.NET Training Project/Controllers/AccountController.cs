using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_Training_Project.Models;
//using ASP.NET_Training_Project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Training_Project.Controllers
{
    public class AccountController : Controller
    {
        //These create the user, validates them, and adds them to the database
        private UserManager<AppUser> UserMgr { get; }
        private SignInManager<AppUser> SignInMgr { get; }

        public AccountController (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;       
        }

        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Login(Login user)
        {
            var result = await SignInMgr.PasswordSignInAsync(user.userName, user.password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Result = "result is: " + result.ToString();
            }
            return View();
        }

        public async Task<IActionResult> Register(User newUser)
        {
            try
            {
                ViewBag.Message = "User already registered";
                AppUser user = await UserMgr.FindByNameAsync(newUser.userName);
                if (user == null)
                {                    
                    user = new AppUser();
                    user.UserName = newUser.userName;
                    user.Email = newUser.eMail;
                    user.FirstName = newUser.firstName;
                    user.LastName = newUser.lastName;

                    IdentityResult result;

                    result = await UserMgr.CreateAsync(user, newUser.password);
                    
                    if(result.Succeeded)
                    {
                        ViewBag.Message = "User created!";
                    }
                    else
                    {
                        ViewBag.Message = result.Errors;
                    }


                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }
    }
}