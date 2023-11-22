using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class AddressService
    {
        private readonly AddressRepository _addressRepository;

        public AddressService(AddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<int> CreateAddressAsync(Address address)
        {
            return await _addressRepository.AddAddressAsync(address);
        }

        public async Task<Address> GetAddressAsync(int addressId)
        {
            return await _addressRepository.GetAddressAsync(addressId);
        }

        public async Task UpdateAddressAsync(Address address)
        {
            var existingAddress = await _addressRepository.GetAddressAsync(address.AddressID);
            if (existingAddress == null)
            {
                throw new KeyNotFoundException("Address not found");
            }
            await _addressRepository.UpdateAddressAsync(address);
        }

        public async Task DeleteAddressAsync(int addressId)
        {
            var existingAddress = await _addressRepository.GetAddressAsync(addressId);
            if (existingAddress == null)
            {
                throw new KeyNotFoundException("Address not found.");
            }

            await _addressRepository.DeleteAddressAsync(addressId);
        }
    }
}
