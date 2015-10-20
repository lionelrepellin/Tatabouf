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

        [TestInitialize]
        public virtual void Initialize()
        {
            _user = new User
            {
                Id = 1,
                Name = "lionel",
                IpAddress = "192.168.0.1"
            };

            _userChoices = new List<User>
            {
                new User 
                { 
                    Id = 1, 
                    Name = "Lionel", 
                    AvailableSeats = 3, 
                    IpAddress = "192.168.0.1", 
                    Choices = new List<Choice>
                    { 
                        new Choice 
                        { 
                            Place = new Place 
                            { 
                                Label = "Carrefour", 
                                InputType = false 
                            } 
                        } 
                    }
                },
                new User 
                { 
                    Id = 2, 
                    Name = "Michel", 
                    AvailableSeats = 1, 
                    IpAddress = "192.168.0.5", 
                    Choices = new List<Choice>
                    { 
                        new Choice 
                        { 
                            Place = new Place 
                            { 
                                Label = "Carrefour"
                            }
                        },
                        new Choice 
                        {
                            Place = new Place 
                            {
                                Label = "Quick"
                            }
                        }
                    }
                }
            };
        }

        protected Mock<IFoodChoiceRepository> GetRepositoryMock()
        {
            var placesId = new List<int> { 1, 2 };

            var repositoryMock = new Mock<IFoodChoiceRepository>();
            repositoryMock.Setup(r => r.Add(It.IsAny<User>()));
            repositoryMock.Setup(r => r.Update(It.IsAny<User>(), It.IsAny<User>()));
            repositoryMock.Setup(r => r.Delete(It.IsAny<User>()));
            repositoryMock.Setup(r => r.GetPlaces()).Returns(It.IsAny<IEnumerable<Place>>());
            repositoryMock.Setup(r => r.GetUsersChoices(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(_userChoices);
            repositoryMock.Setup(r => r.FindById(_userId)).Returns(_user);
            repositoryMock.Setup(r => r.FindByIpAddress("192.168.0.1")).Returns(_user);

            return repositoryMock;
        }
    }
}
