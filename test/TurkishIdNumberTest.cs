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
    [Parallelizable(ParallelScope.Children)]
    public class TurkishIdNumberTest
    {
        private const string validTurkishId = "14948892948";

        [Test]
        public void Ctor_InvalidNumber_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TurkishIdNumber("123"));
        }

        [Test]
        public void Ctor_Valid_SetsValueSuccessfully()
        {
            var result = new TurkishIdNumber(validTurkishId);
            Assert.AreEqual(validTurkishId, result.Value);
            Assert.AreEqual(validTurkishId, result.ToString());
        }

        private static readonly string[] validNumbers = new string[]
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

        private static readonly string?[] invalidNumbers = new string?[]
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
        [TestCaseSource(nameof(validNumbers))]
        public void IsValid_Valid_ReturnsTrue(string number)
        {
            Assert.IsTrue(TurkishIdNumber.IsValid(number));
        }

        [Test]
        [TestCaseSource(nameof(invalidNumbers))]
        public void IsValid_Invalid_ReturnsFalse(string number)
        {
            Assert.IsFalse(TurkishIdNumber.IsValid(number));
        }

        [Test]
        public void Equals_SameValues_ReturnsTrue()
        {
            var id1 = new TurkishIdNumber(validTurkishId);
            var id2 = new TurkishIdNumber(validTurkishId);
            Assert.IsTrue(id1.Equals(id2));
        }

        [Test]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            var id1 = new TurkishIdNumber(validNumbers[0]);
            var id2 = new TurkishIdNumber(validNumbers[1]);
            Assert.IsFalse(id1.Equals(id2));
        }

        [Test]
        public void GetHashCode_SameValue_ReturnsSameHash()
        {
            var id1 = new TurkishIdNumber(validTurkishId);
            var id2 = new TurkishIdNumber(validTurkishId);
            Assert.IsTrue(id1.GetHashCode() == id2.GetHashCode());
        }

        [Test]
        public void StringOperator_ValidValue_ReturnsStringRepresentation()
        {
            var id1 = new TurkishIdNumber(validTurkishId);
            Assert.AreEqual(validTurkishId, (string)id1);
        }
    }
}