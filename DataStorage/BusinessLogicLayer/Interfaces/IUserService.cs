using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateUserAsync(User user);

        Task<User> GetUserAsync(int userId);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(int userId);
    }
}
