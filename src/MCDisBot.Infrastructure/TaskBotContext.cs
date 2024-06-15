using MCDisBot.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MCDisBot.Infrastructure;

public class TaskBotContext(DbContextOptions<TaskBotContext> options) : DbContext(options)
{
  public DbSet<Setting> Settings { get; set; }
  public DbSet<TaskModel> Tasks { get; set; }
}