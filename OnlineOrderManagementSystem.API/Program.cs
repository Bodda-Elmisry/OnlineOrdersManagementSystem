using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OnlineOrderManagementSystem.Domain.DTOs.Custom;
using OnlineOrderManagementSystem.Domain.ModelsConfigrations;
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

builder.Services.Configure<AppSettingDTO>(builder.Configuration.GetSection("AppSettings"));

DependencyInjectionConfigration.ConfigerRepos(builder.Services);
DependencyInjectionConfigration.ConfigerServices(builder.Services);
DependencyInjectionConfigration.ConfigerUOWs(builder.Services);

builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
builder.Services.AddScoped<IMapper, ServiceMapper>();

MapsterConfig.RegisterMappings();


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
