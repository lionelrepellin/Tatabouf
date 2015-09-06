using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tatabouf.Domain
{
    public class Place
    {
        public int Id { get; set; }

        public string Label { get; set; }

        /// <summary>
        /// display column order (form left to right)
        /// </summary>
        public byte DisplayOrder { get; set; }
    }
}
