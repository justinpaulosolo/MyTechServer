using MyTech;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlite<ApplicationDbContext>(
    builder.Configuration.GetConnectionString("Sqlite"));

var app = builder.Build();
app.Run();
