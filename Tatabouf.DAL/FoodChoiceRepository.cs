using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tatabouf.Domain;

namespace Tatabouf.DAL
{
    public interface IFoodChoiceRepository
    {
        void Add(User user);
        void Update(User originalUser, User newUser);
        void Delete(User user);
        IEnumerable<Place> GetPlaces();
        IEnumerable<User> GetUsersChoices(DateTime beginDate, DateTime endDate);
        User FindById(int userId);
        IEnumerable<Place> FindPlacesByIds(List<int> placesId);
    }

    public class FoodChoiceRepository : IFoodChoiceRepository
    {
        [Dependency]
        public TataboufContext TataboufContext { get; set; }

        public void Add(User user)
        {
            TataboufContext.Users.Add(user);
            TataboufContext.SaveChanges();
        }

        public User FindById(int userId)
        {
            return TataboufContext.Users
                            .Include(m => m.SelectedPlaces)
                            .Where(m => m.Id == userId)
                            .SingleOrDefault();
        }

        public IEnumerable<User> GetUsersChoices(DateTime beginDate, DateTime endDate)
        {
            return TataboufContext.Users
                            .Include(m => m.SelectedPlaces)
                            .Where(m => m.InscriptionDate >= beginDate && m.InscriptionDate < endDate)
                            .OrderBy(m => m.Id).ToList();
        }

        public void Delete(User user)
        {
            TataboufContext.Users.Remove(user);
            TataboufContext.SaveChanges();
        }

        public IEnumerable<Place> GetPlaces()
        {
            return TataboufContext.Places.OrderBy(p => p.DisplayOrder).ToList();
        }

        public IEnumerable<Place> FindPlacesByIds(List<int> placesId)
        {
            return TataboufContext.Places.Where(p => placesId.Contains(p.Id)).ToList();
        }

        public void Update(User originalUser, User newUser)
        {
            // name and ip address are not updated
            // inscription date is not updated
            originalUser.IHaveMyLunch = newUser.IHaveMyLunch;
            originalUser.AvailableSeats = newUser.AvailableSeats;
            originalUser.SelectedPlaces = newUser.SelectedPlaces;
            TataboufContext.SaveChanges();
        }
    }
}