using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tatabouf.Domain
{    
    public class Crew
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool MarieBlachere { get; set; }

        public bool Carrefour { get; set; }

        public bool Kebab { get; set; }

        public bool Quick { get; set; }

        public bool Other { get; set; }

        public byte? NumberOfSeatsAvailable { get; set; }

        public DateTime InscriptionDate { get; set; }

        public Crew()
        {
            InscriptionDate = DateTime.Now;
        }
    }
}