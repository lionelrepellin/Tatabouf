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
        User FindByIpAddress(string ip);
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
                                    .Include(m => m.Choices.Select(c => c.Place))
                                    .Where(m => m.Id == userId)
                                    .SingleOrDefault();
        }

        public IEnumerable<User> GetUsersChoices(DateTime beginDate, DateTime endDate)
        {
            return TataboufContext.Users
                                    .Include(m => m.Choices.Select(c => c.Place))
                                    .Where(m => m.InscriptionDate >= beginDate && m.InscriptionDate < endDate)
                                    .OrderBy(m => m.Id)
                                    .ToList();
        }

        public void Delete(User user)
        {
            TataboufContext.Users.Remove(user);
            TataboufContext.SaveChanges();
        }
        
        public IEnumerable<Place> GetPlaces()
        {
            return TataboufContext.Places
                                    .OrderBy(p => p.DisplayOrder)
                                    .ToList();
        }

        public void Update(User originalUser, User newUser)
        {
            // all fields are not updated
            originalUser.AvailableSeats = newUser.AvailableSeats;            
            originalUser.DepartureTime = newUser.DepartureTime;
            originalUser.Choices = newUser.Choices;
            TataboufContext.SaveChanges();
        }
        
        public User FindByIpAddress(string ip)
        {
            return TataboufContext.Users
                                    .OrderByDescending(u => u.Id)                    
                                    .Where(u => u.IpAddress == ip)
                                    .FirstOrDefault();
        }        
    }
}