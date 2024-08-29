using Asp.Versioning;
using DentalAppointment.Commands.Commands;
using DentalAppointment.Commands.Handlers;
using DentalAppointment.Core.AutoMapper;
using DentalAppointment.Core.Handlers;
using DentalAppointment.Core.PipelineBehaviour;
using DentalAppointment.Infrastructure.Data;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using DentalAppointment.Infrastructure.Repositories.Implementations;
using DentalAppointment.Queries.Validations;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version")
    );
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers()
                .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dental Appointment WebAPI - V1", Version = "v1.0" });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                    // options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection")); // Exemple pentru MySQL
                    // options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")); // Exemple pentru PostgreSQL
                });

builder.Services
    .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
    .AddTransient<IUnitOfWork, UnitOfWork>()
    .AddTransient<IAppointmentRepository, AppointmentRepository>()
    .AddAutoMapper(typeof(MappingAppointments))
    .AddMemoryCache()
    .AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        cfg.RegisterServicesFromAssembly(typeof(GetAllAppointmentsHandler).Assembly);
        cfg.RegisterServicesFromAssembly(typeof(CreateAppointmentHandler).Assembly);
    })
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
    .AddValidatorsFromAssemblies(new List<Assembly>
    { typeof(CreateAppointmentCommandValidator).Assembly,
      typeof(GetAppointmentByDateTimeQueryValidator).Assembly});

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