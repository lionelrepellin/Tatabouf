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

        private static bool IsTheRightUser(User user, string ipAddressToCompare)
        {
            return (user != null && user.IpAddress == ipAddressToCompare);
        }

        public void AddUser(User user)
        {
#if DEBUG
            FoodChoiceRepository.Add(user);
#else
            if (!UserAlreadyRegisteredToday(user.IpAddress))
            {
                FoodChoiceRepository.Add(user);
            }
#endif
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
        
        public User FindUserById(int userId)
        {
            return FoodChoiceRepository.FindById(userId);
        }

        public void DeleteUser(int userId, string ipAddressToCompare)
        {
            var userToDelete = FindUserById(userId);
            if (IsTheRightUser(userToDelete, ipAddressToCompare))
            {
                FoodChoiceRepository.Delete(userToDelete);
            }
        }

        public void UpdateUser(User newUser, string ipAddressToCompare)
        {
            var originalUser = FindUserById(newUser.Id);
            if (IsTheRightUser(originalUser, ipAddressToCompare))
            {
                FoodChoiceRepository.Update(originalUser, newUser);
            }
        }
        
        public User FindUserByIpAddress(string ipAddress)
        {
            if (!string.IsNullOrEmpty(ipAddress))
            {
                return FoodChoiceRepository.FindByIpAddress(ipAddress);
            }
            return null;
        }

        public bool UserAlreadyRegisteredToday(string ipAddress)
        {
            var registeredUsers = GetTodayUsersChoices();
            if (registeredUsers.Any())
            {
                var user = registeredUsers.Where(u => u.IpAddress == ipAddress).FirstOrDefault();
                return user != null;
            }
            return false;
        }
    }
}