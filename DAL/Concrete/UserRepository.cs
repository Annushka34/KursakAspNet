using DAL.Abstract;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
   public class UserRepository : SqlRepository, IUserRepository
    {
        public UserRepository(IAppDBContext context)
           : base(context)
        {

        }
        public User Add(User user)
        {
            this.Insert(user);
            this.SaveChanges();
            return user;
        }
        
    }
}
