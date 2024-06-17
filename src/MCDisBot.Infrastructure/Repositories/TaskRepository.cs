using System.Collections.Immutable;
using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MCDisBot.Infrastructure.Repositories;

public class TaskRepository(TaskBotContext context) : ITaskRepository
{
  private readonly TaskBotContext p_context = context;
  
  public async Task<IReadOnlyCollection<TaskModel>> GetAll()
  {
    return await p_context.Tasks.ToListAsync();
  }

  public async Task<TaskModel> GetById(ulong id)
  {
    var res = await p_context.Tasks.SingleOrDefaultAsync(m => m.Id == id);
    if (res is null)
      throw new ArgumentNullException("Не существует модель с типом" + typeof(TaskModel) + "и c Id" + id);
    
    return res;
  }
  
  public bool Exists(ulong id)
  {
    return p_context.Tasks.Any(m => m.Id == id);
  }
  
  public Task Add(TaskModel model)
  {
    p_context.Tasks.Add(model);
    return Task.CompletedTask;
  }

  public async Task Update(TaskModel model)
  {
    await GetById(model.Id);
    p_context.Tasks.Update(model);
  }

  public async Task Remove(ulong id)
  {
    var toDelete = await GetById(id);
    p_context.Tasks.Remove(toDelete);
  }

  public async Task Save()
  {
    await p_context.SaveChangesAsync();
  }
}