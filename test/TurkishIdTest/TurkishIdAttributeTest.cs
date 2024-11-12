using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using TurkishId;

namespace TurkishIdNumberTest
{
    [TestFixture]
    class TurkishIdAttributeTest
    {
        [Test]
        [TestCase("1", false)]
        [TestCase("19191919190", true)]
        [TestCase("19191919191", false)]
        [TestCase(19191919190, true)]
        [TestCase(19191919191, false)]
        public void TurkishIdAttribute_ReturnsExpectedResult(object value, bool expectedResult)
        {
            var context = new ValidationContext(value, null, null);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateValue(value, context, results, [new TurkishIdAttribute()]);
            Assert.That(isValid, Is.EqualTo(expectedResult));
        }
    }
}