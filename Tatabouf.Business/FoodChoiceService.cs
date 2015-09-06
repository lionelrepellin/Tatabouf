using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tatabouf.DAL;
using Tatabouf.Domain;

namespace Tatabouf.Business
{
    public class FoodChoiceService
    {
        [Dependency]
        public IFoodChoiceRepository FoodChoiceRepository { get; set; }

        public void AddUser(User user)
        {
            FoodChoiceRepository.Add(user);
        }

        public IEnumerable<User> GetTodayUsersChoices()
        {
            var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var tomorrow = today.AddDays(1);

            return FoodChoiceRepository.GetUsersChoices(today, tomorrow);
        }

        public IEnumerable<Place> GetPlaces()
        {
            return FoodChoiceRepository.GetPlaces();
        }

        public IEnumerable<Place> FindPlacesByIds(string[] selectedIds)
        {
            if (selectedIds == null) return new List<Place>();

            var idList = new List<int>();
            foreach (var id in selectedIds)
            {
                int result;
                if (Int32.TryParse(id, out result))
                {
                    idList.Add(result);
                }
            }
            return FoodChoiceRepository.FindPlacesByIds(idList);
        }

        public User FindUserById(int userId)
        {
            return FoodChoiceRepository.FindById(userId);
        }

        public void DeleteUser(int userId, string ipAddressToCompare)
        {
            var userToDelete = FindUserById(userId);
            if (userToDelete != null && userToDelete.IpAddress == ipAddressToCompare)
            {
                FoodChoiceRepository.Delete(userToDelete);
            }
        }

        public void UpdateUser(User newUser, string ipAddressToCompare)
        {
            var originalUser = FindUserById(newUser.Id);
            if (originalUser != null && originalUser.IpAddress == ipAddressToCompare)
            {
                FoodChoiceRepository.Update(originalUser, newUser);
            }
        }
    }
}
