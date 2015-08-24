using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tatabouf.DAL;
using Tatabouf.Domain;
using Tatabouf.Models;
using Tatabouf.Util;

namespace Tatabouf.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public TataboufRepository Repository { get; set; }
        

        public ActionResult Index()
        {
            var dates = Repository.FindAllDates();
            var model = new ContainerModel()
            {
                Crew = new CrewModel(),
                Dates = Converter.GetCrewModels(dates)
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
                var dates = Repository.FindAllDates();

                if (IsNameExists(crew.Name, dates))
                {
                    ModelState.AddModelError("Name", "Le nom existe déjà !");
                    model.Dates = Converter.GetCrewModels(dates);
                    return View("Index", model);
                }
                else
                {
                    Repository.AddCrew(crew);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var dates = Repository.FindAllDates();
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
                    Crew = Converter.GetCrewModel(crew)
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
                var crew = Converter.GetCrew(model.Crew);
                Repository.UpdateCrew(crew);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Form", "Popup", model);
            }
        }


        private bool IsNameExists(string name, IEnumerable<Crew> dates)
        {
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
