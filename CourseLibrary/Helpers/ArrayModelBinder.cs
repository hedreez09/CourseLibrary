using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CourseLibrary.Helpers
{
	public class ArrayModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			//Our binder wroks only on enumerabe types
			if (!bindingContext.ModelMetadata.IsEnumerableType)
			{
				bindingContext.Result = ModelBindingResult.Failed();
				return Task.CompletedTask;
			}

			//Get the inpitted value through the vaalue provider
			var value = bindingContext.ValueProvider
				.GetValue(bindingContext.ModelName).ToString();

			//If that vlaue is null or whitespace, we return null
			if(string.IsNullOrWhiteSpace(value))
			{
				bindingContext.Result = ModelBindingResult.Success(null);
				return Task.CompletedTask;
			}


			//The value isn't null whitespace,
			//and the that of model is enumerable.
			//Get The enumerable's type, and a converter

			var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
			var converter = TypeDescriptor.GetConverter(elementType);


			//Convert each item in the value list to the enumerable type
			var values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => converter.ConvertFromString(x.Trim())).ToArray();

			//Create an array of that type and set it to a model Value
			var typedValues = Array.CreateInstance(elementType, values.Length);
			values.CopyTo(typedValues, 0);
			bindingContext.Model = typedValues;

			//return a successful result , passing in the model
			bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
			return Task.CompletedTask;
		}
	}
}
