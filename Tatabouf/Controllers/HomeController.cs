using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tatabouf.DAL;
using Tatabouf.Domain;

namespace Tatabouf.Controllers
{
    public class HomeController : Controller
    {
        private readonly TataboufRepository _tataboufRepository;

        public HomeController()
        {
            _tataboufRepository = new TataboufRepository();
        }

        public ActionResult Index()
        {
            //var c = new Crew();
            //c.Name = "toto";
            //c.MarieBlachere = true;
            //c.NumberOfSeatsAvailable = 3;
            //_tataboufRepository.AddDate(c);

            //var c = new Crew();
            //c.Id = 2;
            //c.Name = "Tata";

            //_tataboufRepository.UpdateDate(c);

            var dates = _tataboufRepository.FindAllDates();
            
            return View(dates);
        }

        [HttpPost]
        public ActionResult Add(Crew crew)
        {
            if (ModelState.IsValid)
            {
                _tataboufRepository.AddMember(crew);         
            }

            return RedirectToAction("Index");
        }


    }
}
