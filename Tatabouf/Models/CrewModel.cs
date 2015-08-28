using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
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

    public class CrewModel
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

        public bool MarieBlachere { get; set; }

        public bool Carrefour { get; set; }

        public bool Kebab { get; set; }

        public bool Quick { get; set; }

        public bool Other { get; set; }
        
        [CheckboxControl("Vas-y coche au moins une case !", 1)]
        public byte CheckboxCount
        {
            get
            {
                var count = (MarieBlachere) ? 1 : 0;
                count += (Carrefour) ? 1 : 0;
                count += (Kebab) ? 1 : 0;
                count += (Quick) ? 1 : 0;
                count += (Other) ? 1 : 0;
                return (byte)count;
            }            
        }

        [CheckCountOfSeats("4 places maximum autorisées : t'as pas un bus !", 4)]
        public byte? NumberOfSeatsAvailable { get; set; }
    }
}