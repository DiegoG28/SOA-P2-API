using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Context;
using Service.IServices;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IEmployeeService, EmployeesService>();
builder.Services.AddTransient<IAssetService, AssetsService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IAuthService, AuthService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

TimeSpan targetTime = new TimeSpan(17, 40, 00);

TimeSpan timeUntilTarget = targetTime - DateTime.Now.TimeOfDay;
if (timeUntilTarget < TimeSpan.Zero)
{
    timeUntilTarget = timeUntilTarget.Add(TimeSpan.FromDays(1)); // Ejecutar en la próxima fecha
}

// Crea un temporizador que ejecutará la tarea en la hora específica
Timer timer = new Timer(_ =>
{
    // Lógica de la tarea a ejecutar
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var emailService = services.GetRequiredService<IEmailService>();
        emailService.SendReminderEmails();
    }
}, null, timeUntilTarget, Timeout.InfiniteTimeSpan);


app.Run();

timer.Dispose();


