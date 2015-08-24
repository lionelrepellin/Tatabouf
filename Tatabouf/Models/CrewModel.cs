using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tatabouf.Attributes;

namespace Tatabouf.Models
{
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

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(15, ErrorMessage = "15 caractères maximum")]
        public string Name { get; set; }

        public bool MarieBlachere { get; set; }

        public bool Carrefour { get; set; }

        public bool Kebab { get; set; }

        public bool Quick { get; set; }

        public bool Other { get; set; }
        
        [CheckboxControl("Faites votre choix !", 1)]
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

        [CheckCountOfSeats("Maxi 4 places !", 4)]
        public byte? NumberOfSeatsAvailable { get; set; }
    }
}