using MCDisBot.Core.Dto.Setting;
using MCDisBot.Core.Models;

namespace MCDisBot.Core.Services.Interfaces;

public interface ISettingService
{
  Task<bool> Add(AddSettingRequest newSetting);
  Task<bool> Update(UpdateSettingRequest newSetting);
  Task<GetSettingResponse?> GetById(ulong serverId);
  Task<bool> Delete(ulong serverId);
}