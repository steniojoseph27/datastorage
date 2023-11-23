using NUnit.Framework;
using Moq;
using DataAccessLayer;
using BusinessLogicLayer;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using DataAccessLayer.Entities;

namespace DataStorageTest
{
    [TestFixture]
    public class Tests
    {
        private Mock<UserRepository> _userRepositoryMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<UserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Test]
        public async Task CreateUserAsync_ShouldCallAddUserOnce()
        {
            var user = new User
            {
                FirstName = "Stenio",
                LastName = "Joseph",
                Age = 33,
                DateOfBirth = DateTime.Now
            };

            await _userService.CreateUserAsync(user);

            _userRepositoryMock.Verify(repo => repo.AddUserAsync(It.IsAny<User>()), Times.Once);
        }
    }
}