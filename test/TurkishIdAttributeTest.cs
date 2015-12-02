using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using TurkishId;

namespace TurkishIdNumberTest
{    
    [TestFixture]
    class TurkishIdAttributeTest
    {
        private class Dummy
        {
            [TurkishId]
            public string Id { get; set; }
        }

        [Test]
        [Row("1", false)]
        [Row("19191919190", true)]
        [Row("19191919191", false)]
        [Row(19191919190, true)]
        [Row(19191919191, false)]
        void TurkishIdAttribute_ReturnsExpectedResult(object value, bool expectedResult)
        {
            var context = new ValidationContext(value, null, null);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateValue(value, context, results, new[] { new TurkishIdAttribute() });
            Assert.AreEqual(expectedResult, isValid);
        }
    }
}
