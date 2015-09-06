using System.Collections.Generic;

namespace Tatabouf.Models
{
    public class ContainerModel
    {
        /// <summary>
        /// mapped form (check boxes)
        /// </summary>
        public UserModel FoodChoice { get; set; }

        /// <summary>
        /// users and their choices
        /// </summary>
        public IEnumerable<UserModel> UsersChoices { get; set; }

        /// <summary>
        /// all places registered in database
        /// </summary>
        public IEnumerable<PlaceModel> AllPlaces { get; set; }
        
        public string IpVisitor { get; set; }
    }
}