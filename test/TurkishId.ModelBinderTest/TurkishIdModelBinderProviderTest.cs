using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Moq;
using NUnit.Framework;
using TurkishId.ModelBinder;

namespace TurkishId.ModelBinderTest
{
    [TestFixture]
    public class TurkishIdModelBinderProviderTest
    {
        public abstract class FakeStringModelMetadata : ModelMetadata
        {
            public FakeStringModelMetadata()
                : base(ModelMetadataIdentity.ForType(typeof(string)))
            {
            }
        }

        public abstract class FakeTurkishIdNumberModelMetadata : ModelMetadata
        {
            public FakeTurkishIdNumberModelMetadata()
                : base(ModelMetadataIdentity.ForType(typeof(TurkishIdNumber)))
            {
            }
        }

        [Test]
        public void GetBinder_ModelTypeIsTurkishIdNumber_ReturnsTurkishIdNumberModelBinder()
        {
            var metadataMock = new Mock<FakeTurkishIdNumberModelMetadata>();
            var mock = new Mock<ModelBinderProviderContext>();
            _ = mock.SetupGet(c => c.Metadata).Returns(metadataMock.Object);

            var provider = new TurkishIdModelBinderProvider();
            var result = provider.GetBinder(mock.Object);
            Assert.That(result, Is.InstanceOf<TurkishIdModelBinder>());
        }

        [Test]
        public void GetBinder_ModelTypeIsSomethingElse_ReturnsNull()
        {
            var metadataMock = new Mock<FakeStringModelMetadata>();
            var mock = new Mock<ModelBinderProviderContext>();
            _ = mock.SetupGet(c => c.Metadata).Returns(metadataMock.Object);

            var provider = new TurkishIdModelBinderProvider();
            var result = provider.GetBinder(mock.Object);
            Assert.That(result, Is.Null);
        }
    }
}
