using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tatabouf.Domain;
using Tatabouf.Models;
using Tatabouf.Utility;

namespace Tatabouf.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var usersChoices = MainService.GetTodayUsersChoices();
            var places = MainService.GetPlaces();
            var model = new ContainerModel()
            {
                FoodChoice = new UserModel(),
                UsersChoices = Converter.UsersToUserModels(usersChoices),
                AllPlaces = Converter.PlacesToPlaceModels(places),
                IpVisitor = GetIP(Request)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ContainerModel model, string[] selectedPlacesId)
        {
            var usersChoices = MainService.GetTodayUsersChoices();

            if (ModelState.IsValid)
            {
                var user = Converter.UserModelToUser(model.FoodChoice);
                user.SelectedPlaces = MainService.FindPlacesByIds(selectedPlacesId).ToList();

                var errorMessage = string.Empty;
                var allControlsAreOk = ValidationService.DoAllControls(user, usersChoices, out errorMessage);

                if (allControlsAreOk)
                {
                    user.IpAddress = GetIP(Request);
                    MainService.AddUser(user);
                    
                    logger.Debug("Ajout de l'utilisateur: {0} - IP: {1}", user.Name, user.IpAddress);                   
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", errorMessage);
                    model.UsersChoices = Converter.UsersToUserModels(usersChoices);
                    model.IpVisitor = GetIP(Request);
                    model.AllPlaces = Converter.PlacesToPlaceModels(MainService.GetPlaces());
                    
                    logger.Error("Erreur lors de l'ajout: {0} - IP: {1} - erreur: {2}", model.FoodChoice.Name, model.IpVisitor, errorMessage);
                    return View("Index", model);
                }
            }
            else
            {
                var allPlaces = MainService.GetPlaces();

                model.UsersChoices = Converter.UsersToUserModels(usersChoices);
                model.AllPlaces = Converter.PlacesToPlaceModels(allPlaces);

                logger.Error("Erreur lors de l'ajout: {0} - IP: {1}", model.FoodChoice.Name, model.IpVisitor);
                return View("Index", model);
            }
        }
        
        public ActionResult Edit(int id)
        {
            var user = MainService.FindUserById(id);
            if (user != null)
            {
                var allPlaces = MainService.GetPlaces();
                var container = new ContainerModel
                {
                    FoodChoice = Converter.UserToFoodChoiceModel(user),
                    IpVisitor = GetIP(Request),
                    AllPlaces = Converter.PlacesToPlaceModels(allPlaces)
                };
                return View("_Form", "Popup", container);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ContainerModel model, string[] selectedPlacesId)
        {
            if (ModelState.IsValid)
            {
                var ip = GetIP(Request);
                var user = Converter.UserModelToUser(model.FoodChoice);                
                user.SelectedPlaces = MainService.FindPlacesByIds(selectedPlacesId).ToList();

                var errorMessage = string.Empty;
                if (ValidationService.ControlCheckBoxes(user, out errorMessage))
                {
                    MainService.UpdateUser(user, ip);
                    logger.Debug("Modification de l'utilisateur: {0} - IP: {1}", user.Name, ip);
                }
                return RedirectToAction("Index");
            }
            else
            {
                // errors are not managed
                logger.Error("Erreur lors de la modification: {0}", model.FoodChoice.Name);
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
                MainService.DeleteUser(id, ip);
            }
        }
    }
}
