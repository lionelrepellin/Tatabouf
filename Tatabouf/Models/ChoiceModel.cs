using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tatabouf.Models
{
    public class ChoiceModel
    {
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        
        [StringLength(15, ErrorMessage = "Choix autre: 15 caractères maximum")]
        public string Other { get; set; }
    }
}