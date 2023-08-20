using IMSInfrastructure.DbContext.IMS;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("ImsDatabase"));
dataSourceBuilder.UseNodaTime();
dataSourceBuilder.MapEnum<UserRole>();
dataSourceBuilder.MapEnum<UserStatus>();
dataSourceBuilder.MapEnum<UserType>();
dataSourceBuilder.MapEnum<UserGender>();
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<ImsContext>(options =>
{
    options.UseNpgsql(dataSource, o=>o.UseNodaTime());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();