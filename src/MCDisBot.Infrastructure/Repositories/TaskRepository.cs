using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;

namespace MCDisBot.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
  public Task<IReadOnlyCollection<TaskModel>> GetAll()
  {
    throw new NotImplementedException();
  }

  public Task<TaskModel> GetById(ulong id)
  {
    throw new NotImplementedException();
  }

  public Task Add(TaskModel model)
  {
    throw new NotImplementedException();
  }

  public Task Update(TaskModel model)
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