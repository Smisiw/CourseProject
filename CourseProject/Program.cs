using CourseProject.Data;
using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using CourseProject.Data.Repositories;
using CourseProject.Services.Implementations;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
builder.Services.AddScoped<IBaseRepository<SmartPhone>, SmartPhoneRepository>();
builder.Services.AddScoped<IBaseRepository<Brand>, BrandRepository>();
builder.Services.AddScoped<IBaseRepository<CellPhone>, CellPhoneRepository>();
builder.Services.AddScoped<IBaseRepository<Cart>, CartRepository>();
builder.Services.AddScoped<IBaseRepository<CartItem>, CartItemRepository>();
builder.Services.AddScoped<IBaseRepository<Phone>, PhoneRepository>();
builder.Services.AddScoped<IBaseRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IBaseRepository<OrderItem>, OrderItemRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISmartPhoneService, SmartPhoneService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICellPhoneService, CellPhoneService>();
builder.Services.AddScoped<IPhoneService, PhoneService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
        options.Cookie.Name = "jwtToken";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("user_role", "admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();

app.Run();
