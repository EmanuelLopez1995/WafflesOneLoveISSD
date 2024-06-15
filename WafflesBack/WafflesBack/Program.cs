using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WafflesBack.AutofacModules;

var builder = WebApplication.CreateBuilder(args);

// Configurar Autofac como el proveedor de servicios
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Configurar archivos de configuración
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Añadir servicios a la colección de servicios de ASP.NET Core
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Configurar Autofac
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new RepositoryModules());
    containerBuilder.RegisterModule(new ServiceModules());
});

var app = builder.Build();

// Configurar el pipeline de solicitud HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("corsapp");
app.UseAuthorization();
app.MapControllers();
app.Run();