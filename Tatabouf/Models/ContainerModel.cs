using System;
using System.Collections.Generic;
using System.Globalization;

namespace Tatabouf.Models
{
    public class ContainerModel
    {
        public string DateOfTheDay { get; set; }

        /// <summary>
        /// bound form (check boxes)
        /// </summary>
        public UserModel FoodChoice { get; set; }

        /// <summary>
        /// all registered users and their choices
        /// </summary>
        public IEnumerable<UserModel> UsersChoices { get; set; }

        /// <summary>
        /// all places registered in database
        /// </summary>
        public IEnumerable<PlaceModel> Places { get; set; }

        /// <summary>
        /// selected user choices to insert in dB
        /// </summary>
        public IEnumerable<ChoiceModel> Choices { get; set; }

        public string IpVisitor { get; set; }

        public bool ShowForm { get; set; }

        public ContainerModel()
        {
            DateOfTheDay = DateTime.Now.ToString("d MMM", CultureInfo.CreateSpecificCulture("fr-FR"));
        }
    }
}