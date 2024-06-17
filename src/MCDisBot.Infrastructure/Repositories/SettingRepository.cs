using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MCDisBot.Infrastructure.Repositories;

public class SettingRepository(TaskBotContext context) : ISettingRepository
{
  private readonly TaskBotContext p_context = context;
  
  public async Task<IReadOnlyCollection<Setting>> GetAll()
  {
    return await p_context.Settings.ToListAsync();
  }

  public async Task<Setting> GetById(ulong id)
  {
    var res = await p_context.Settings.SingleOrDefaultAsync(m => m.ServerId == id);
    if (res is null)
      throw new ArgumentNullException("Не существует модель с типом" + typeof(TaskModel) + "и c Id" + id);
    
    return res;
  }
  
  public bool Exists(ulong id)
  {
    return p_context.Settings.Any(m => m.ServerId == id);
  }
  
  public Task Add(Setting model)
  {
    p_context.Settings.Add(model);
    return Task.CompletedTask;
  }

  public async Task Update(Setting model)
  {
    await GetById(model.ServerId);
    p_context.Settings.Update(model);
  }

  public async Task Remove(ulong id)
  {
    var toDelete = await GetById(id);
    p_context.Settings.Remove(toDelete);
  }

  public async Task Save()
  {
    await p_context.SaveChangesAsync();
  }
}