using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    namespace DAL.IRepositories
    {
        public interface IUserRepository
        {
            Task<User?> GetByEmailAsync(string email);
            Task<User> CreateAsync(User user);
            Task<User> GetByIdAsync(int id);
            Task UpdateAsync(User user);
        }
    }
}
