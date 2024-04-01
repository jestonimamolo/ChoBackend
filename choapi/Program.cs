using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using choapi.Models;
using Microsoft.EntityFrameworkCore;
using choapi.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ChoDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString"));
});

//builder.Services.AddDbContext<ChoDBContext>(options => options.UseSqlServer(
//    builder.Configuration.GetConnectionString("DBConnectionString")
//    ));

// DI
builder.Services.AddScoped<IUserDAL, UserDAL>();
builder.Services.AddScoped<IRestaurantDAL, RestaurantDAL>();
builder.Services.AddScoped<IBookingDAL, BookingDAL>();
builder.Services.AddScoped<IRestaurantTableDAL, RestaurantTableDAL>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// JwtBearerDefaults AuthenticationScheme
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Service");
    c.RoutePrefix = string.Empty;  // Set Swagger UI at apps root    
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
