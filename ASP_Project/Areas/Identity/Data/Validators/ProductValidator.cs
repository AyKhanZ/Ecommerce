using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ASP_Project.Areas.Identity.Data.Validators;
public class ProductValidator : AbstractValidator<Product>
{
	private readonly static string nameMsg; 
	private readonly static string numberOfSIMCardsMsg;
	private readonly static string nuclearNumberMsg;
	private readonly static string categoryMsg;
	private readonly static string priceMsg;
	private readonly static string internalMemoryMsg;
	private readonly static string ramMsg;
	private readonly static string quantityMsg;
	private readonly static string imagesMsg;

	public ProductValidator(UserContext userContext)
	{
		RuleFor(p => p.Name).Must(CheckName).WithMessage(nameMsg).Length(0, 50);
		RuleFor(p => p.NumberOfSIMCards).Must((ns) => ns > 0).WithMessage(numberOfSIMCardsMsg);  
		RuleFor(p => p.NuclearNumber).Must((nn) => nn > 0 ).WithMessage(nuclearNumberMsg);   
		RuleFor(c => c.CategoryId)
			.Must((categoryId) => categoryId >= 0) // Assuming the value -1 represents "Select a category"
			.WithMessage(categoryMsg);
		RuleFor(pv => pv.Price).Must((pv) => pv > 0).WithMessage(priceMsg);
		RuleFor(pv => pv.InternalMemory)
			.Must((im) => im != "-1")
			.WithMessage(internalMemoryMsg);
		RuleFor(pv => pv.RAM)
			.Must((ram) => ram != "-1")
			.WithMessage(ramMsg);
		RuleFor(pv => pv.Quantity).Must((pv) => pv > 0).WithMessage(quantityMsg);
		RuleFor(pv => pv.ProductImages)
			.Must(CheckImages)
			.WithMessage(imagesMsg);
	}
	static ProductValidator()
	{
		nameMsg = "Product\'s name must be between 1 and 50 characters and contain only letters and digits!";
		numberOfSIMCardsMsg = "Choice is required and must be possitive!";
		nuclearNumberMsg = "Choice is required and must be possitive!";
		categoryMsg = "You must select category!";
		priceMsg = "Choice is required and must be possitive!";
		internalMemoryMsg = "You must select category!";
		ramMsg = "You must select category!";
		quantityMsg = "Choice is required and must be possitive!";
		imagesMsg = "Choose images!";
	}
	public static bool CheckName(string? name)
	{
		Regex re = new(@"^[A-Za-z]");
		return name != null && re.IsMatch(name) && name != "";
	}
	private bool CheckImages(List<ProductImage> productImages)
	{
		var a = productImages;
		if (productImages != null && productImages.Count > 0) return true;
		return false;
	}
}
