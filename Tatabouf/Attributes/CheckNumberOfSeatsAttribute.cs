using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tatabouf.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class CheckNumberOfSeatsAttribute : ValidationAttribute
    {
        private int _maxSeats;

        public CheckNumberOfSeatsAttribute(string errorMessage, int maxAvailableSeats)
            : base(errorMessage)
        {
            _maxSeats = maxAvailableSeats;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var numberOfSeats = (byte)value;
                if (numberOfSeats < 0 || numberOfSeats > _maxSeats)
                {
                    return false;
                }
            }
            return true;
        }
    }
}