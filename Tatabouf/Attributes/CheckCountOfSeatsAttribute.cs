using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tatabouf.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class CheckCountOfSeatsAttribute : ValidationAttribute
    {
        private int _maxSeats;

        public CheckCountOfSeatsAttribute(string errorMessage, int maxSeatsAvaible)
            : base(errorMessage)
        {
            _maxSeats = maxSeatsAvaible;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var nbPlaces = (byte)value;
                if(nbPlaces > _maxSeats)
                {
                    return false;
                }
            }
            return true;
        }
    }
}