using Common.Interfaces.Service;
using Common.Repository;
using Data;
using Microsoft.EntityFrameworkCore;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<Context>(opt => opt.UseSqlServer(connectionString, sql => sql.MigrationsAssembly("Data")))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IEmployeeShiftService, EmployeeShiftService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corsapp");
app.UseAuthorization();

app.MapControllers();

app.Run();
