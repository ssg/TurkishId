// <copyright file="TurkishIdNumber.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License, see LICENSE.txt for details
// </copyright>

namespace TurkishId
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents a valid Turkish ID number
    /// </summary>
    public class TurkishIdNumber
    {
        /// <summary>
        /// Length of a Turkish ID number
        /// </summary>
        public const int Length = 11;

        /// <summary>
        /// Initializes a new instance of the <see cref="TurkishIdNumber"/> class.
        /// </summary>
        /// <param name="number">Turkish ID number</param>
        public TurkishIdNumber(string number)
        {
            if (!IsValid(number))
            {
                throw new ArgumentException("Not a valid Turkish ID number", nameof(number));
            }

            this.Value = number;
        }

        /// <summary>
        /// Gets the value of Turkish ID number
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// A Turkish ID number can always be converted to a string
        /// </summary>
        /// <param name="instance">A Turkish ID number instance</param>
        public static implicit operator string(TurkishIdNumber instance) => instance.Value;

        /// <summary>
        /// Check if given Turkish ID number is valid
        /// </summary>
        /// <param name="number">Input number</param>
        /// <returns>Whether the number is a valid Turkish ID number</returns>
        public static unsafe bool IsValid(string number)
        {
            if (number.Length != Length)
            {
                return false;
            }

            fixed (char* inputPtr = number)
            {
                bool invalid = false;
                char* pInput = inputPtr;
                int oddSum = nextDigit(ref pInput, ref invalid);
                if (oddSum == 0 || invalid)
                {
                    return false;
                }

                int evenSum = 0;
                for (int i = 0; i < 4; i++)
                {
                    evenSum += nextDigit(ref pInput, ref invalid);
                    oddSum += nextDigit(ref pInput, ref invalid);
                }

                int firstChecksum = nextDigit(ref pInput, ref invalid);
                int finalChecksum = nextDigit(ref pInput, ref invalid);
                if (invalid)
                {
                    return false;
                }

                int final = (oddSum + evenSum + firstChecksum) % 10;
                if (finalChecksum != final)
                {
                    return false;
                }

                int first = ((oddSum * 7) - evenSum) % 10;
                if (first < 0)
                {
                    first += 10;
                }

                return firstChecksum == first;
            }
        }

        /// <summary>
        /// Get the string representation of the Turkish ID number
        /// </summary>
        /// <returns>String representation of the Turkish ID number</returns>
        public override string ToString() => Value;

        /// <summary>
        /// Get the hash code for this instance
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Check if another object equals to this instance
        /// </summary>
        /// <param name="obj">Other object to compare with</param>
        /// <returns>Whether both objects represent the same Turkish ID number</returns>
        public override bool Equals(object obj)
        {
            return obj is TurkishIdNumber instance && Value == instance.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe int nextDigit(ref char* ptr, ref bool invalid)
        {
            int result = *ptr++ - '0';
            if (result < 0 || result > 9)
            {
                invalid = true;
            }

            return result;
        }
    }
}