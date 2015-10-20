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
using Tatabouf.Utility;
using Tatabouf.Business;

namespace Tatabouf.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest : BaseTest
    {
        private HomeController controller;

        [TestInitialize]
        public override void Initialize()
        {
            // initialize controller with services and fake repository
            controller = new HomeController();
            controller.ValidationService = new ValidationService();

            var foodChoiceService = new FoodChoiceService();
            foodChoiceService.FoodChoiceRepository = GetRepositoryMock().Object;
            controller.FoodChoiceService = foodChoiceService;
        }

        [TestMethod]
        public void Controller_AddWithSameName()
        {
            var model = new ContainerModel
            {
                FoodChoice = new UserModel
                {
                    Name = "Michel"
                }
            };

            var viewResult = (ViewResult)controller.Add(model);
            Assert.AreEqual("Le nom existe déjà !", GetErrorMessage(viewResult));
        }

        [TestMethod]
        public void Controller_TODO()
        {
            var model = new ContainerModel
            {
                FoodChoice = new UserModel
                {
                    Name = "Bob"
                }
            };

            var viewResult = (ViewResult)controller.Add(model);
            Assert.AreEqual("Merci de cocher au moins une case !", GetErrorMessage(viewResult));
        }

        //[TestMethod]
        //public void Controller_AddWithNameButToMuchChoices()
        //{
        //    var selectedPlacesId = new string[] { "1", "2" };

        //    var model = new ContainerModel
        //    {
        //        FoodChoice = new UserModel
        //        {
        //            Name = "Gérard",
        //            IBroughtMyLunch = true
        //        }
        //    };

        //    var viewResult = (ViewResult)controller.Add(model, selectedPlacesId);
        //    Assert.AreEqual("Si tatabouf, pourquoi aller chercher bonheur ailleurs ?", GetErrorMessage(viewResult));
        //}

        //[TestMethod]
        //public void Controller_AddWithNameButToMuchChoicesAndSeats()
        //{
        //    var model = new ContainerModel
        //    {
        //        FoodChoice = new UserModel
        //        {
        //            Name = "Gérard",
        //            IBroughtMyLunch = true,
        //            NumberOfAvailableSeats = 4
        //        }
        //    };

        //    var viewResult = (ViewResult)controller.Add(model, null);
        //    Assert.AreEqual("Tatabouf ou tapatabouf ?", GetErrorMessage(viewResult));
        //}

        //[TestMethod]
        //public void Controller_AddIMyLunch()
        //{
        //    var model = new ContainerModel
        //    {
        //        FoodChoice = new UserModel
        //        {
        //            Name = "Raoul",
        //            IBroughtMyLunch = true
        //        }
        //    };

        //    var routeResult = (RedirectToRouteResult)controller.Add(model, null);
        //    var method = routeResult.RouteValues.First().Value.ToString();
        //    Assert.AreEqual("Index", method);
        //}

        [TestMethod]
        public void Controller_AddWithNameButNoChoice()
        {
            var model = new ContainerModel
            {
                FoodChoice = new UserModel
                {
                    Name = "Simon",                    
                },
                Choices = new List<ChoiceModel>()
            };

            var viewResult = (ViewResult)controller.Add(model);
            Assert.AreEqual("Merci de cocher au moins une case !", GetErrorMessage(viewResult));            
        }

        [TestMethod]
        public void Controller_AddOneOrMoreChoicesAndSeats()
        {
            var model = new ContainerModel
            {
                FoodChoice = new UserModel
                {
                    Name = "Charles",
                    NumberOfAvailableSeats = 3
                },
                Choices = new List<ChoiceModel>
                {
                    new ChoiceModel { PlaceId = 1 },
                    new ChoiceModel { PlaceId = 5 }
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
    }

}
