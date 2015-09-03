using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tatabouf.Domain;

namespace Tatabouf.Tests
{
    [TestClass]
    public class CrewTest
    {
        [TestMethod]
        public void Crew_NoChoice()
        {
            var crew = new Crew();
            var errorMessage = crew.CheckAllBoxes();
            Assert.AreEqual("Merci de cocher au moins une case !", errorMessage);
        }

        [TestMethod]
        public void Crew_AllBoxChecked()
        {
            var crew = new Crew();
            crew.Kebab = true;
            crew.Quick = true;
            crew.MarieBlachere = true;
            crew.Carrefour = true;
            crew.Other = true;
            crew.IGotIt = true;
            var errorMessage = crew.CheckAllBoxes();
            Assert.AreEqual("Si tatabouf, pourquoi aller chercher bonheur ailleurs ?", errorMessage);
        }

        [TestMethod]
        public void Crew_OneBoxCheckedAndIGotIt()
        {
            var crew = new Crew();
            crew.Kebab = true;
            crew.IGotIt = true;
            var errorMessage = crew.CheckAllBoxes();
            Assert.AreEqual("Si tatabouf, pourquoi aller chercher bonheur ailleurs ?", errorMessage);
        }

        [TestMethod]
        public void Crew_IGotIt()
        {
            var crew = new Crew();
            crew.IGotIt = true;
            var errorMessage = crew.CheckAllBoxes();
            Assert.AreEqual(string.Empty, errorMessage);
        }

        [TestMethod]
        public void Crew_OneBoxChecked()
        {
            var crew = new Crew();
            crew.Other = true;
            var errorMessage = crew.CheckAllBoxes();
            Assert.AreEqual(string.Empty, errorMessage);
        }

        [TestMethod]
        public void Crew_OneOrMoreBoxChecked()
        {
            var crew = new Crew();
            crew.Other = true;
            crew.Carrefour = true;
            var errorMessage = crew.CheckAllBoxes();
            Assert.AreEqual(string.Empty, errorMessage);
        }

        [TestMethod]
        public void Crew_IGotItAndOneSeatAvailable()
        {
            var crew = new Crew();
            crew.IGotIt = true;
            crew.NumberOfSeatsAvailable = 1;
            var errorMessage = crew.CheckAllBoxes();
            Assert.AreEqual("Tatabouf ou tapatabouf ?", errorMessage);
        }

        [TestMethod]
        public void Crew_IGotItAndOneOrMoreBoxCheckedAndSeatsAvailable()
        {
            var crew = new Crew();
            crew.IGotIt = true;
            crew.MarieBlachere = true;
            crew.NumberOfSeatsAvailable = 1;
            var errorMessage = crew.CheckAllBoxes();
            Assert.AreEqual("Si tatabouf, pourquoi aller chercher bonheur ailleurs ?", errorMessage);
        }
    }
}
