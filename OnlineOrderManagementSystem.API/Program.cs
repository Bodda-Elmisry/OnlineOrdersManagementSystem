using Microsoft.EntityFrameworkCore;
using OnlineOrderManagementSystem.Inferastructure.Configrations;
using OnlineOrderManagementSystem.Inferastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.add
builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(
                                builder.Configuration.GetConnectionString("DefaultConnection")
                            ).EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Information),
                            ServiceLifetime.Scoped
                    );

DependencyInjectionConfigration.ConfigerRepos(builder.Services);
DependencyInjectionConfigration.ConfigerServices(builder.Services);
DependencyInjectionConfigration.ConfigerUOWs(builder.Services);


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
