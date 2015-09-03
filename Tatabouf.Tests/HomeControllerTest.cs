using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatabouf.Controllers;
using Moq;
using Tatabouf.DAL;
using System.Web.Mvc;
using Tatabouf.Domain;
using Tatabouf.Models;
using Tatabouf.Util;

namespace Tatabouf.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;

        [TestInitialize]
        public void Initialize()
        {
            controller = new HomeController();
            controller.Repository = GetRepositoryMock();
        }

        [TestMethod]
        public void Controller_AddWithSameName()
        {
            var model = new ContainerModel
            {
                Crew = new CrewModel
                {
                    Carrefour = true,
                    Name = "BOB"
                }
            };

            var viewResult = (ViewResult)controller.Add(model);
            Assert.AreEqual("Le nom existe déjà !", GetErrorMessage(viewResult));
        }

        [TestMethod]
        public void Controller_AddWithNameButNoChoice()
        {
            var model = new ContainerModel
            {
                Crew = new CrewModel
                {
                    Name = "Gérard"
                }
            };

            var viewResult = (ViewResult)controller.Add(model);
            Assert.AreEqual("Merci de cocher au moins une case !", GetErrorMessage(viewResult));
        }


        [TestMethod]
        public void Controller_AddWithNameButToMuchChoices()
        {
            var model = new ContainerModel
            {
                Crew = new CrewModel
                {
                    Name = "Gérard",
                    Carrefour = true,
                    IGotIt = true
                }
            };

            var viewResult = (ViewResult)controller.Add(model);
            Assert.AreEqual("Si tatabouf, pourquoi aller chercher bonheur ailleurs ?", GetErrorMessage(viewResult));
        }

        [TestMethod]
        public void Controller_AddWithNameButToMuchChoicesAndSeats()
        {
            var model = new ContainerModel
            {
                Crew = new CrewModel
                {
                    Name = "Gérard",
                    IGotIt = true,
                    NumberOfSeatsAvailable = 4
                }
            };

            var viewResult = (ViewResult)controller.Add(model);
            Assert.AreEqual("Tatabouf ou tapatabouf ?", GetErrorMessage(viewResult));
        }

        [TestMethod]
        public void Controller_AddIGotIt()
        {
            var model = new ContainerModel
            {
                Crew = new CrewModel
                {
                    Name = "Gérard",
                    IGotIt = true
                }
            };

            var routeResult = (RedirectToRouteResult)controller.Add(model);
            var method = routeResult.RouteValues.First().Value.ToString();
            Assert.AreEqual("Index", method);
        }

        [TestMethod]
        public void Controller_AddOneOrMoreChoices()
        {
            var model = new ContainerModel
            {
                Crew = new CrewModel
                {
                    Name = "Gérard",
                    Carrefour = true,
                    MarieBlachere = true
                }
            };

            var routeResult = (RedirectToRouteResult)controller.Add(model);
            var method = routeResult.RouteValues.First().Value.ToString();
            Assert.AreEqual("Index", method);
        }

        [TestMethod]
        public void Controller_AddOneOrMoreChoicesAndSeats()
        {
            var model = new ContainerModel
            {
                Crew = new CrewModel
                {
                    Name = "Gérard",
                    Carrefour = true,
                    MarieBlachere = true,
                    NumberOfSeatsAvailable = 3
                }
            };

            var routeResult = (RedirectToRouteResult)controller.Add(model);
            var method = routeResult.RouteValues.First().Value.ToString();
            Assert.AreEqual("Index", method);
        }

        private static string GetErrorMessage(ViewResult viewResult)
        {
            var errorMessage = viewResult.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            return errorMessage;
        }

        private ITataboufRepository GetRepositoryMock()
        {
            var repositoryMock = new Mock<ITataboufRepository>();
            repositoryMock.Setup(r => r.AddCrew(It.IsAny<Crew>()));
            repositoryMock.Setup(r => r.UpdateCrew(It.IsAny<Crew>(), It.IsAny<string>()));
            repositoryMock.Setup(r => r.DeleteCrew(It.IsAny<int>(), It.IsAny<string>()));
            repositoryMock.Setup(r => r.GetAllDates()).Returns(GetFakeDates());
            repositoryMock.Setup(r => r.FindCrewById(It.IsAny<int>())).Returns(GetFakeCrew());
            return repositoryMock.Object;
        }

        private IEnumerable<Crew> GetFakeDates()
        {
            var dates = new List<Crew>();
            dates.Add(new Crew { Id = 1, Name = "Raoul", Carrefour = true, MarieBlachere = true });
            dates.Add(new Crew { Id = 2, Name = "Marcel", Quick = true, NumberOfSeatsAvailable = 3 });
            dates.Add(new Crew { Id = 3, Name = "René", Kebab = true, MarieBlachere = true });
            dates.Add(new Crew { Id = 4, Name = "Bob", Carrefour = true, Other = true });
            dates.Add(new Crew { Id = 5, Name = "Michel", IGotIt = true });

            return dates;
        }

        private Crew GetFakeCrew()
        {
            return new Crew
            {
                Id = 1,
                MarieBlachere = true,
                NumberOfSeatsAvailable = 3
            };
        }
    }
}
