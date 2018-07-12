using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using TurkishId;
using Xunit;

namespace TurkishIdNumberTest
{
    public class TurkishIdAttributeTest
    {
        private class Dummy
        {
            [TurkishId]
            public string Id { get; set; }
        }

        [Theory]
        [InlineData("1", false)]
        [InlineData("19191919190", true)]
        [InlineData("19191919191", false)]
        [InlineData(19191919190, true)]
        [InlineData(19191919191, false)]
        public void TurkishIdAttribute_ReturnsExpectedResult(object value, bool expectedResult)
        {
            var context = new ValidationContext(value, null, null);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateValue(value, context, results, new[] { new TurkishIdAttribute() });
            isValid.Should().Be(expectedResult);
        }
    }
}