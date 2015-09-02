using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tatabouf.Domain;
using Tatabouf.Models;
using Tatabouf.Util;

namespace Tatabouf.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            var dates = Repository.GetAllDates();
            var model = new ContainerModel()
            {
                Crew = new CrewModel(),
                Dates = Converter.GetCrewModels(dates),
                IpVisitor = GetIP(Request)
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ContainerModel model)
        {
            if (ModelState.IsValid)
            {
                var crew = Converter.GetCrew(model.Crew);
                var dates = Repository.GetAllDates();

                if (IsNameExists(crew.Name, dates))
                {
                    ModelState.AddModelError("Name", "Le nom existe déjà !");
                    model.Dates = Converter.GetCrewModels(dates);
                    return View("Index", model);
                }
                else
                {
                    crew.IpAddress = GetIP(Request);
                    Repository.AddCrew(crew);

                    logger.Debug(string.Format("Ajout de l'utilisateur: {0} par l'IP: {1}", crew.Name, crew.IpAddress));
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var dates = Repository.GetAllDates();
                model.Dates = Converter.GetCrewModels(dates);
                return View("Index", model);
            }
        }


        public ActionResult Edit(int id)
        {
            var crew = Repository.FindCrewById(id);
            if (crew != null)
            {
                var container = new ContainerModel
                {
                    Crew = Converter.GetCrewModel(crew),
                    IpVisitor = GetIP(Request)
                };
                return View("Form", "Popup", container);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ContainerModel model)
        {
            if (ModelState.IsValid)
            {
                var ip = GetIP(Request);
                var crew = Converter.GetCrew(model.Crew);
                Repository.UpdateCrew(crew, ip);
                
                logger.Debug(string.Format("Modification de l'utilisateur: {0} par l'IP: {1}", crew.Name, ip));
                return RedirectToAction("Index");
            }
            else
            {
                // errors are not managed
                return RedirectToAction("Index");
            }
        }

        [HttpPost]        
        public void Remove(int id)
        {
            if (id > 0)
            {
                var ip = GetIP(Request);
                logger.Debug(string.Format("Suppression de l'utilisateur id: {0} par l'IP: {1}", id, ip));
                Repository.DeleteCrew(id, ip);
            }
        }


        // Check if name already exists in registered dates for the current day
        private bool IsNameExists(string name, IEnumerable<Crew> dates)
        {
            if (!dates.Any()) { return false; }

            if (!string.IsNullOrEmpty(name) && dates != null && dates.Any())
            {
                name = name.ToLower().Trim();
                var names = dates.Select(d => d.Name.ToLower().Trim()).ToList();
                return names.Contains(name);
            }
            return true;
        }        
    }
}
