
using DataLayer.Entities;
using DataLayer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ShipManagemntDbContext _context;

        public UserRepository(ShipManagemntDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.UserDefinition.ToList();
        }

        public User GetByEmail(string email)
        {
            var data = _context.UserDefinition.Where(user => user.Email == email).FirstOrDefault();

            return data;
        }
    }
}
