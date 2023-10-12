using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation; 
using System.Text.RegularExpressions;

namespace ASP_Project.Areas.Identity.Data.Validators;
public class ParentCategoryValidator : AbstractValidator<ParentCategory>
{
    private readonly static string nameMsg;

    private readonly UserContext _dbContext;

    public ParentCategoryValidator(UserContext userContext)
    {
        _dbContext = userContext;
        RuleFor(pc => pc.Name)
            .Must((parentCategory, name) => CheckName(parentCategory.Id, name))
            .WithMessage(nameMsg);
    }

    static ParentCategoryValidator()
    {
        nameMsg = "Parent category name must be between 1 and 25 characters,contain only letters and be unique!"; 
    } 

    private bool CheckName(int parentCategoryId, string? name)
    {
        Regex re = new(@"^[A-Za-z]");
        if (name != null && re.IsMatch(name) && name.Length > 0 && name.Length <= 25)
        {
            var existingParentCategory = _dbContext.ParentCategories
                .FirstOrDefault(c => c.Id != parentCategoryId && c.Name == name);

            return existingParentCategory == null;
        }
        return false;
    }
}