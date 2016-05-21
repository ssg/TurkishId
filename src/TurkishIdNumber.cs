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
using System.Runtime.CompilerServices;

namespace TurkishId
{
    public class TurkishIdNumber
    {
        public const int Length = 11;

        private string value;

        public TurkishIdNumber(string number)
        {
            if (number == null)
            {
                throw new ArgumentNullException("number");
            }
            if (!IsValid(number))
            {
                throw new ArgumentException("Not a valid Turkish ID number", "number");
            }
            this.value = number;
        }

        public string Value
        {
            get
            {
                return value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe int nextDigit(ref char* ptr, ref bool invalid)
        {
            int result = *ptr++ - '0';
            if (result > 9)
            {
                invalid = true;
            }
            return result;
        }

        /// <summary>
        /// Check if given Turkish ID number is valid
        /// </summary>
        public static unsafe bool IsValid(string number)
        {
            if (number == null || number.Length != 11)
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

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
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