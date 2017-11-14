using BLL.Infrastructure.Identity.Service;
using BLL.Infrastructure.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KursakAspNet3.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userManager;
        private readonly SignInService _signInManager;
        private readonly IAuthenticationManager _authManager;

        public HomeController(UserService userManager, SignInService signInManager, IAuthenticationManager authManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authManager = authManager;
        }

        public SignInService SignInManager
        {
            get
            {
                return _signInManager;
            }
        }

        public UserService UserManager
        {
            get
            {
                return _userManager;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _authManager;
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}