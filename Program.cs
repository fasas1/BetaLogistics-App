using AutoMapper;
using BetaLogistics.Data;
using BetaLogistics.Repository.IRepository;
using BetaLogistics.Repository;
using Microsoft.EntityFrameworkCore;
using QuickLogistics;
using BetaLogistics.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using BetaLogistics.Hubs;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers(opts =>
{
    opts.CacheProfiles.Add("Default30",
       new CacheProfile()
       {
           Duration = 30
       });
})
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
       
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Number of retry attempts
            maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
            errorNumbersToAdd: null); // List of additional error numbers to consider transient
    });
});

builder.Services.AddResponseCaching();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSignalR();


var apiVersioningBuilder= builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified= true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
apiVersioningBuilder.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});



builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();

var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(u =>
{
    u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(u =>
{
    u.RequireHttpsMetadata = false;
    u.SaveToken = true;
    u.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
            "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
           new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0",
        Title = "Beta Logistics v1",
        Description = "API to manage logistics",
        TermsOfService = new Uri("https://example.com/terms"),

        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2.0",
        Title = "Beta Logisticsnv2",
        Description = "API to manage logistics",
        TermsOfService = new Uri("https://example.com/terms"),

        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();     

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
 {
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "BetaLogisticsV2");
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "BetaLogisticsV1");      
    });
 }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<TrackingHub>("/hubs/chatHub");

app.Run();
