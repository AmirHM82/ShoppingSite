using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using ShoppingSite.Core.Accessibility.Handlers.Account;
using ShoppingSite.Core.Accessibility.Requirements.Account;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.Moderators.StartupModerators;
using ShoppingSite.Core.Services;
using ShoppingSite.DAL.Context;
using ShoppingSite.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SQL"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

//If u wanna apply aythorize attribute globally: (It means with this code u doesn't need to write [Authorize] in every controller or action)
//builder.Services.AddMvc(options =>
//{
//    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//});

builder.Services.AddCustomeIdentity();  //To retrieve user and role from database using entityframework core

/*
 * We need to change some of the rules for setting password for users
 * (I just write it on AddIdentity method)
 */
//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireUppercase = false;
//});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicies();
});

//Well it seems that it has changed
//builder.Services.AddAuthentication().AddGoogle()

builder.Services.AddDatabaseServices();
builder.Services.AddPolicyServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}
app.UseStaticFiles();

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseHttpsRedirection();

app.Run();
