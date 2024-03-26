using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RaythosAircraft.Areas.Identity.Data;
using RaythosAircraft.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RaythosAircraft_db_ContextConnection") ?? throw new InvalidOperationException("Connection string 'RaythosAircraft_db_ContextConnection' not found.");

//Dependancy injection of db
builder.Services.AddDbContext<RaythosAircraft_db_Context>(options =>
    options.UseSqlServer(connectionString));

//We are disabling this ('changing to false') caz not going to implement the email verification now
//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    //.AddEntityFrameworkStores<RaythosAircraft_db_Context>();

//Add capability of identity roles
builder.Services.AddDefaultIdentity<User>().AddDefaultTokenProviders()
    .AddRoles<IdentityRole>() //Injects identity role
    .AddEntityFrameworkStores<RaythosAircraft_db_Context>();

// Add services to the container - Add Session Services
builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    // Make the session cookie essential
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews(); //Default for conventional controller with seperate views

//Support to view identity Razor pages**
builder.Services.AddRazorPages();

//Disabling a validation in the registration
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure this is before UseAuthorization
app.UseAuthorization();

app.UseSession(); // Ensure this is after UseAuthorization

//Default conventional mvc controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Adding roots for identity Razor pages**
app.MapRazorPages();

app.Run();
