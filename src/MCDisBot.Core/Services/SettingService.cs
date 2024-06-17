using MCDisBot.Core.Dto.Setting;
using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Mapping.Setting;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MCDisBot.Core.Services;

public class SettingService(ISettingRepository repository, ILogger<SettingService> logger) : ISettingService
{
  private readonly ISettingRepository p_repository = repository;
  private readonly ILogger<SettingService> p_logger = logger;
  
  public async Task<bool> Add(AddSettingRequest newSetting)
  {
    var setting = new Setting
    {
      ServerId = newSetting.ServerId,
      Roles = newSetting.Roles,
      ChannelClient = newSetting.ChannelClient,
      ChannelDev = newSetting.ChannelDev
    };
    
    await p_repository.Add(setting);
    await p_repository.Save();
    
    return true;;
  }

  public async Task<bool> Update(UpdateSettingRequest newSetting)
  {
    if (p_repository.Exists(newSetting.ServerId))
    {
      var setting = SettingMapper.Map(newSetting);
      
      await p_repository.Update(setting);
      await p_repository.Save();
      
      p_logger.LogInformation(@"Обнавили настройку сервера с id {serverId}", newSetting.ServerId);
    }
    else
    {
      p_logger.LogWarning("Попытка обновить настройку сервера c id {serverId}", newSetting.ServerId);
      return false;
    }
    
    return true;
  }

  public async Task<GetSettingResponse?> GetById(ulong serverId)
  {
    if (!p_repository.Exists(serverId))
    {
      p_logger.LogWarning("Попытка получить не существующую настройку сервера c id {serverId}", serverId);
      return null;
    }
    
    var res = await p_repository.GetById(serverId);
    p_logger.LogInformation(@"Передали настройку сервера с id {serverId}", serverId);
    var request = SettingResponseMapper.Map(res);
    return request;
  }

  public async Task<bool> Delete(ulong serverId)
  {
    if (p_repository.Exists(serverId))
    {
      await p_repository.Remove(serverId);
      await p_repository.Save();
      p_logger.LogInformation(@"Удалили настройку сервера с id {serverId}", serverId);
    }
    else
    {
      p_logger.LogWarning("Попытка удалить настройку сервера c id {serverId}", serverId);
      return false;
    }
    
    return true;
  }
}