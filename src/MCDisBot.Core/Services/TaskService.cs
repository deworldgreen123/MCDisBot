using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Mapping.Task;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MCDisBot.Core.Services;

public class TaskService(ITaskRepository repository, ISettingService settingService, ILogger logger) : ITaskService
{
  public async Task<bool> Create(CreateTaskRequest newTask)
  {
    var task = TaskModelMapper.Map(newTask);
    
    if (!await ValidationCheck(task)) return false;
    
    await repository.Add(task);
    await repository.Save();
    
    return true;
  }

  public async Task<bool> Delete(ulong taskId)
  {
    try
    {
      await repository.Remove(taskId);
      await repository.Save();
      logger.LogInformation(@"Удалили задачу с id {taskId}", taskId);
    }
    catch (ArgumentNullException)
    {
      logger.LogError("Попытка удалить не существующую задачу c {taskId}", taskId);
      return false;
    }
    
    return true;
  }

  public Task<TaskStatus> GetStatus(ulong taskId)
  {
    throw new NotImplementedException();
  }

  public Task<TaskModel> GetById(ulong taskId)
  {
    throw new NotImplementedException();
  }

  public Task<bool> AddDevToTask(TaskModel task, ulong devId)
  {
    throw new NotImplementedException();
  }

  private Task<bool> ValidationCheck(TaskModel task)
  {
    throw new NotImplementedException();
  }

  private Task<bool> SendTask(TaskModel task)
  {
    throw new NotImplementedException();
  }
}