using todo.server.Data;
using Microsoft.EntityFrameworkCore;
using todo.server;
using todo.server.Services.Interfaces;
using todo.server.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (env != null && env.Equals("localhost", StringComparison.OrdinalIgnoreCase))
    builder.Services.AddDbContext<DataContext, LocalDataContext>(options => options.UseSqlite("DataSource=Data\\app.db"));

else
    builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDbConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsLocalhost())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
    
if(app.Environment.IsLocalhost())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        if(context != null && context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();
    }
}

app.Run();
