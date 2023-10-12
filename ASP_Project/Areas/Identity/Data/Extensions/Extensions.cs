using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASP_Project.Areas.Identity.Data.Extensions;
public static class Extensions
{
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}
