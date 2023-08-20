// See https://aka.ms/new-console-template for more information

using IMSInfrastructure.DbContext.IMS;
using Microsoft.EntityFrameworkCore;
using Npgsql;

Console.WriteLine("Hello, World!");

var dataSourceBuilder = new NpgsqlDataSourceBuilder("Server=localhost;Port=5432;Database=ims;User Id=ims_user;Password=123456789;");
dataSourceBuilder.MapEnum<UserRole>();
dataSourceBuilder.MapEnum<UserStatus>();
dataSourceBuilder.MapEnum<UserType>();
dataSourceBuilder.UseNodaTime();
var dataSource = dataSourceBuilder.Build();
var optionBuilder = new DbContextOptionsBuilder<ImsContext>();
var context = new ImsContext(optionBuilder.UseNpgsql(dataSource, o => o.UseNodaTime()).Options);

var data = context.TblUser.ToList();

Console.WriteLine("success");

// context.TblUser.Update(new TblUser()
// {
//     Id = new Guid("661450ae-c96e-4287-ac8c-4c66bf0d4141"),
//     Email = "test@x.com",
//     Password = "test",
//     Status = UserStatus.NeedActivation,
//     Roles = new UserRole[] { UserRole.User, UserRole.Admin },
//     Type = UserType.Student
// });
// context.SaveChanges();