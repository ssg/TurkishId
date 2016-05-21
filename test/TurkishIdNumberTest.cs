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
using NUnit.Framework;
using TurkishId;

namespace TurkishIdNumberTest
{
    [TestFixture]
    [Parallelizable]
    public class TurkishIdNumberTest
    {
        private const string validTurkishId = "14948892948";

        [Test]
        [Parallelizable]
        public void ctor_null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TurkishIdNumber(null));
        }

        [Test]
        [Parallelizable]
        public void ctor_NumericValue_ReturnCorrectsValue()
        {
            var id = new TurkishIdNumber(long.Parse(validTurkishId));
            Assert.AreEqual(validTurkishId, id.Value);
        }

        [Test]
        [Parallelizable]
        [TestCaseSource("invalidNumbers")]
        public void ctor_InvalidNumericValue_ThrowsArgumentException(string number)
        {
            long value;
            if (long.TryParse(number, out value))
            {
                Assert.Throws<ArgumentException>(() => new TurkishIdNumber(long.Parse(number)));
            }
        }

        [Test]
        [Parallelizable]
        [TestCaseSource("invalidNumbers")]
        public void ctor_Invalid_ThrowsArgumentException(string number)
        {
            if (number == null)
            {
                Assert.Throws<ArgumentNullException>(() => new TurkishIdNumber(number));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => new TurkishIdNumber(number));
            }
        }

        [Test]
        [Parallelizable]
        public void ctor_Valid_SetsValueSuccessfully()
        {
            var result = new TurkishIdNumber(validTurkishId);
            Assert.AreEqual(validTurkishId, result.Value);
            Assert.AreEqual(validTurkishId, result.ToString());
        }

        private static string[] validNumbers = new string[]
        {
            validTurkishId,
            "19191919190",
            "76558242278",
            "80476431508",
            "76735508630",
            "90794350894",
            "43473624496",
            "10000000146",
            "56673392584",
            "29260807600",
            "93212606504",
            "35201408508",
            "64404737702",
        };

        private static string[] invalidNumbers = new string[]
        {
            null,
            "04948892948", // first digit zero
            "14948892946", // last digit invalid
            "14948892937", // last second digit invalid

            // non numeric chars
            "A4948892948",
            "7B558242278",
            "80C76431508",
            "767D5508630",
            "9079E350894",
            "43473F24496",
            "566733G2584",
            "2926080H600",
            "93212606I04",
            "352014085J8",
            "3520140853K",

            // uneven length
            "7",
            "76",
            "76558",
            "765582",
            "7655824",
            "76558242",
            "765582422",
            "7655824227",
            "765582422781",

            // spaces
            " 765582422781",
            "765582422781 ",
        };

        [Test]
        [Parallelizable]
        [TestCaseSource("validNumbers")]
        public void IsValid_Valid_ReturnsTrue(string number)
        {
            Assert.IsTrue(TurkishIdNumber.IsValid(number));
        }

        [Test]
        [Parallelizable]
        [TestCaseSource("invalidNumbers")]
        public void IsValid_Invalid_ReturnsFalse(string number)
        {
            Assert.IsFalse(TurkishIdNumber.IsValid(number));
        }

        [Test]
        [Parallelizable]
        [TestCaseSource("validNumbers")]
        public void IsValidInt_Valid_ReturnsTrue(string number)
        {
            Assert.IsTrue(TurkishIdNumber.IsValid(long.Parse(number)));
        }

        [Test]
        [Parallelizable]
        [TestCaseSource("invalidNumbers")]
        public void IsValidInt_Invalid_ReturnsFalse(string number)
        {
            long value;
            if (long.TryParse(number, out value))
            {
                Assert.IsFalse(TurkishIdNumber.IsValid(value));
            }
        }

        [Test]
        [Parallelizable]
        public void Equals_SameValues_ReturnsTrue()
        {
            var id1 = new TurkishIdNumber(validTurkishId);
            var id2 = new TurkishIdNumber(validTurkishId);
            Assert.IsTrue(id1.Equals(id2));
        }

        [Test]
        [Parallelizable]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            var id1 = new TurkishIdNumber(validNumbers[0]);
            var id2 = new TurkishIdNumber(validNumbers[1]);
            Assert.IsFalse(id1.Equals(id2));
        }

        [Test]
        [Parallelizable]
        public void Equals_NullTarget_ReturnsFalse()
        {
            var id1 = new TurkishIdNumber(validNumbers[0]);
            TurkishIdNumber id2 = null;
            Assert.IsFalse(id1.Equals(id2));
        }

        [Test]
        [Parallelizable]
        public void GetHashCode_SameValue_ReturnsSameHash()
        {
            var id1 = new TurkishIdNumber(validTurkishId);
            var id2 = new TurkishIdNumber(validTurkishId);
            Assert.IsTrue(id1.GetHashCode() == id2.GetHashCode());
        }

        [Test]
        [Parallelizable]
        public void stringOperator_ReturnsValue()
        {
            var id1 = new TurkishIdNumber(validTurkishId);
            Assert.AreEqual(validTurkishId, (string)id1);
        }

        public object ArgumentExceptionvar { get; set; }
    }
}