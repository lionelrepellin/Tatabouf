using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tatabouf.Domain;

namespace Tatabouf.Business
{
    public class ValidationService
    {
        public bool DoAllControls(User user, IEnumerable<User> userChoices, out string errorMessage)
        {
            var error = string.Empty;
            if (!ControlCheckBoxes(user, out error))
            {
                errorMessage = error;
                return false;
            }
            if (!ControlSameNameExists(user.Name, userChoices, out error))
            {
                errorMessage = error;
                return false;
            }
            errorMessage = error;
            return true;
        }
        
        public bool ControlCheckBoxes(User user, out string errorMessage)
        {
            var choicesCount = user.Choices.Count();
            if (choicesCount == 0)
            {
                errorMessage = "Merci de cocher au moins une case !";
                return false;
            }            
            errorMessage = string.Empty;
            return true;
        }
        
        // Check if the name already exists in registered choices for the current day
        public bool ControlSameNameExists(string name, IEnumerable<User> userChoices, out string errorMessage)
        {
            if (!userChoices.Any())
            {
                errorMessage = string.Empty;
                return true;
            }
            if (IsNameAlreadyExists(name, userChoices))
            {
                errorMessage = "Le nom existe déjà !";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        private static bool IsNameAlreadyExists(string name, IEnumerable<User> userChoices)
        {
            if (!string.IsNullOrEmpty(name) && userChoices != null && userChoices.Any())
            {
                name = name.ToLower().Trim();
                var names = userChoices.Select(d => d.Name.ToLower().Trim()).ToList();
                return names.Contains(name);
            }
            return false;
        }

        // Try to prevent hacks from GDE ;)
        public void RemoveInvalidEntry(User user, IEnumerable<Place> places)
        {
            var validatedChoices = new List<Choice>();

            foreach (var userChoice in user.Choices)
            {
                // check if place id use the right input type
                var validPlace = places.Where(p => p.Id == userChoice.PlaceId
                                                && ((p.InputType && !string.IsNullOrEmpty(userChoice.OtherIdea)) // input text
                                                || (!p.InputType && string.IsNullOrEmpty(userChoice.OtherIdea)))) // checkbox
                                                .SingleOrDefault();

                if (validPlace != null)
                {
                    validatedChoices.Add(userChoice);
                }
            }
            user.Choices = validatedChoices;
        }
    }
}