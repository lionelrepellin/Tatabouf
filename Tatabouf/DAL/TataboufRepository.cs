using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tatabouf.Domain;

namespace Tatabouf.DAL
{
    public class TataboufRepository
    {
        public void AddMember(Crew crew)
        {
            using (var ctx = new TataboufContext())
            {
                ctx.Dates.Add(crew);
                ctx.SaveChanges();
            }
        }

        public void UpdateDate(Crew crew)
        {
            using (var ctx = new TataboufContext())
            {
                var date = ctx.Dates.Where(d => d.Id == crew.Id).SingleOrDefault();

                if (date != null)
                {
                    date = crew;
                    ctx.SaveChanges();
                }
            }
        }

        public void RemoveDate(int crewId)
        {
            using (var ctx = new TataboufContext())
            {
                var date = ctx.Dates.Where(d => d.Id == crewId).SingleOrDefault();

                if (date != null)
                {
                    ctx.Dates.Remove(date);
                    ctx.SaveChanges();
                }
            }
        }

        public IEnumerable<Crew> FindAllDates()
        {
            using (var ctx = new TataboufContext())
            {
                var now = DateTime.Now.ToString("yyyy-MM-dd");
                var all = ctx.Dates.OrderByDescending(d => d.Id).Take(30).ToList();

                return all.Where(d => d.InscriptionDate.ToString("yyyy-MM-dd") == now).OrderBy(d => d.InscriptionDate);
            }
        }

        public Crew FindCrewById(int crewId)
        {
            using (var ctx = new TataboufContext())
            {
                return ctx.Dates.Where(d => d.Id == crewId).SingleOrDefault();
            }
        }
    }
}