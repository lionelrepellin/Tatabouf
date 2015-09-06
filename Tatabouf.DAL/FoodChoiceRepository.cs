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
        public MainContext Context { get; set; }

        public void Add(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public User FindById(int userId)
        {
            return Context.Users
                            .Include(m => m.SelectedPlaces)
                            .Where(m => m.Id == userId)
                            .SingleOrDefault();
        }

        public IEnumerable<User> GetUsersChoices(DateTime beginDate, DateTime endDate)
        {
            return Context.Users
                            .Include(m => m.SelectedPlaces)
                            .Where(m => m.InscriptionDate >= beginDate && m.InscriptionDate < endDate)
                            .OrderBy(m => m.Id).ToList();
        }

        public void Delete(User user)
        {
            Context.Users.Remove(user);
            Context.SaveChanges();
        }

        public IEnumerable<Place> GetPlaces()
        {
            return Context.Places.OrderBy(p => p.DisplayOrder).ToList();
        }

        public IEnumerable<Place> FindPlacesByIds(List<int> placesId)
        {
            return Context.Places.Where(p => placesId.Contains(p.Id)).ToList();
        }

        public void Update(User originalUser, User newUser)
        {
            // name and ip address are not updated
            // inscription date is not updated
            originalUser.IGotMyLunch = newUser.IGotMyLunch;
            originalUser.AvailableSeats = newUser.AvailableSeats;
            originalUser.SelectedPlaces = newUser.SelectedPlaces;
            Context.SaveChanges();
        }
    }
}