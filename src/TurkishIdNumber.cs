/*
     Copyright 2014 Sedat Kapanoglu

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Globalization;

namespace TurkishId
{
    public class TurkishIdNumber
    {
        public const int Length = 11;

        public const long MinValue = 10000000000;
        public const long MaxValue = 99999999999;

        private long numericValue;
        private string textValue;

        public TurkishIdNumber(string number)
        {
            if (number == null)
            {
                throw new ArgumentNullException("number");
            }
            if (!long.TryParse(number, out numericValue) || !IsValid(numericValue))
            {
                throw new ArgumentException("Invalid value", "number");
            }
            this.textValue = number;
        }

        public TurkishIdNumber(long number)
        {
            if (!IsValid(number))
            {
                throw new ArgumentException("Invalid value", "number");
            }
            this.numericValue = number;
        }

        public string Value
        {
            get
            {
                if (textValue == null)
                {
                    textValue = numericValue.ToString(CultureInfo.InvariantCulture);
                }
                return textValue;
            }
        }

        /// <summary>
        /// Check if given Turkish ID number is valid
        /// </summary>
        public static bool IsValid(string number)
        {
            long value;
            return long.TryParse(number, out value)
                && IsValid(value);
        }

        /// <summary>
        /// Check if given Turkish ID number is valid
        /// </summary>
        public static bool IsValid(long number)
        {
            if (number < MinValue || number > MaxValue)
            {
                return false;
            }
            int finalChecksum = (int)(number % 10);
            int firstChecksum = (int)((number / 10) % 10);
            int x = (int)(number / 100);
            int d1 = x / 100000000;
            int d2 = (x / 10000000) % 10;
            int d3 = (x / 1000000) % 10;
            int d4 = (x / 100000) % 10;
            int d5 = (x / 10000) % 10;
            int d6 = (x / 1000) % 10;
            int d7 = (x / 100) % 10;
            int d8 = (x / 10) % 10;
            int d9 = x % 10;
            int oddSum = d1 + d3 + d5 + d7 + d9;
            int evenSum = d2 + d4 + d6 + d8;
            int result = (oddSum + evenSum + firstChecksum) % 10;
            if (finalChecksum != result)
            {
                return false;
            }
            result = ((oddSum * 7) - evenSum) % 10;
            if (result < 0)
            {
                result += 10;
            }
            return firstChecksum == result;
        }

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return numericValue.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var instance = obj as TurkishIdNumber;
            return instance != null && this.Value == instance.Value;
        }

        public static implicit operator string(TurkishIdNumber instance)
        {
            return instance.Value;
        }
    }
}