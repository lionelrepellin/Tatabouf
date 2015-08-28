using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tatabouf.Domain;

namespace Tatabouf.DAL
{
    public class TataboufRepository
    {
        [Dependency]
        public TataboufContext Context { get; set; }

        public void AddCrew(Crew crew)
        {
            Context.Dates.Add(crew);
            Context.SaveChanges();
        }

        public void UpdateCrew(Crew crew)
        {
            var date = Context.Dates.Where(d => d.Id == crew.Id).SingleOrDefault();
            if (date != null)
            {
                // name is not updated
                date.Carrefour = crew.Carrefour;
                date.Kebab = crew.Kebab;
                date.MarieBlachere = crew.MarieBlachere;
                date.NumberOfSeatsAvailable = crew.NumberOfSeatsAvailable;
                date.Other = crew.Other;
                date.Quick = crew.Quick;

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
    }
}