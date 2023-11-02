//using ASP_Project.Areas.Identity.Data.DbContexts;
//using ASP_Project.Areas.Identity.Data.Models;
//using FluentValidation;

//namespace ASP_Project.Areas.Identity.Data.Validators;
//public class ProductVariationValidator : AbstractValidator<ProductVariation>
//{
//    private readonly static string priceMsg;
//    private readonly static string internalMemoryMsg;
//    private readonly static string ramMsg;
//    private readonly static string quantityMsg;
//    private readonly static string imagesMsg;

//    public ProductVariationValidator(UserContext userContext)
//    {
//        RuleFor(pv => pv.Price).Must((pv) => pv > 0).WithMessage(priceMsg);
//        RuleFor(pv => pv.InternalMemory)
//            .Must((im) => im != "-1")
//            .WithMessage(internalMemoryMsg);
//        RuleFor(pv => pv.RAM)
//            .Must((ram) => ram != "-1")
//            .WithMessage(ramMsg);
//        RuleFor(pv => pv.Quantity).Must((pv) => pv > 0).WithMessage(quantityMsg);
//        RuleFor(pv => pv.ProductImages)
//            .Must(CheckImages)
//            .WithMessage(imagesMsg);
//    }
//    static ProductVariationValidator()
//    {
//        priceMsg = "Choice is required and must be possitive!";
//        internalMemoryMsg = "You must select category!";
//        ramMsg = "You must select category!";
//        quantityMsg = "Choice is required and must be possitive!";
//        imagesMsg = "Choose images!";
//    }
//    private bool CheckImages(List<ProductImage> productImages)
//    {
//        var a = productImages;
//        if (productImages != null && productImages.Count > 0) return true;
//        return false;
//    }
//}