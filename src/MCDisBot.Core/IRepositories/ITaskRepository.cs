using MCDisBot.Core.Models;

namespace MCDisBot.Core.IRepositories;

public interface ITaskRepository
{
  Task<IReadOnlyCollection<TaskModel>> GetAll();
  Task<TaskModel> GetById(ulong id);
  Task Add(TaskModel model);
  Task Update(TaskModel model);
  Task<bool> Remove(ulong id);
  Task Save();
}