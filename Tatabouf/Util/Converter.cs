using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tatabouf.Domain;
using Tatabouf.Models;

namespace Tatabouf.Util
{
    //TODO replace converter by AutoMapper
    public class Converter
    {
        public static CrewModel GetCrewModel(Crew crew)
        {
            return new CrewModel
            {
                Id = crew.Id,
                Name = crew.Name,
                Carrefour = crew.Carrefour,
                Kebab = crew.Kebab,
                MarieBlachere = crew.MarieBlachere,
                Quick = crew.Quick,
                Other = crew.Other,
                IGotIt = crew.IGotIt,
                NumberOfSeatsAvailable = crew.NumberOfSeatsAvailable,
                IpAddress = crew.IpAddress
            };
        }

        public static Crew GetCrew(CrewModel model)
        {
            return new Crew
            {
                Id = model.Id,
                Name = model.Name,
                Carrefour = model.Carrefour,
                Kebab = model.Kebab,
                MarieBlachere = model.MarieBlachere,
                Quick = model.Quick,
                Other = model.Other,
                IGotIt = model.IGotIt,
                NumberOfSeatsAvailable = model.NumberOfSeatsAvailable,
                IpAddress = model.IpAddress
            };
        }

        public static IEnumerable<CrewModel> GetCrewModels(IEnumerable<Crew> crews)
        {
            foreach (var crew in crews)
            {
                yield return GetCrewModel(crew);
            }
        }
    }
}