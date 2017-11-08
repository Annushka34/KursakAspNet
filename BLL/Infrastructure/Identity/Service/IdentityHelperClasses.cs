using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Identity.Service
{
    public class ApplicationUserStore : UserStore<AppUser>
    {
        public ApplicationUserStore(DAL.Entities.AppDBContext context)
        : base(context)
        {
        }
    }

    public class ApplicationRoleStore : RoleStore<AppRole>
    {
        public ApplicationRoleStore(DAL.Entities.AppDBContext context)
        : base(context)
        {
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            //MailMessage mailMessage = new MailMessage
            //{
            //    Subject = message.Subject,
            //    Body = message.Body,
            //    IsBodyHtml = true
            //};
            //mailMessage.To.Add(new MailAddress(message.Destination));
            //SmtpClient smtpClient = new SmtpClient();
            //smtpClient.SendAsync(mailMessage, null);
            //return Task.FromResult(0);
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

  
    }

