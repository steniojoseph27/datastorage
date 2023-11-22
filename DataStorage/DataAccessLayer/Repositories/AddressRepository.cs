using DataAccessLayer.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class AddressRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<AddressRepository> _logger;

        public AddressRepository(string connectionString, ILogger<AddressRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<int> AddAddressAsync(Address address)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                string sql = "INSERT INTO Address (Province, City, CountryID) VALUES (@Province, @City, @CountryID); SELECT SCOPE_IDENTITY();";

                using var command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Province", address.Province);
                command.Parameters.AddWithValue("@City", address.City);
                command.Parameters.AddWithValue("@CountryID", address.CountryID);

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error adding address: {City} {Province}", address.City, address.Province);
                throw;
            }
        }

        public async Task<Address> GetAddressAsync(int addressId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                string sql = "SELECT * FROM Addresses WHERE AddressID = @AddressID";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@AddressID", addressId);

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Address
                    {
                        AddressID = reader.GetInt32("AddressID"),
                        Province = reader.GetString("Province"),
                        City = reader.GetString("City"),
                        CountryID = reader.GetInt32("CountryID")
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID: {AddressID}", addressId);
                throw;
            }
        }

        public async Task UpdateAddressAsync(Address address)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                string sql = "UPDATE Addresses SET Province = @Province, City = @City, CountryID = @CountryID WHERE AddressID = @AddressID";
                using var command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@AddressID", address.AddressID);
                command.Parameters.AddWithValue("@Province", address.Province);
                command.Parameters.AddWithValue("@City", address.City);
                command.Parameters.AddWithValue("@CountryID", address.CountryID);
                
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error updating user with ID: {AddressID}", address.AddressID);
                throw;
            }
        }

        public async Task DeleteAddressAsync(int addressId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                string sql = "DELETE FROM Addresses WHERE AddressID = @AddressID";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@AddressID", addressId);

                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error deleting user with ID: {AddressID}", addressId);
                throw;
            }
        }
    }

}
