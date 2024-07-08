using MyTech;
using MyTech.Data;
using MyTech.Domain;
using MyTech.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlite<ApplicationDbContext>(
    builder.Configuration.GetConnectionString("Sqlite"));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<ICollectionsService, CollectionsService>();
builder.Services.AddControllers();
var app = builder.Build();

app.MapIdentityApi<User>();
app.MapControllers();

app.Run();
