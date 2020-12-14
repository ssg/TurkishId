using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using NUnit.Framework;
using TurkishId.ModelBinder;

namespace TurkishId.ModelBinderTest
{
    [TestFixture]
    public class TurkishIdModelBinderProviderTest
    {
        [Test]
        public void GetBinder_ModelTypeIsTurkishIdNumber_ReturnsTurkishIdNumberModelBinder()
        {
            var mock = new Mock<ModelBinderProviderContext>();
            _ = mock.Setup(c => c.Metadata.ModelType).Returns(typeof(TurkishIdNumber));

            var provider = new TurkishIdModelBinderProvider();
            var result = provider.GetBinder(mock.Object);
            Assert.That(result, Is.InstanceOf<TurkishIdModelBinder>());
        }

        [Test]
        public void GetBinder_ModelTypeIsSomethingElse_ReturnsNull()
        {
            var mock = new Mock<ModelBinderProviderContext>();
            _ = mock.Setup(c => c.Metadata.ModelType).Returns(typeof(string));

            var provider = new TurkishIdModelBinderProvider();
            var result = provider.GetBinder(mock.Object);
            Assert.That(result, Is.Null);
        }
    }
}
