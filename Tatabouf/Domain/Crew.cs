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

        public bool IGotIt { get; set; }

        public byte? NumberOfSeatsAvailable { get; set; }

        public DateTime InscriptionDate { get; set; }

        public string IpAddress { get; set; }

        public Crew()
        {
            InscriptionDate = DateTime.Now;
        }

        public string CheckAllBoxes()
        {
            var choicesCount = GetChoicesCount();

            if (choicesCount == 0 && !IGotIt)
            {
                return "Merci de cocher au moins une case !";
            }
            else if (IGotIt && choicesCount > 0)
            {
                return "Si tatabouf, pourquoi aller chercher bonheur ailleurs ?";
            }
            else if (IGotIt && NumberOfSeatsAvailable.HasValue && NumberOfSeatsAvailable.Value > 0)
            {
                return "Tatabouf ou tapatabouf ?";
            }

            return string.Empty;
        }

        private int GetChoicesCount()
        {
            var count = (MarieBlachere) ? 1 : 0;
            count += (Carrefour) ? 1 : 0;
            count += (Kebab) ? 1 : 0;
            count += (Quick) ? 1 : 0;
            count += (Other) ? 1 : 0;
            return count;
        }
    }
}