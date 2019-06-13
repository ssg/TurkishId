// <copyright file="TurkishIdNumber.cs" company="Sedat Kapanoglu">
//      Copyright 2014 Sedat Kapanoglu
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>

using System;
using System.Runtime.CompilerServices;

namespace TurkishId
{
    /// <summary>
    /// Represents a valid Turkish ID number.
    /// </summary>
    public class TurkishIdNumber
    {
        /// <summary>
        /// Length of a Turkish ID number.
        /// </summary>
        public const int Length = 11;

        /// <summary>
        /// Initializes a new instance of the <see cref="TurkishIdNumber"/> class.
        /// </summary>
        /// <param name="number">Input text.</param>
        public TurkishIdNumber(string number)
        {
            if (!IsValid(number))
            {
                throw new ArgumentException("Not a valid Turkish ID number", nameof(number));
            }

            this.Value = number;
        }

        /// <summary>
        /// Gets the value of ID number.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Convert a Turkish ID number into a string.
        /// </summary>
        /// <param name="instance">TurkishIDNumber instance.</param>
        public static implicit operator string(TurkishIdNumber instance)
        {
            return instance?.Value ?? string.Empty;
        }

        /// <summary>
        /// Check if given Turkish ID number is valid.
        /// </summary>
        /// <param name="number">Input text.</param>
        /// <returns>true if valid, false otherwise.</returns>
        public static unsafe bool IsValid(string number)
        {
            if (number == null)
            {
                return false;
            }

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

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is TurkishIdNumber instance && this.Value == instance.Value;
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