using DataAccessLayer.Entities;
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

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddUserAsync(User user)
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

        public async Task<User> GetUserAsync(int userId)
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

        public async Task UpdateUserAsync(User user)
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

        public async Task DeleteUserAsync(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string sql = "DELETE FROM Users WHERE UserID = @UserID";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@UserID", userId);

            await command.ExecuteNonQueryAsync();
        }
    }
}
