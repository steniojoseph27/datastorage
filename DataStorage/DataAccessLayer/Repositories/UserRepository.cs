using DataAccessLayer.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class UserRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(string connectionString, ILogger<UserRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<int> AddUserAsync(User user)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                string sql = "INSERT INTO Users (FirstName, LastName, Age, DateOfBirth, AddressID) VALUES (@FirstName, @LastName, @Age, @DateOfBirth, @AddressID); SELECT SCOPE_IDENTITY();";

                using var command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                command.Parameters.AddWithValue("@AddressID", user.AddressID);

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error adding user: {FirstName} {Last}", user.FirstName, user.LastName);
                throw;
            }
        }

        public async Task<User> GetUserAsync(int userId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                string sql = "SELECT * FROM Users WHERE UserID = @UserID";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new User
                    {
                        UserID = reader.GetInt32("UserID"),
                        FirstName = reader.GetString("FirstName"),
                        LastName = reader.GetString("LastName"),
                        Age = reader.GetInt32("Age"),
                        DateOfBirth = reader.GetDateTime("DateOfBirth"),
                        AddressID = reader.GetInt32("AddressID")
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID: {UserId}", userId);
                throw;
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                string sql = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Age = @Age, DateOfBirth = @DateOfBirth, AddressID = @AddressID WHERE UserID = @UserID";
                using var command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                command.Parameters.AddWithValue("@AddressID", user.AddressID);

                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error updating user with ID: {UserId}", user.UserID);
                throw;
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                string sql = "DELETE FROM Users WHERE UserID = @UserID";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error deleting user with ID: {UserId}", userId);
                throw;
            }
        }
    }
}
