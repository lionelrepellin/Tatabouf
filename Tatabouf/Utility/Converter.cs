using System.Collections.Generic;
using Tatabouf.Domain;
using Tatabouf.Models;

namespace Tatabouf.Utility
{
    //TODO replace converter by AutoMapper
    public static class Converter
    {
        public static UserModel UserToFoodChoiceModel(User userChoice)
        {
            return new UserModel
            {
                Id = userChoice.Id,
                Name = userChoice.Name,
                IBroughtMyLunch = userChoice.IHaveMyLunch,
                NumberOfAvailableSeats = userChoice.AvailableSeats,
                IP = userChoice.IpAddress,
                Choices = PlacesToPlaceModels(userChoice.SelectedPlaces)
            };
        }

        public static User UserModelToUser(UserModel model)
        {
            return new User
            {
                Id = model.Id,
                Name = model.Name,
                IHaveMyLunch = model.IBroughtMyLunch,
                AvailableSeats = model.NumberOfAvailableSeats,
                IpAddress = model.IP,
                SelectedPlaces = PlaceModelsToPlaces(model.Choices)
            };
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
                Label = model.Label
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
                Label = place.Label
            };
        }
    }
}