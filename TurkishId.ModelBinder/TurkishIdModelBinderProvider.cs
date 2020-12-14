using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace TurkishId.ModelBinder
{
    public class TurkishIdModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(TurkishIdNumber) 
                ? new TurkishIdModelBinder() 
                : null;
        }
    }
}
