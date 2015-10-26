using System;
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
            var places = FoodChoiceService.GetPlaces();
            var usersChoices = FoodChoiceService.GetTodayUsersChoices();            
            var ipAddress = GetIP(Request);            
#if DEBUG
            var userAlreadyRegistered = false;
            User user = null;
#else
            var userAlreadyRegistered = FoodChoiceService.UserAlreadyRegisteredToday(ipAddress);
            var user = FoodChoiceService.FindUserByIpAddress(ipAddress);
#endif
            var model = new ContainerModel()
            {
                // empty form
                FoodChoice = new UserModel{
                    Name = user == null ? null : user.Name                    
                },

                // users choices list
                UsersChoices = Converter.UsersToUserModels(usersChoices),

                // places list
                Places = Converter.PlacesToPlaceModels(places),

                IpVisitor = ipAddress,
                ShowForm = !userAlreadyRegistered
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ContainerModel model)
        {
            var usersChoices = FoodChoiceService.GetTodayUsersChoices();
            var ipAddress = GetIP(Request);

            if (ModelState.IsValid)
            {
                var user = Converter.UserModelToUser(model);

                // check choices consistency
                var places = FoodChoiceService.GetPlaces();
                ValidationService.RemoveInvalidEntry(user, places);

                var errorMessage = string.Empty;
                var allControlsAreOk = ValidationService.DoAllControls(user, usersChoices, out errorMessage);

                if (allControlsAreOk)
                {
                    user.IpAddress = ipAddress;
                    FoodChoiceService.AddUser(user);

                    logger.Debug("Ajout de l'utilisateur: {0} - IP: {1}", user.Name, user.IpAddress);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", errorMessage);
                    model.UsersChoices = Converter.UsersToUserModels(usersChoices);
                    model.IpVisitor = ipAddress;
                    model.Places = Converter.PlacesToPlaceModels(FoodChoiceService.GetPlaces());
                    model.ShowForm = true;

                    logger.Error("Erreur lors de l'ajout: {0} - IP: {1} - erreur: {2}", model.FoodChoice.Name, model.IpVisitor, errorMessage);
                    return View("Index", model);
                }
            }
            else
            {
                var allPlaces = FoodChoiceService.GetPlaces();
                model.UsersChoices = Converter.UsersToUserModels(usersChoices);
                model.Places = Converter.PlacesToPlaceModels(allPlaces);
                model.ShowForm = true;

                logger.Error("Erreur lors de l'ajout: {0} - IP: {1}", model.FoodChoice.Name, ipAddress);
                return View("Index", model);
            }
        }

        public ActionResult Edit(int id)
        {
            var user = FoodChoiceService.FindUserById(id);
            if (user != null)
            {
                var allPlaces = FoodChoiceService.GetPlaces();
                var container = new ContainerModel
                {
                    FoodChoice = Converter.UserToFoodChoiceModel(user),
                    IpVisitor = GetIP(Request),
                    Places = Converter.PlacesToPlaceModels(allPlaces),
                    ShowForm = true
                };
                return View("_Form", "Popup", container);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ContainerModel model)
        {
            var ipAddress = GetIP(Request);

            if (ModelState.IsValid)
            {                
                var user = Converter.UserModelToUser(model);

                // check choices consistency
                var places = FoodChoiceService.GetPlaces();
                ValidationService.RemoveInvalidEntry(user, places);

                var errorMessage = string.Empty;
                if (ValidationService.ControlCheckBoxes(user, out errorMessage))
                {
                    FoodChoiceService.UpdateUser(user, ipAddress);
                    logger.Debug("Modification de l'utilisateur: {0} - IP: {1}", user.Name, ipAddress);
                }
                return RedirectToAction("Index");
            }
            else
            {
                // errors are not managed
                logger.Error("Erreur lors de la modification: {0} - IP: {1}", model.FoodChoice.Name, ipAddress);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public void Remove(int id)
        {
            if (id > 0)
            {
                var ipAddress = GetIP(Request);
                logger.Debug(string.Format("Suppression de l'utilisateur id: {0} par l'IP: {1}", id, ipAddress));
                FoodChoiceService.DeleteUser(id, ipAddress);
            }
        }
    }
}