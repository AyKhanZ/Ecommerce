using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ASP_Project.Areas.Identity.Data.Validators;
public class CategoryValidator : AbstractValidator<Category>
{
    private readonly static string nameMsg;
    private readonly static string selectMsg;

    private readonly UserContext _dbContext;

    public CategoryValidator(UserContext userContext)
    {
        _dbContext = userContext;
        RuleFor(c => c.Name)
            .Must((category, name) => CheckName(category.Id, name))
            .WithMessage(nameMsg);
        RuleFor(c => c.ParentCategoryId)
            .Must((parentCategoryId) => parentCategoryId >= 0) // Assuming the value -1 represents "Select a category"
            .WithMessage(selectMsg);
    }

    static CategoryValidator()
    {
        nameMsg = "Category name must be between 1 and 25 characters,contain only letters and be unique!";
        selectMsg = "You must select category!";
    }

    private bool CheckName(int categoryId, string? name)
    {
        Regex re = new(@"^[A-Za-z]");
        if (name != null && re.IsMatch(name) && name.Length > 0 && name.Length <= 25)
        {
            var existingCategory = _dbContext.Categories
                .FirstOrDefault(c => c.Id != categoryId && c.Name == name);

            return existingCategory == null;
        }
        return false;
    }
}