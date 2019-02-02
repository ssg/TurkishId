// <copyright file="TurkishIdAttribute.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License, see LICENSE.txt for details
// </copyright>

namespace TurkishId
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Validation attribute for Turkish ID numbers
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TurkishIdAttribute : ValidationAttribute
    {
        /// <summary>
        /// Check if given input is a valid Turkish ID number
        /// </summary>
        /// <param name="value">Input value</param>
        /// <returns>Whether the input is a valid Turkish ID number</returns>
        public override bool IsValid(object value)
        {
            return TurkishIdNumber.IsValid(value?.ToString());
        }
    }
}