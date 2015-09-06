using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tatabouf.DAL;
using Tatabouf.Business;
using Tatabouf.Domain;

namespace Tatabouf.Tests.Business
{
    [TestClass]
    public class FoodChoiceServiceTest : BaseTest
    {
        [TestInitialize]
        public void Initialize()
        {
            _user = new User
            {
                Id = _userId,
                Name = "lionel",
                IpAddress = "192.168.0.1"
            };            
        }

        [TestMethod]
        public void FoodChoiceService_AddUser()
        {
            // Arrange
            var repositoryMock = GetRepositoryMock();

            var service = new FoodChoiceService();
            service.FoodChoiceRepository = repositoryMock.Object;

            var user = new User
            {
                Name = "lionel",
                IpAddress = "192.168.0.1",
                AvailableSeats = 2,
                IHaveMyLunch = true
            };

            // Act
            service.AddUser(user);

            // Assert
            repositoryMock.Verify(r => r.Add(user));
        }

        [TestMethod]
        public void FoodChoiceService_FindUserById()
        {
            const int userId = 1;
            var repositoryMock = GetRepositoryMock();

            var service = new FoodChoiceService();
            service.FoodChoiceRepository = repositoryMock.Object;

            var user = service.FindUserById(userId);

            Assert.IsNotNull(user);
            repositoryMock.Verify(r => r.FindById(userId));
        }

        [TestMethod]
        public void FoodChoiceService_GetPlaces()
        {
            var repositoryMock = GetRepositoryMock();

            var service = new FoodChoiceService();
            service.FoodChoiceRepository = repositoryMock.Object;

            service.GetPlaces();
            repositoryMock.Verify(r => r.GetPlaces());
        }

        [TestMethod]
        public void FoodChoiceService_GetTodayUsersChoices()
        {
            var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var tomorrow = today.AddDays(1);

            var repositoryMock = GetRepositoryMock();

            var service = new FoodChoiceService();
            service.FoodChoiceRepository = repositoryMock.Object;
            var usersChoices = service.GetTodayUsersChoices();

            repositoryMock.Verify(r => r.GetUsersChoices(today, tomorrow));
        }

        [TestMethod]
        public void FoodChoiceService_FindPlacesByIds()
        {
            var repositoryMock = GetRepositoryMock();

            var service = new FoodChoiceService();
            service.FoodChoiceRepository = repositoryMock.Object;

            var selectedIdString = new string[] { "1", "2", "3", "4" };
            var selectedIdInt = new List<int> { 1, 2, 3, 4 };

            var result = service.FindPlacesByIds(selectedIdString);
            repositoryMock.Verify(r => r.FindPlacesByIds(selectedIdInt));
        }

        [TestMethod]
        public void FoodChoiceService_DeleteUser_Ok()
        {
            const int userId = 1;
            const string ipAddressToCompare = "192.168.0.1";

            var repositoryMock = GetRepositoryMock();

            var service = new FoodChoiceService();
            service.FoodChoiceRepository = repositoryMock.Object;

            service.DeleteUser(userId, ipAddressToCompare);

            repositoryMock.Verify(r => r.FindById(userId));
            repositoryMock.Verify(r => r.Delete(_user));
        }

        [TestMethod]
        public void FoodChoiceService_DeleteUser_IpAddressIncorrect()
        {
            const int userId = 1;
            const string ipAddressToCompare = "127.0.0.1";

            var repositoryMock = GetRepositoryMock();

            var service = new FoodChoiceService();
            service.FoodChoiceRepository = repositoryMock.Object;

            service.DeleteUser(userId, ipAddressToCompare);

            repositoryMock.Verify(r => r.FindById(userId));
            repositoryMock.Verify(r => r.Delete(_user), Times.Never);
        }

        [TestMethod]
        public void FoodChoiceService_DeleteUser_BadUserId()
        {
            const int userId = 99;
            const string ipAddressToCompare = "192.168.0.1";

            var repositoryMock = GetRepositoryMock();

            var service = new FoodChoiceService();
            service.FoodChoiceRepository = repositoryMock.Object;

            service.DeleteUser(userId, ipAddressToCompare);

            repositoryMock.Verify(r => r.FindById(userId));
            repositoryMock.Verify(r => r.Delete(_user), Times.Never);
        }

        [TestMethod]
        public void FoodChoiceService_UpdateUser_Ok()
        {
            const int userId = 1;
            var userUpdated = new User
            {
                Id = _userId,
                Name = "lionel",
                SelectedPlaces = new List<Place>{
                    new Place { Id = 1, Label = "Carrefour" },
                    new Place { Id = 1, Label = "Quick" }
                }
            };

            const string ipAddressToCompare = "192.168.0.1";

            var repositoryMock = GetRepositoryMock();

            var service = new FoodChoiceService();
            service.FoodChoiceRepository = repositoryMock.Object;

            service.UpdateUser(userUpdated, ipAddressToCompare);

            repositoryMock.Verify(r => r.FindById(userId));
            repositoryMock.Verify(r => r.Update(_user, userUpdated));
        }

        [TestMethod]
        public void FoodChoiceService_UpdateUser_IpAddressIncorrect()
        {
            const int userId = 1;
            var userUpdated = new User
            {
                Id = _userId,
                Name = "lionel",
                SelectedPlaces = new List<Place>{
                    new Place { Id = 1, Label = "Carrefour" },
                    new Place { Id = 1, Label = "Quick" }
                }
            };

            const string ipAddressToCompare = "127.0.0.1";

            var repositoryMock = GetRepositoryMock();

            var service = new FoodChoiceService();
            service.FoodChoiceRepository = repositoryMock.Object;

            service.UpdateUser(userUpdated, ipAddressToCompare);

            repositoryMock.Verify(r => r.FindById(userId));
            repositoryMock.Verify(r => r.Update(_user, userUpdated), Times.Never);
        }

        [TestMethod]
        public void FoodChoiceService_UpdateUser_BadUserId()
        {
            const int userId = 99;
            var userUpdated = new User
            {
                Id = userId,
                Name = "lionel",
                SelectedPlaces = new List<Place>{
                    new Place { Id = 1, Label = "Carrefour" },
                    new Place { Id = 1, Label = "Quick" }
                }
            };

            const string ipAddressToCompare = "192.168.0.1";

            var repositoryMock = GetRepositoryMock();

            var service = new FoodChoiceService();
            service.FoodChoiceRepository = repositoryMock.Object;

            service.UpdateUser(userUpdated, ipAddressToCompare);

            repositoryMock.Verify(r => r.FindById(userId));
            repositoryMock.Verify(r => r.Update(_user, userUpdated), Times.Never);
        }
    }
}