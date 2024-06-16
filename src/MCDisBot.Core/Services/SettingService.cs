using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services.Interfaces;

namespace MCDisBot.Core.Services;

public class SettingService(ISettingRepository repository) : ISettingService
{
  public Task<bool> Add(Setting newSetting)
  {
    throw new NotImplementedException();
  }

  public Task<bool> Update(Setting newSetting)
  {
    throw new NotImplementedException();
  }

  public Task<Setting> GetById(ulong serverId)
  {
    throw new NotImplementedException();
  }

  public Task<bool> Delete(ulong serverId)
  {
    throw new NotImplementedException();
  }
}