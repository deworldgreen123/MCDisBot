using MCDisBot.Core.Models;

namespace MCDisBot.Core.Services.Interfaces;

public interface ISettingService
{
  void Add(Setting newSetting);
  void Update(Setting newSetting);
  Setting GetById(ulong serverId);
  void Delete(ulong serverId);
}