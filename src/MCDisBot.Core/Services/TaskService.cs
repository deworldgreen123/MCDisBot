using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MCDisBot.Core.Services;

public class TaskService(ITaskRepository repository, ISettingService settingService, ILogger logger) : ITaskService
{
  public async Task<bool> Create(CreateTaskRequest newTask)
  {
    var task = new TaskModel
    {
      Id = newTask.Id,
      Content = newTask.Content,
      LifeTime = newTask.LifeTime,
      Status = TaskStatus.Created,
      ServerId = newTask.ServerId,
      UserId = newTask.UserId,
      DevId = null,
      Roles = newTask.Roles
    };
    
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
      logger.LogError("Попытка удалить не существуещую задачу c {taskId}", taskId);
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

  public Task<bool> AddDevToTask(ulong devId)
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