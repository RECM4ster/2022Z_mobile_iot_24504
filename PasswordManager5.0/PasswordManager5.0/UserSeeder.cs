using PasswordManager5._0.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager5._0
{
    public class UserSeeder
    {
        private readonly PasswordManagerDbContext _dbContext;

        public UserSeeder(PasswordManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Users.Any())
                {
                    var user = GetUsers();
                    _dbContext.Users.AddRange(user);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Username="GrzegorzAdmin",
                    Password="ZAQ!2wsx",
                    Email="grzegorz@admin.com",
                    UserHash="CA3E0B0280E2762EC5304F05B09DA6C446DBD2648754A7C63365111F69C8E921E00A6AC87292510104442FD97FE7EC60A7D1F7443DF655C25A6117F24B9AA28A"

                },
                new User()
                {
                    Username="BartekAdmin",
                    Password="zaq1@WSX",
                    Email="bartek@admin.com",
                    UserHash="2D9DC37F5C0E9CD04DAAF6AF5F740EC57BB327512D24648A4670644EF4F5B2622D8522A2E9C2E6FD756D4150C18285604F76E66CDEF00DD5240BEC55C5355C51"
                }
            };
            return users;
        }


    }
}
