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

        /// <summary>
        /// input checkbox or input text 
        /// </summary>
        /// <example>True = Text</example>
        public bool InputType { get; set; }

        /// <summary>
        /// Css class used where choice is selected
        /// </summary>
        public string Css { get; set; }

        public byte Priority { get; set; }

        public virtual ICollection<Choice> Choices { get; set; }
    }
}
