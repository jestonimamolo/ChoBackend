using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using choapi.Models;
using Microsoft.EntityFrameworkCore;
using choapi.DAL;
using Microsoft.Extensions.FileProviders;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://cho-web-bm7qsb.flutterflow.app", "https://sama-all.com", "https://cho-mobile-48qrmh.flutterflow.app")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

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
builder.Services.AddScoped<ICategoryDAL, CategoryDAL>();
builder.Services.AddScoped<IUserDAL, UserDAL>();
builder.Services.AddScoped<IEstablishmentDAL, EstablishmentDAL>();
builder.Services.AddScoped<IBookingDAL, BookingDAL>();
builder.Services.AddScoped<IEstablishmentTableDAL, EstablishmentTableDAL>();
builder.Services.AddScoped<IPaymentDAL, PaymentDAL>();
builder.Services.AddScoped<ICuisineDAL, CuisineDAL>();
builder.Services.AddScoped<ICreditDAL, CreditDAL>();
builder.Services.AddScoped<ITransactionDAL, TransactionDAL>();
builder.Services.AddScoped<IManagerDAL, ManagerDAL>();
builder.Services.AddScoped<IPromotionDAL, PromotionDAL>();
builder.Services.AddScoped<IFCMNotificationDAL, FCMNoticationDAL>();
builder.Services.AddScoped<ISaveEstablishmentDAL, SaveEstablishmentDAL>();
builder.Services.AddScoped<IReviewDAL, ReviewDAL>();
builder.Services.AddScoped<ICardDetailsDAL, CardDetailsDAL>();
builder.Services.AddScoped<IAppInfoDAL, AppInfoDAL>();
builder.Services.AddScoped<INotificationDAL, NotificationDAL>();
builder.Services.AddScoped<ILoyaltyDAL, LoyaltyDAL>();
builder.Services.AddScoped<IInviteDAL, InviteDAL>();

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SamaAll REST API");
        c.RoutePrefix = string.Empty;  // Set Swagger UI at apps root    
    });
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Content/Files")),
    RequestPath = "/Content/Files"
});

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
