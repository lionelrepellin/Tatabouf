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
            var choicesCount = user.SelectedPlaces.Count();

            if (choicesCount == 0 && !user.IHaveMyLunch)
            {
                errorMessage = "Merci de cocher au moins une case !";
                return false;
            }
            else if (user.IHaveMyLunch && choicesCount > 0)
            {
                errorMessage = "Si tatabouf, pourquoi aller chercher bonheur ailleurs ?";
                return false;
            }
            else if (user.IHaveMyLunch && user.AvailableSeats.HasValue && user.AvailableSeats.Value > 0)
            {
                errorMessage = "Tatabouf ou tapatabouf ?";
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
    }
}