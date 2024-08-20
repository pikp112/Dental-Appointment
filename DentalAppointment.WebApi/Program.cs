using DentalAppointment.Infrastructure.Data;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using DentalAppointment.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    // options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection")); // Exemple pentru MySQL
    // options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")); // Exemple pentru PostgreSQL
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
    .AddAutoMapper(Assembly.GetExecutingAssembly())
    .AddMemoryCache();

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