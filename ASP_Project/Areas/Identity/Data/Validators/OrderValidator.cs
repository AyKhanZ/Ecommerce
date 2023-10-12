using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using System.Net;
using System.Text.RegularExpressions;

namespace ASP_Project.Areas.Identity.Data.Validators
{
	public class OrderValidator : AbstractValidator<Order>
	{
		private readonly static string nameMsg;
		private readonly static string imageURLMsg;
		private readonly static string descriptionMsg;

		private readonly UserContext _dbContext;

		public OrderValidator(UserContext userContext)
		{
			_dbContext = userContext;
			

		}
		static OrderValidator()
		{
			
		}
		public static bool CheckName(string name)
		{
			Regex re = new(@"^[A-Za-z]");
			return re.IsMatch(name);
		}
		
	}
}
