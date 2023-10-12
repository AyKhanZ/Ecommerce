using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using System.Net;
using System.Text.RegularExpressions;

namespace ASP_Project.Areas.Identity.Data.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        private readonly static string makeMsg;
        // private readonly static string imageURLMsg;
        private readonly static string fabricMsg;
        private readonly static string descriptionMsg;

        private readonly UserContext _dbContext;

        public ProductValidator(UserContext userContext)
        {
            _dbContext = userContext;
            RuleFor(p => p.Make).NotEmpty().Must(CheckMake).WithMessage(makeMsg).Length(0, 50);
            // RuleFor(p => p.ImageURLs).Must(CheckImageURL).NotNull().WithMessage(imageURLMsg);
            RuleFor(p => p.Fabric).NotEmpty().Must(CheckFabric).WithMessage(fabricMsg).Length(0, 50); //Min lenght Must be changed
            RuleFor(p => p.Description).NotEmpty().Must(CheckDescription).WithMessage(descriptionMsg).Length(0, 100); //Min lenght must be changed

        }
        static ProductValidator()
        {
            makeMsg = "Product\'s make must be between 1 and 50 characters and contain only letters!";
            // imageURLMsg = "Product\'s image is required!";
            descriptionMsg = "Product\'s description must be between 1 and 100 characters!";
            fabricMsg = "Product\'s fabric must be between 1 and 100 characters!";
        }
        public static bool CheckMake(string? make)
        {
            
            Regex re = new(@"^[A-Za-z]");
            return make != null && re.IsMatch(make);
        }
        public static bool CheckDescription(string? description)
        {
            Regex re = new(@"^[a-zA-Z0-9!.\-;:,\s]+$");
            return description != null && re.IsMatch(description);
        }
        
        public static bool CheckFabric(string? fabric)
        {
            Regex re = new(@"^[a-zA-Z0-9!.\-;:,\s]+$");
            return  fabric != null && re.IsMatch(fabric);
        }
        
        // public static bool CheckImageURL(List<ProductImage> productImages)
        // {
        //     foreach (var productImage in productImages)
        //     {
        //         byte[] imageBytes = productImage.ImageData; // Get the image binary data
        //
        //         if (imageBytes == null || imageBytes.Length == 0)
        //         {
        //             return false; // Return false if any image data is null or empty
        //         }
        //
        //         // You can perform additional checks on the image data if needed.
        //         // For example, you might check the image format or dimensions.
        //
        //         // Here, you can add any additional checks based on the image data.
        //
        //         // For example, you could check the image format using a library like ImageSharp:
        //         // if (!IsValidImageFormat(imageBytes))
        //         // {
        //         //     return false;
        //         // }
        //     }
        //
        //     return true; // Return true if all image data is valid
        // }
        
        // public static bool CheckImageURL(List<ProductImage> imageUrls)
        // {
        //     foreach (var productImage in imageUrls)
        //     {
        //         string imageUrl = productImage.Url; // Assuming ImageUrl is the property containing the URL.
        //
        //         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
        //         request.Method = "HEAD";
        //
        //         try
        //         {
        //             using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //             {
        //                 if (response.StatusCode != HttpStatusCode.OK)
        //                 {
        //                     return false; // Return false if any URL does not return HttpStatusCode.OK
        //                 }
        //             }
        //         }
        //         catch (WebException)
        //         {
        //             // Handle exceptions, e.g., URL not found or unreachable
        //             return false;
        //         }
        //     }
        //
        //     return true; // Return true if all URLs return HttpStatusCode.OK
        // }
        // public static bool CheckImageURL(List<ProductImage> imageUrls)
        // {
        //     foreach (var imageUrl in imageUrls)
        //     {
        //         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
        //         request.Method = "HEAD";
        //
        //         try
        //         {
        //             using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //             {
        //                 if (response.StatusCode != HttpStatusCode.OK)
        //                 {
        //                     return false; // Return false if any URL does not return HttpStatusCode.OK
        //                 }
        //             }
        //         }
        //         catch (WebException)
        //         {
        //             // Handle exceptions, e.g., URL not found or unreachable
        //             return false;
        //         }
        //     }
        //
        //     return true; // Return true if all URLs return HttpStatusCode.OK
        //     // foreach (var imageUrl in imageUrls)
        //     // {
        //     //     HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
        //     //     request.Method = "HEAD";
        //     //     using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //     //     {
        //     //         if (response.StatusCode == HttpStatusCode.OK)
        //     //         {
        //     //             return true;
        //     //         }
        //     //         return false;
        //     //     }
        //     // }
        //     //
        // }
    }
}
