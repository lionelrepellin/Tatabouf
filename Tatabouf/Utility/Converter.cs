using System;
using System.Linq;
using System.Collections.Generic;
using Tatabouf.Domain;
using Tatabouf.Models;

namespace Tatabouf.Utility
{
    public static class Converter
    {
        public static UserModel UserToFoodChoiceModel(User userChoice)
        {
            var choiceModels = userChoice.Choices.Select(c => new ChoiceModel
            {
                UserId = c.UserId,
                PlaceId = c.PlaceId,
                Other = c.OtherIdea                
            }).ToList();
            
            return new UserModel
            {
                Id = userChoice.Id,
                Name = userChoice.Name,
                NumberOfAvailableSeats = userChoice.AvailableSeats,
                IP = userChoice.IpAddress,
                DepartureTime = userChoice.DepartureTime,
                ChoiceModels = choiceModels
            };
        }
                
        public static User UserModelToUser(ContainerModel model)
        {
            // select only checked box: PlaceId > 0
            var choices = model.Choices.Where(c => c.PlaceId > 0).Select(c => new Choice
            {
                UserId = c.UserId,
                PlaceId = c.PlaceId,                
                OtherIdea = CheckOtherIdeaValue(c.Other)
            });

            return new User
            {
                Id = model.FoodChoice.Id,
                Name = model.FoodChoice.Name,
                AvailableSeats = model.FoodChoice.NumberOfAvailableSeats,
                IpAddress = model.FoodChoice.IP,
                DepartureTime = model.FoodChoice.DepartureTime,
                Choices = choices.ToList()
            };
        }

        private static string CheckOtherIdeaValue(string idea)
        {
            const int maxChars = 20;

            if (string.IsNullOrEmpty(idea) || string.IsNullOrWhiteSpace(idea))
            {
                return null;
            }
            else
            {
                idea = idea.Trim();
                if (idea.Length > maxChars)
                {
                    idea = idea.Substring(0, maxChars);
                }
                return idea;
            }
        }
        
        public static ICollection<Place> PlaceModelsToPlaces(IEnumerable<PlaceModel> choices)
        {
            var places = new List<Place>();
            foreach (var choice in choices)
            {
                places.Add(PlaceModelToPlace(choice));
            }
            return places;
        }

        public static Place PlaceModelToPlace(PlaceModel model)
        {
            return new Place
            {
                Id = model.Id,
                Label = model.Label,
                InputType = model.InputType,
                Css = model.Css
            };
        }

        public static IEnumerable<UserModel> UsersToUserModels(IEnumerable<User> userChoices)
        {
            foreach (var choice in userChoices)
            {
                yield return UserToFoodChoiceModel(choice);
            }
        }

        public static IEnumerable<PlaceModel> PlacesToPlaceModels(IEnumerable<Place> places)
        {
            foreach (var place in places)
            {
                yield return PlaceToPlaceModel(place);
            }
        }

        public static PlaceModel PlaceToPlaceModel(Place place)
        {
            return new PlaceModel
            {
                Id = place.Id,
                Label = place.Label,
                InputType = place.InputType,
                Css = place.Css
            };
        }
    }
}