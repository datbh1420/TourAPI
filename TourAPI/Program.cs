using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using TourAPI.Data;
using TourAPI.Mapping;
using TourAPI.Middleware;
using TourAPI.Models.Domains;
using TourAPI.Repository;
using TourAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            new List<string>{}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    }
);




builder.Services.AddDbContext<TourDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TourDBConnectionString"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
);

#region AddIdentity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<TourDBContext>()
        .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
});
#endregion

#region AddLog
var log = new LoggerConfiguration()
    .WriteTo.File("Logs/", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Warning()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(log);
#endregion

AddDI(builder.Services);



builder.Services.AddFluentValidation(opts =>
{
    opts.ImplicitlyValidateChildProperties = true;
    opts.RegisterValidatorsFromAssemblyContaining<Tour>();
    opts.RegisterValidatorsFromAssemblyContaining<Booking>();
    opts.RegisterValidatorsFromAssemblyContaining<BaseEntity>();
    opts.RegisterValidatorsFromAssemblyContaining<Category>();
    opts.RegisterValidatorsFromAssemblyContaining<Image>();
    opts.RegisterValidatorsFromAssemblyContaining<Review>();
    opts.RegisterValidatorsFromAssemblyContaining<Location>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<WriteLog>();


app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images",
});

app.MapControllers();

app.Run();



void AddDI(IServiceCollection services)
{
    services.AddScoped<BookingRepository>();
    services.AddScoped<CategoryRepository>();
    services.AddScoped<ImageRepository>();
    services.AddScoped<ReviewRepository>();
    services.AddScoped<TourRepository>();
    services.AddScoped<LocationRepository>();
    services.AddScoped<ILocationService, LocationService>();
    services.AddScoped<ITourService, TourService>();
    services.AddScoped<IReviewService, ReviewService>();
    services.AddScoped<IImageService, ImageService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<IBookingService, BookingService>();
    services.AddScoped<IEmailService, EmailService>();
    services.AddAutoMapper(typeof(AutoMapProfile));
}