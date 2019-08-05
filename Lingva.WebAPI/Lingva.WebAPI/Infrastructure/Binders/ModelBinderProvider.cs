using Lingva.WebAPI.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

namespace Lingva.WebAPI.Infrastructure.Binders
{
    public class ModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(PostsListOptionsModel))
            {
                return new BinderTypeModelBinder(typeof(PostsListOptionsModelBinder));
            }

            if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
            {
                return new BinderTypeModelBinder(typeof(DateTimeModelBinder));
            }

            return null;
        }
    }
}
