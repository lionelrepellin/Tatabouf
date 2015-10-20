using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tatabouf.Domain
{
    public class Choice
    {
        public int UserId { get; set; }

        public int PlaceId { get; set; }

        public string OtherIdea { get; set; }

        public Place Place { get; set; }

        public User User { get; set; }
    }
}