using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using NSubstitute;
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
            var metadataMock = Substitute.For<FakeTurkishIdNumberModelMetadata>();
            var mock = Substitute.For<ModelBinderProviderContext>();
            _ = mock.Metadata.Returns(metadataMock);

            var provider = new TurkishIdModelBinderProvider();
            var result = provider.GetBinder(mock);
            Assert.That(result, Is.InstanceOf<TurkishIdModelBinder>());
        }

        [Test]
        public void GetBinder_ModelTypeIsSomethingElse_ReturnsNull()
        {
            var metadataMock = Substitute.For<FakeStringModelMetadata>();
            var mock = Substitute.For<ModelBinderProviderContext>();
            _ = mock.Metadata.Returns(metadataMock);

            var provider = new TurkishIdModelBinderProvider();
            var result = provider.GetBinder(mock);
            Assert.That(result, Is.Null);
        }
    }
}
