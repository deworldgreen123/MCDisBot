using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services.Interfaces;

namespace MCDisBot.Core.Services;

public class TaskService(ITaskRepository repository, ISettingService settingService) : ITaskService
{
  public Task Create(TaskModel newTask)
  {
    throw new NotImplementedException();
  }

  public Task Delete(ulong taskId)
  {
    throw new NotImplementedException();
  }

  public Task<TaskStatus> GetStatus(ulong taskId)
  {
    throw new NotImplementedException();
  }

  public Task<TaskModel> GetById(ulong taskId)
  {
    throw new NotImplementedException();
  }

  public Task AddDevToTask(ulong devId)
  {
    throw new NotImplementedException();
  }

  private Task<bool> ValidationCheck(TaskModel task)
  {
    throw new NotImplementedException();
  }

  private Task SendTask(TaskModel task)
  {
    throw new NotImplementedException();
  }
}