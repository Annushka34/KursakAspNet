using BLL.Infrastructure.Identity.Service;
using BLL.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IAccountProvider
    {
        StatusAccountViewModel Login(LoginViewModel model);
        Task<SignInStatus> LoginAsync(LoginViewModel model);
        void Logout();
        Task<IdentityResult> Register(RegisterViewModel model);

        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo);
      
    }
}
