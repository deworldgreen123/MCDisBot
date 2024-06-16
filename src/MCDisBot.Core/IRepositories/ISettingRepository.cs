using MCDisBot.Core.Models;

namespace MCDisBot.Core.IRepositories;

public interface ISettingRepository
{
  Task<IReadOnlyCollection<Setting>> GetAll();
  Task<Setting> GetById(ulong id);
  Task Add(Setting model);
  Task Update(Setting model);
  Task Remove(ulong id);
  Task Save();
}