using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ShopOnline.Api.Data;
using ShopOnline.Api.Repositories;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Api.Respositories;
using ShopOnline.Api.Respositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<ShopOnlineDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopOnlineConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

var app = builder.Build();

// start Development-only code
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors(policy =>
    policy.WithOrigins(
        Environment.GetEnvironmentVariable("ShopOnlineServerHttp"), 
        Environment.GetEnvironmentVariable("ShopOnlineServerHttps"))
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
);
// end Development-only code

// start Production-only code
/*
app.UseCors(policy =>
    policy.WithOrigins("https://shoponlineclient2.azurewebsites.net")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
);
*/
// end Production-only code

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
