using BusinessLogicLayer.Helper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _userRepository.GetUserAsync(userId);
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _userRepository.GetUserAsync(user.UserID);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var existingUser = await _userRepository.GetUserAsync(userId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            
            await _userRepository.DeleteUserAsync(userId);
        }
    }
}
