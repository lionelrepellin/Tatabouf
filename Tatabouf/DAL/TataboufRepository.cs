using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tatabouf.Domain;

namespace Tatabouf.DAL
{
    public interface ITataboufRepository
    {
        void AddCrew(Crew crew);
        void UpdateCrew(Crew crew, string ipToCompare);
        IEnumerable<Crew> GetAllDates();
        Crew FindCrewById(int crewId);
        void DeleteCrew(int crewId, string ipToCompare);
    }

    public class TataboufRepository : ITataboufRepository
    {
        [Dependency]
        public TataboufContext Context { get; set; }

        public void AddCrew(Crew crew)
        {
            Context.Dates.Add(crew);
            Context.SaveChanges();
        }

        public void UpdateCrew(Crew crew, string ipToCompare)
        {
            var crewToUpdate = FindCrewById(crew.Id);
            if (crewToUpdate != null && crewToUpdate.IpAddress == ipToCompare)
            {
                // name is not updated
                crewToUpdate.Carrefour = crew.Carrefour;
                crewToUpdate.Kebab = crew.Kebab;
                crewToUpdate.MarieBlachere = crew.MarieBlachere;
                crewToUpdate.NumberOfSeatsAvailable = crew.NumberOfSeatsAvailable;
                crewToUpdate.Other = crew.Other;
                crewToUpdate.Quick = crew.Quick;

                Context.SaveChanges();
            }
        }

        public IEnumerable<Crew> GetAllDates()
        {
            var now = DateTime.Now.ToString("yyyy-MM-dd");
            var all = Context.Dates.OrderByDescending(d => d.Id).Take(30).ToList();

            return all.Where(d => d.InscriptionDate.ToString("yyyy-MM-dd") == now).OrderBy(d => d.InscriptionDate);
        }

        public Crew FindCrewById(int crewId)
        {
            return Context.Dates.Where(d => d.Id == crewId).SingleOrDefault();
        }

        public void DeleteCrew(int crewId, string ipToCompare)
        {
            var crewToRemove = FindCrewById(crewId);
            if (crewToRemove != null && crewToRemove.IpAddress == ipToCompare)
            {
                Context.Dates.Remove(crewToRemove);
                Context.SaveChanges();
            }
        }
    }
}