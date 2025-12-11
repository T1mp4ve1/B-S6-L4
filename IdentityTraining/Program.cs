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
    //option.Password.RequireNonAlphanumeric = true; //
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); //

builder.Services.AddScoped<UserManager<AppUser>>(); //
builder.Services.AddScoped<SignInManager<AppUser>>(); //
builder.Services.AddScoped<RoleManager<IdentityRole>>(); //

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
