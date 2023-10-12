// using ASP_Project.Areas.Identity.Data.DbContexts;
// using ASP_Project.Areas.Identity.Data.Extensions;
// using ASP_Project.Areas.Identity.Data.Models;
// using FluentValidation;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using System.Data;
//
// // hey bitch
// namespace ASP_Project.Areas.Admin.Controllers
// {
// 	[Area("Admin")]
// 	[Authorize(Roles = "Admin")]
// 	public class OrderController : Controller
// 	{
// 		private IValidator<Order> _productValidator { get; set; }
//
// 		private readonly UserContext _dbContext;
//
// 		public OrderController(UserContext context, IValidator<Order> validator)
// 		{
// 			_dbContext = context;
// 			_productValidator = validator;
// 		}
//
// 		public IActionResult Index()
// 		{
// 			var categories = _dbContext.Categories.ToList();
// 			if (categories != null)
// 			{
// 				return View(categories);
// 			}
// 			throw new ArgumentException("Order is not valid...");
// 		}
//
//
// 		public IActionResult Create()
// 		{
// 			return View();
// 		}
//
//
// 		//[HttpPost]
// 		//[ValidateAntiForgeryToken]
// 		//public async Task<IActionResult> Create(Order order)
// 		//{
// 			//var result = await _productValidator.ValidateAsync();
//
// 			//if (result.IsValid)
// 			//{
// 			//	_dbContext.Categories.Add(order);
// 			//	_dbContext.SaveChanges();
// 			//	TempData["success"] = "Category created succsessfully";
// 			//	return RedirectToAction("Index", "Category");
// 			//}
// 			//result.AddToModelState(this.ModelState);
//
// 			//return View(order);
// 		//}
//
//
//
// 		public IActionResult Edit(int id)
// 		{
// 			var category = _dbContext.Categories.Find(id);
//
// 			if (category == null)
// 			{
// 				return NotFound();
// 			}
//
// 			return View(category);
// 		}
//
// 		//[HttpPost]
// 		//[ValidateAntiForgeryToken]
// 		//public async Task<IActionResult> Edit(Category category)
// 		//{
// 		//	var result = await _categoryValidator.ValidateAsync(category);
//
// 		//	if (ModelState.IsValid)
// 		//	{
// 		//		_dbContext.Categories.Update(category);
// 		//		_dbContext.SaveChanges();
// 		//		TempData["success"] = "Category updated succsessfully";
//
// 		//		return RedirectToAction("Index", "Category");
// 		//	}
// 		//	result.AddToModelState(this.ModelState);
//
// 		//	return View(category);
// 		//}
//
//
// 		public IActionResult Delete(int id)
// 		{
// 			var category = _dbContext!.Categories!.Find(id);
//
// 			if (category == null)
// 			{
// 				return NotFound();
// 			}
//
// 			return View(category);
// 		}
//
// 		[HttpPost, ActionName("Delete")]
// 		[ValidateAntiForgeryToken]
// 		public IActionResult DeletePost(int id)
// 		{
// 			var category = _dbContext!.Categories!.Find(id);
//
// 			if (category == null)
// 			{
// 				return NotFound();
// 			}
// 			_dbContext.Categories.Remove(category);
// 			_dbContext.SaveChanges();
// 			TempData["success"] = "Category was deleted succsessfully";
//
// 			return RedirectToAction("Index");
// 		}
// 	}
// }
