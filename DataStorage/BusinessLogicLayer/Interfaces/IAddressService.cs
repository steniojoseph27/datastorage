using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAddressService
    {
        Task<int> CreateAddressAsync(Address address);

        Task<Address> GetAddressAsync(int addressId);

        Task UpdateAddressAsync(Address address);

        Task DeleteAddressAsync(int AddressId);
    }
}
