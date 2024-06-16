using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;

namespace MCDisBot.Infrastructure.Repositories;

public class SettingRepository : ISettingRepository
{
  public Task<IReadOnlyCollection<Setting>> GetAll()
  {
    throw new NotImplementedException();
  }

  public Task<Setting> GetById(ulong id)
  {
    throw new NotImplementedException();
  }

  public Task Add(Setting model)
  {
    throw new NotImplementedException();
  }

  public Task Update(Setting model)
  {
    throw new NotImplementedException();
  }

  public Task Remove(ulong id)
  {
    throw new NotImplementedException();
  }

  public Task Save()
  {
    throw new NotImplementedException();
  }
}