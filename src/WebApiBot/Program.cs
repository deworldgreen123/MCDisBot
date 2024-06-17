using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Services;
using MCDisBot.Core.Services.Interfaces;
using MCDisBot.Infrastructure;
using MCDisBot.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders().AddConsole();
var services = builder.Services;

services.AddTransient<ITaskService, TaskService>();
services.AddTransient<ISettingService, SettingService>();
services.AddTransient<ITaskRepository, TaskRepository>();
services.AddTransient<ISettingRepository, SettingRepository>();
services.AddDbContext<TaskBotContext>(options =>
{
  var connection = builder.Configuration.GetConnectionString("Postgres");
  options.UseNpgsql(connection);
});

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1",
    new OpenApiInfo { Title = "test Ocs", Version = "v1" });
});

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();