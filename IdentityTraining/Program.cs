using IdentityTraining.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => //
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //

builder.Services.AddIdentity<AppUser, IdentityRole>(options => //
{
    options.SignIn.RequireConfirmedPhoneNumber = false; //
    options.SignIn.RequireConfirmedEmail = false; //
    options.SignIn.RequireConfirmedAccount = false; //
    options.Password.RequiredLength = 8; //
    options.Password.RequireDigit = true; //
    options.Password.RequireUppercase = true; //
    options.Password.RequireLowercase = true; //
    options.Password.RequireNonAlphanumeric = false; //
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); //

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
