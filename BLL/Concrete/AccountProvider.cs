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
        private readonly SignInService _signInManager;
        private readonly UserService _userManager;
        private readonly IAuthenticationManager _authManager;

        public AccountProvider(UserService userManager,
           SignInService signInManager,
           IAuthenticationManager authManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authManager = authManager;
        }

        public SignInService SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<SignInService>();
            }
        }

        public UserService UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().Get<UserService>();
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _authManager;
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
        public async Task<SignInStatus> LoginAsync(LoginViewModel model)
        {
            var result = await SignInManager
                .PasswordSignInAsync(model.Email, model.Password, model.IsRememberMe, shouldLockout: false);
            return result;
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

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _authManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo)

        {
            return await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        }

        public async Task<SignInStatus> ExternalSignInAsyncFacebook(ExternalLoginInfo loginInfo)

        {
            var user = new AppUser { UserName = loginInfo.Email, Email = loginInfo.Email };
            var result = await UserManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
                }
            }
            return await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        }
    }
}
