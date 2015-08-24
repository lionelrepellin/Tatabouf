using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tatabouf.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class CheckboxControlAttribute : ValidationAttribute
    {
        private byte _minCheckboxChecked;

        public CheckboxControlAttribute(string errorMessage, byte minCheckboxChecked)
            : base(errorMessage)
        {
            _minCheckboxChecked = minCheckboxChecked;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var checkboxChecked = (byte)value;
                return (_minCheckboxChecked <= checkboxChecked);
            }
            return true;
        }
    }
}