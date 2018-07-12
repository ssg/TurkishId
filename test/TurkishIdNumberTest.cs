using System;
using FluentAssertions;
using TurkishId;
using Xunit;

namespace TurkishIdNumberTest
{
    public class TurkishIdNumberTest
    {
        private const string validTurkishId = "14948892948";

        [Fact]
        public void ctor_null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TurkishIdNumber(null));
        }

        [Fact]
        public void ctor_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TurkishIdNumber(null));
        }

        [Fact]
        public void ctor_InvalidNumber_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TurkishIdNumber("123"));
        }

        [Fact]
        public void ctor_Valid_SetsValueSuccessfully()
        {
            var result = new TurkishIdNumber(validTurkishId);
            validTurkishId.Should().Be(result.Value);
                validTurkishId.Should().Be(result.ToString());
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

        [Fact]
        public void IsValid_Valid_ReturnsTrue()
        {
            foreach (var validNumber in validNumbers)
            {
                TurkishIdNumber.IsValid(validNumber).Should().BeTrue();
            }
        }

        [Fact]
        public void IsValid_Invalid_ReturnsFalse()
        {
            foreach (var invalidNumber in invalidNumbers)
            {
                TurkishIdNumber.IsValid(invalidNumber).Should().BeFalse();
            }
        }

        [Fact]
        public void Equals_SameValues_ReturnsTrue()
        {
            var id1 = new TurkishIdNumber(validTurkishId);
            var id2 = new TurkishIdNumber(validTurkishId);
            id1.Should().Be(id2);
        }

        [Fact]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            var id1 = new TurkishIdNumber(validNumbers[0]);
            var id2 = new TurkishIdNumber(validNumbers[1]);
            id1.Should().NotBe(id2);
        }

        [Fact]
        public void Equals_NullTarget_ReturnsFalse()
        {
            var id1 = new TurkishIdNumber(validNumbers[0]);
            TurkishIdNumber id2 = null;
            id1.Should().NotBe(id2);
        }

        [Fact]
        public void GetHashCode_SameValue_ReturnsSameHash()
        {
            var id1 = new TurkishIdNumber(validTurkishId);
            var id2 = new TurkishIdNumber(validTurkishId);
            id1.GetHashCode().Should().Be(id2.GetHashCode());
        }

        [Fact]
        public void stringOperator_ReturnsValue()
        {
            var id1 = new TurkishIdNumber(validTurkishId);
            validTurkishId.Should().Be((string) id1);
        }

        public object ArgumentExceptionvar { get; set; }
    }
}