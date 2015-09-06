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
        public int Id { get; set; }

        public Action ActionName
        {
            get
            {
                return (Id == 0) ? Action.Add : Action.Update;
            }
        }

        [Required(ErrorMessage = "C'est toi John Doe ?")]
        [StringLength(15, ErrorMessage = "C'est trop long ! 15 caractères maximum")]
        public string Name { get; set; }

        public IEnumerable<PlaceModel> Choices { get; set; }

        public bool IGotIt { get; set; }
        
        [CheckNumberOfSeats("4 places maxi autorisées: t'as pas un bus !", 4)]
        public byte? NumberOfAvailableSeats { get; set; }

        public string Ip { get; set; }

        public UserModel()
        {
            Choices = new HashSet<PlaceModel>();
        }
    }
}