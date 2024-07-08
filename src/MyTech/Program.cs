using MyTech;
using MyTech.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlite<ApplicationDbContext>(
    builder.Configuration.GetConnectionString("Sqlite"));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

app.MapIdentityApi<User>();

app.Run();
