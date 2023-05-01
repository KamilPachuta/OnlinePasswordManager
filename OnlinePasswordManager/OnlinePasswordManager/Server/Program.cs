using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using OnlinePasswordManager.Server;
using OnlinePasswordManager.Server.Data.Context;
using OnlinePasswordManager.Server.Data.Entities;
using OnlinePasswordManager.Server.Data.Validators;
using OnlinePasswordManager.Server.Middleware;
using OnlinePasswordManager.Server.Services.PasswordService;
using OnlinePasswordManager.Server.Services.UserContextService;
using OnlinePasswordManager.Server.Services.UserService;
using OnlinePasswordManager.Shared.Models.DTO;
using OnlinePasswordManager.Shared.Models.Validators;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

builder.Services.AddControllersWithViews();//.AddFluentValidation()
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<OnlinePasswordManagerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OnlinePasswordManagerConnection")));
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<UserRegisterDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<UserLoginDto>, LoginUserDtoValidator>();
builder.Services.AddHttpContextAccessor();

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
