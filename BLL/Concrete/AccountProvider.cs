using BLL.Abstract;
using BLL.Infrastructure.Identity.Service;
using BLL.ViewModel;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Concrete
{
    public class AccountProvider:IAccountProvider
    {
        private SignInService _signInManager;
        private UserService _userManager;
        private readonly IAuthenticationManager _authManager;

        //public AccountProvider(IUserRepository userRepository)
        //{

        //    //_userRepository = userRepository;
        //}

        public SignInService SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<SignInService>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public UserService UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().Get<UserService>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        public StatusAccountViewModel Login(LoginViewModel model)
        {
            var result = SignInManager
                .PasswordSignIn(model.Email, model.Password, model.IsRememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return StatusAccountViewModel.Success;
            }
            return StatusAccountViewModel.Error;
        }

        public void Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

        }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return result;
        }

        public IEnumerable<string> UserRoles(string email)
        {
            throw new NotImplementedException();
            //var user= _userRepository.GetUserByEmail(email);
            //return user.Roles.Select(r=>r.Name);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _authManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo)
        {
            return await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        }


    }
}
