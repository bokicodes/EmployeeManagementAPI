using EmployeeManagement.Application.Helpers;
using EmployeeManagement.Application.ServiceInterfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.RepositoryInterfaces;
using EmployeeManagement.Infrastructure.DatabaseContext;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EmployeeManagementDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IZaposleniRepository, ZaposleniRepository>();
builder.Services.AddScoped<IRadnoMestoRepository, RadnoMestoRepository>();
builder.Services.AddScoped<IOrgCelinaRepository, OrgCelinaRepository>();
builder.Services.AddScoped<IDodeljenZadatakRepository, DodeljenZadatakRepository>();

builder.Services.AddScoped<IZaposleniService, ZaposleniService>();
builder.Services.AddScoped<IRadnoMestoService, RadnoMestoService>();
builder.Services.AddScoped<IOrgCelinaService, OrgCelinaService>();
builder.Services.AddScoped<IDodeljenZadatakService, DodeljenZadatakService>();

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
