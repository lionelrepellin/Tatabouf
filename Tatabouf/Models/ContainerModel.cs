using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tatabouf.Models
{
    public class ContainerModel
    {
        public CrewModel Crew { get; set; }

        public IEnumerable<CrewModel> Dates { get; set; }

        public string IpVisitor { get; set; }
    }
}