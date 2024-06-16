using MCDisBot.Core.Models;

namespace MCDisBot.Core.Services.Interfaces;

public interface ISettingService
{
  Task Add(Setting newSetting);
  Task Update(Setting newSetting);
  Task<Setting> GetById(ulong serverId);
  Task Delete(ulong serverId);
}