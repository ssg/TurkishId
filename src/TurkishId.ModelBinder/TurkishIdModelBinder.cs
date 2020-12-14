using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace TurkishId.ModelBinder
{
    public class TurkishIdModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string modelName = bindingContext.ModelName;
            var result = bindingContext.ValueProvider.GetValue(modelName);
            if (result == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            if (!TurkishIdNumber.TryParse(result.FirstValue, out var value))
            {
                bindingContext.ModelState.AddModelError(modelName, "Invalid value");
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(value);
            return Task.CompletedTask;
        }
    }
}