// <copyright file="TurkishIdModelBinderProvider.cs" company="Sedat Kapanoglu">
//      Copyright 2014-2020 Sedat Kapanoglu
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

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TurkishId.ModelBinder
{
    /// <summary>
    /// Model binder provider for <c ref="TurkishIdNumber"/>.
    /// </summary>
    /// <example>
    /// Use this by adding
    /// <code>
    /// options.ModelBinderProviders.Insert(0, new TurkishIdModelBinderProvider());
    /// </code> inside your <code>AddMvc(options => { ... })</code> directive.
    /// </example>
    public class TurkishIdModelBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc/>
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(TurkishIdNumber)
                ? new TurkishIdModelBinder()
                : null;
        }
    }
}
