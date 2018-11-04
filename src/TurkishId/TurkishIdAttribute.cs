using System;
using System.ComponentModel.DataAnnotations;

namespace TurkishId
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TurkishIdAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return TurkishIdNumber.IsValid(value?.ToString());
        }
    }
}