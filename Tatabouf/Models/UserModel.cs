using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tatabouf.Attributes;

namespace Tatabouf.Models
{
    /// <summary>
    /// Actions name for Add and Update
    /// </summary>
    public enum Action
    {
        Add,
        Update
    };

    public class UserModel
    {
        private const string _defaultDepartureTime = "13:00";

        public int Id { get; set; }

        public Action ActionName
        {
            get
            {
                return (Id == 0) ? Action.Add : Action.Update;
            }
        }

        [Required(ErrorMessage = "C'est toi John Doe ?")]
        [StringLength(15, ErrorMessage = "Nom d'utilisateur: 15 caractères maximum")]
        public string Name { get; set; }

        public DateTime? DepartureTime { get; set; }

        public string DefaultDepartureTime
        {
            get
            {
                return _defaultDepartureTime;        
            }
        }

        public string FormattedDepartureTime
        {
            get
            {
                return DepartureTime.HasValue ? DepartureTime.Value.ToString("H:mm") : string.Empty;
            }
        }

        public IEnumerable<ChoiceModel> ChoiceModels { get; set; }

        [CheckNumberOfSeats("4 places maxi autorisées: t'as pas un bus !", 4)]
        public byte? NumberOfAvailableSeats { get; set; }

        public string IP { get; set; }
    }
}