global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using web_application_task.Data;
using web_application_task.Services.ProfileService;

var builder = WebApplication.CreateBuilder(args);

// Add DB Context Entity Framework
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add mapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
//                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    }).AddCookie(options =>
//    {
//        options.Cookie.Name = "web-login"; // Set the cookie name
//        options.Cookie.HttpOnly = true; // Set other cookie options as needed
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set the expiration time
//        options.LoginPath = "/User/Login"; // Set the login path
//                                           // Add other configuration options as needed
//    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.Name = "token"; // Replace with your desired cookie name
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Set the expiration time for the cookie
    options.LoginPath = "/User/Login"; // Specify the login page URL
});

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


