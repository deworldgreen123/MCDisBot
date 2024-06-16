using MCDisBot.Core.Models;

namespace MCDisBot.Core.Services.Interfaces;

public interface ISettingService
{
  Task<bool> Add(Setting newSetting);
  Task<bool> Update(Setting newSetting);
  Task<Setting> GetById(ulong serverId);
  Task<bool> Delete(ulong serverId);
}