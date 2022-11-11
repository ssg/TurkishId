// <copyright file="TurkishIdModelBinder.cs" company="Sedat Kapanoglu">
//      Copyright 2014-2022 Sedat Kapanoglu
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TurkishId.ModelBinder
{
    /// <summary>
    /// Model binder for <c ref="TurkishIdNumber"/> class.
    /// </summary>
    public class TurkishIdModelBinder : IModelBinder
    {
        /// <inheritdoc/>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string modelName = bindingContext.ModelName;
            var result = bindingContext.ValueProvider.GetValue(modelName);
            if (result == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            string text = result.FirstValue;

            if (String.IsNullOrWhiteSpace(text))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            if (!TurkishIdNumber.TryParse(text, out var id))
            {
                _ = bindingContext.ModelState.TryAddModelError(
                        modelName,
                        bindingContext.ModelMetadata.ModelBindingMessageProvider
                            .ValueIsInvalidAccessor(result.ToString()));
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(id);
            return Task.CompletedTask;
        }
    }
}