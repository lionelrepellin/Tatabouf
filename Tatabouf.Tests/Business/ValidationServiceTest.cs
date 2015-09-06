using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tatabouf.Business;
using Tatabouf.Domain;

namespace Tatabouf.Tests.Business
{
    [TestClass]
    public class ValidationServiceTest
    {
        [TestMethod]
        public void ControlSameNameExists_SameNameFound()
        {
            var errorMessage = string.Empty;
            var userName = "Lionel";
            IEnumerable<User> users = new List<User>
            {
                new User{ Name = "Gérard" },
                new User{ Name = "Michel" },
                new User{ Name = "Lionel" },
                new User{ Name = "Bernard" }
            };

            var service = new ValidationService();
            var result = service.ControlSameNameExists(userName, users, out errorMessage);

            Assert.IsFalse(result);
            Assert.AreEqual("Le nom existe déjà !", errorMessage);
        }

        [TestMethod]
        public void ControlSameNameExists_EmptyUsersList()
        {
            var errorMessage = string.Empty;
            var userName = "lionel";
            IEnumerable<User> users = new List<User>();

            var service = new ValidationService();
            var result = service.ControlSameNameExists(userName, users, out errorMessage);

            Assert.IsTrue(result);
            Assert.IsTrue(string.IsNullOrEmpty(errorMessage));
        }

        [TestMethod]
        public void ControlSameNameExists_ItWorksFine()
        {
            var errorMessage = string.Empty;
            var userName = "Lionel";
            IEnumerable<User> users = new List<User>
            {
                new User{ Name = "Gérard" },
                new User{ Name = "Michel" },
                new User{ Name = "Gilbert" },
                new User{ Name = "Bernard" }
            };

            var service = new ValidationService();
            var result = service.ControlSameNameExists(userName, users, out errorMessage);

            Assert.IsTrue(result);
            Assert.IsTrue(string.IsNullOrEmpty(errorMessage));
        }

        [TestMethod]
        public void ControlCheckBoxes_NoChoiceMade()
        {
            var errorMessage = string.Empty;
            var user = new User();

            var service = new ValidationService();
            var result = service.ControlCheckBoxes(user, out errorMessage);

            Assert.IsFalse(result);
            Assert.AreEqual("Merci de cocher au moins une case !", errorMessage);
        }

        [TestMethod]
        public void ControlCheckBoxes_IGotMyLunchAndOtherChoice()
        {
            var errorMessage = string.Empty;
            var user = new User
            {
                IGotMyLunch = true,
                SelectedPlaces = new List<Place> { new Place { Label = "Carrefour" } }
            };

            var service = new ValidationService();
            var result = service.ControlCheckBoxes(user, out errorMessage);

            Assert.IsFalse(result);
            Assert.AreEqual("Si tatabouf, pourquoi aller chercher bonheur ailleurs ?", errorMessage);
        }

        [TestMethod]
        public void ControlCheckBoxes_IGotMyLunchAndAvailableSeats()
        {
            var errorMessage = string.Empty;
            var user = new User
            {
                IGotMyLunch = true,
                AvailableSeats = 2
            };

            var service = new ValidationService();
            var result = service.ControlCheckBoxes(user, out errorMessage);

            Assert.IsFalse(result);
            Assert.AreEqual("Tatabouf ou tapatabouf ?", errorMessage);
        }

        [TestMethod]
        public void ControlCheckBoxes_AllIsChecked()
        {
            var errorMessage = string.Empty;
            var user = new User
            {
                IGotMyLunch = true,
                AvailableSeats = 2,
                SelectedPlaces = new List<Place> { 
                    new Place { Label = "Carrefour" },
                    new Place { Label = "Quick" }, 
                    new Place { Label = "Autre" } 
                }
            };

            var service = new ValidationService();
            var result = service.ControlCheckBoxes(user, out errorMessage);

            Assert.IsFalse(result);
            Assert.AreEqual("Si tatabouf, pourquoi aller chercher bonheur ailleurs ?", errorMessage);
        }

        [TestMethod]
        public void ControlCheckBoxes_IGotMyLunch()
        {
            var errorMessage = string.Empty;
            var user = new User
            {
                IGotMyLunch = true
            };

            var service = new ValidationService();
            var result = service.ControlCheckBoxes(user, out errorMessage);

            Assert.IsTrue(result);
            Assert.IsTrue(string.IsNullOrEmpty(errorMessage));
        }

        [TestMethod]
        public void ControlCheckBoxes_IMadeChoices()
        {
            var errorMessage = string.Empty;
            var user = new User
            {
                AvailableSeats = 2,
                SelectedPlaces = new List<Place> { 
                    new Place { Label = "Carrefour" },
                    new Place { Label = "Quick" }, 
                    new Place { Label = "Autre" } 
                }
            };

            var service = new ValidationService();
            var result = service.ControlCheckBoxes(user, out errorMessage);

            Assert.IsTrue(result);
            Assert.IsTrue(string.IsNullOrEmpty(errorMessage));
        }
    }
}