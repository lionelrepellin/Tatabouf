using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tatabouf.DAL;
using Tatabouf.Domain;

namespace Tatabouf.Tests
{
    public abstract class BaseTest
    {
        protected readonly int _userId = 1;
        protected User _user;
        protected IEnumerable<User> _userChoices;
        protected IEnumerable<Place> _places;



        [TestInitialize]
        public void InitializeData()
        {
            _places = new List<Place>
            {
                new Place { Id = 1, Label = "Carrefour", DisplayOrder = 1 },
                new Place { Id = 2, Label = "Quick", DisplayOrder = 2 },
                new Place { Id = 3, Label = "Autre", DisplayOrder = 5 }
            };

            _userChoices = new List<User>
            {
                new User { Id = 10, Name = "Gérard", IpAddress = "192.168.0.2", IGotMyLunch = true },
                new User { Id = 11, Name = "Michel", IpAddress = "192.168.0.8", IGotMyLunch = true, AvailableSeats = 3 },
                new User { Id = 12, Name = "Jacques", IpAddress = "192.168.0.2", SelectedPlaces = _places.ToList() }
            };
        }


        protected Mock<IFoodChoiceRepository> GetRepositoryMock()
        {
            var placesId = new List<int> { 1, 2 };
            


            var repositoryMock = new Mock<IFoodChoiceRepository>();
            repositoryMock.Setup(r => r.Add(It.IsAny<User>()));
            repositoryMock.Setup(r => r.Delete(It.IsAny<User>()));
            repositoryMock.Setup(r => r.Update(It.IsAny<User>(), It.IsAny<User>()));
            repositoryMock.Setup(r => r.FindById(_userId)).Returns(_user);
            //repositoryMock.Setup(r => r.FindPlacesByIds(It.IsAny<List<int>>())).Returns(It.IsAny<IEnumerable<Place>>());
            repositoryMock.Setup(r => r.FindPlacesByIds(placesId)).Returns(_places);
            repositoryMock.Setup(r => r.GetPlaces()).Returns(It.IsAny<IEnumerable<Place>>());
            //repositoryMock.Setup(r => r.GetUsersChoices(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(It.IsAny<IEnumerable<User>>());
            repositoryMock.Setup(r => r.GetUsersChoices(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(_userChoices);
            return repositoryMock;
        }
    }
}
