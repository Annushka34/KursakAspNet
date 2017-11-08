using Autofac;
using BLL.Concrete;
using BLL.Infrastructure.Identity.Service;
using DAL.Abstract;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace BLL.Infrastructure.Identity
{
    public class DataModule : Module
    {
        private string _connStr;
        private IAppBuilder _app;
        public DataModule(string connString, IAppBuilder app)
        {
            _connStr = connString;
            _app = app;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DAL.Entities.AppDBContext(this._connStr)).As<IAppDBContext>().InstancePerRequest();
            builder.Register(ctx =>
            {
                var context = (AppDBContext)ctx.Resolve<IAppDBContext>();
                return context;
            }).AsSelf().InstancePerDependency();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<AppUser>>().InstancePerRequest();
            builder.RegisterType<UserService>().AsSelf().InstancePerRequest();
            builder.RegisterType<RoleService>().AsSelf().InstancePerRequest();
            builder.RegisterType<SignInService>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => _app.GetDataProtectionProvider()).InstancePerRequest();
            builder.RegisterType<AccountProvider>().AsSelf().InstancePerRequest();

            //builder.RegisterType<SqlRepository>().As<ISqlRepository>().InstancePerRequest();
            //builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            base.Load(builder);
        }
    }
}
