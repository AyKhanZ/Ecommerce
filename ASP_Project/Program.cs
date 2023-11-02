using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using ASP_Project.Services.Classes;
using Microsoft.AspNetCore.Identity.UI.Services;
using ASP_Project.Areas.Identity.Data.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddSingleton<ApplicationUser>();

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<UserContext>();

builder.Services.AddScoped<IValidator<Category>, CategoryValidator>();
builder.Services.AddScoped<IValidator<ParentCategory>, ParentCategoryValidator>();
builder.Services.AddScoped<IValidator<Product>, ProductValidator>(); 
builder.Services.AddScoped<IValidator<Order>, OrderValidator>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
} 


app.MapRazorPages();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");


app.Run();