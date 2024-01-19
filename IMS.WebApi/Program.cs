using System.Globalization;
using System.Text;
using IMS.Application;
using IMS.Application.Middlewares;
using IMS.Infrastructure;
using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using IMS.Infrastructure.Util;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "ggwp", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
//authentication

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
    };
});*/

//Mapster global config
TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);

//culture info settings
builder.Services.InjectInfrastructureServices();

//register services and repositories from application project
builder.Services.InjectApplicationServices();

//configure serilog
var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Debug()
    .WriteTo.Logger(l => l.Filter
        .ByIncludingOnly(evt => evt.Level is LogEventLevel.Error or LogEventLevel.Warning or LogEventLevel.Fatal)
        .WriteTo.File(path:"Logs/ex_.log", outputTemplate:"{Timestamp:o} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}",
            rollingInterval:RollingInterval.Day, retainedFileCountLimit:7, rollOnFileSizeLimit:true, fileSizeLimitBytes:1000000))
    .WriteTo.Logger(l => l.Filter
        .ByIncludingOnly(evt => evt.Level is LogEventLevel.Information or LogEventLevel.Debug)
        .WriteTo.File(path:"Logs/cp_.log", outputTemplate:"{Timestamp:o} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}",
            rollingInterval:RollingInterval.Day, retainedFileCountLimit:7, rollOnFileSizeLimit:true, fileSizeLimitBytes:1000000))
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("ImsDatabase"));
dataSourceBuilder.UseNodaTime();
dataSourceBuilder.MapEnum<UserRole>();
dataSourceBuilder.MapEnum<UserStatus>();
dataSourceBuilder.MapEnum<AccountType>();
dataSourceBuilder.MapEnum<UserGender>();
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<ImsContext>(options =>
{
    options.UseNpgsql(dataSource, o=>o.UseNodaTime());
});

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();