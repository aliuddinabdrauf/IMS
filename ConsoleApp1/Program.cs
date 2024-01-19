// See https://aka.ms/new-console-template for more information

using IMS.Infrastructure.DbContext.IMS;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Npgsql;

Console.WriteLine("Hello, World!");
TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);
var test = new Test1();
var test2 = test.Adapt<Test2>();
Console.WriteLine(test2.Id);
Console.WriteLine(test2.Name);

/*var dataSourceBuilder = new NpgsqlDataSourceBuilder("Server=localhost;Port=5432;Database=ims;User Id=ims_user;Password=123456789;");
dataSourceBuilder.MapEnum<UserRole>();
dataSourceBuilder.MapEnum<UserStatus>();
dataSourceBuilder.MapEnum<UserType>();
dataSourceBuilder.UseNodaTime();
var dataSource = dataSourceBuilder.Build();
var optionBuilder = new DbContextOptionsBuilder<ImsContext>();
var context = new ImsContext(optionBuilder.UseNpgsql(dataSource, o => o.UseNodaTime()).Options);

var data = context.TblUsers.ToList();*/

Console.WriteLine("success");

    public class Test1
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "test";
    };

    public class Test3 : Test1
    {
        
    }

    public record Test2(Guid? Id, string? Name)
    {
        public Guid? Id { get; set; } = Id;
    }





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