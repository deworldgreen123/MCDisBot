using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services.Interfaces;

namespace MCDisBot.Core.Services;

public class SettingService(ISettingRepository repository) : ISettingService
{
  public void Add(Setting newSetting)
  {
    throw new NotImplementedException();
  }

  public void Update(Setting newSetting)
  {
    throw new NotImplementedException();
  }

  public Setting GetById(ulong serverId)
  {
    throw new NotImplementedException();
  }

  public void Delete(ulong serverId)
  {
    throw new NotImplementedException();
  }
}