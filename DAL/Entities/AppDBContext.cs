using DAL.Abstract;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.Entities
{
    public class AppDBContext : IdentityDbContext<AppUser>,IEFContext
    {
        public AppDBContext() : base("IdentityDb") { }

        public AppDBContext(string connString)
            : base(connString)
        {
            Database.SetInitializer<AppDBContext>(new DBIitializer());
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        //public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
