using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.Enums;
using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MCDisBot.Core.Services;

public class TaskService(ITaskRepository taskRepository, ISettingRepository settingRepository, ILogger<TaskService> logger) : ITaskService
{
  private readonly ITaskRepository p_taskRepository = taskRepository;
  private readonly ISettingRepository p_settingRepository = settingRepository;
  private readonly ILogger<TaskService> p_logger = logger;
  
  public async Task<bool> Create(CreateTaskRequest newTask)
  {
    var task = new TaskModel
    {
      Id = newTask.Id,
      Content = newTask.Content,
      LifeTime = newTask.LifeTime,
      Status = StatusTask.CREATED,
      ServerId = newTask.ServerId,
      UserId = newTask.UserId,
      DevId = null,
      Roles = newTask.Roles
    };
    
    if (!await ValidationCheck(task) || p_taskRepository.Exists(newTask.Id)) return false;
    
    await p_taskRepository.Add(task);
    await p_taskRepository.Save();
    
    return true;
  }

  public async Task<bool> Delete(ulong taskId)
  {
    if (p_taskRepository.Exists(taskId))
    {
      await p_taskRepository.Remove(taskId);
      await p_taskRepository.Save();
      p_logger.LogInformation(@"Удалили задачу с id {taskId}", taskId);
    }
    else
    {
      p_logger.LogWarning("Попытка удалить несуществующую задачу c id {taskId}", taskId);
      return false;
    }
    
    return true;
  }

  public async Task<StatusTask?> GetStatus(ulong taskId)
  {
    if (!p_taskRepository.Exists(taskId))
    {
      p_logger.LogWarning("Попытка получить статус несуществующей задачи c id {taskId}", taskId);
      return null;
    }
    
    var res = await p_taskRepository.GetById(taskId);
    p_logger.LogInformation(@"Передали статус задачи с id {taskId}", taskId);
    return res.Status;
  }

  public async Task<GetTaskResponse?> GetById(ulong taskId)
  {
    if (!p_taskRepository.Exists(taskId))
    {
      p_logger.LogWarning("Попытка получить не существующую задачу c id {taskId}", taskId);
      return null;
    }
    
    var res = await p_taskRepository.GetById(taskId);
    p_logger.LogInformation(@"Передали задачу с id {taskId}", taskId);
    var request = new GetTaskResponse
    {
      Content = res.Content,
      LifeTime = res.LifeTime,
      Status = res.Status,
      ServerId = res.ServerId,
      UserId = res.UserId,
      DevId = res.DevId,
      Roles = res.Roles
    };
    
    return request;
  }

  public async Task<bool> AddDevToTask(AddDevToTaskRequest request)
  {
    if (!p_taskRepository.Exists(request.TaskId))
    {
      p_logger.LogInformation(@"Несуществует задачи с id {taskId}", request.TaskId);
      return false;
    }
    
    var res = await p_taskRepository.GetById(request.TaskId);
    if (res.DevId is not null)
      return false;
    
    var taskWithDev = new TaskModel
    {
      Id = res.Id,
      Content = res.Content,
      LifeTime = res.LifeTime,
      Status = StatusTask.COMPLETED,
      ServerId = res.ServerId,
      UserId = res.UserId,
      DevId = request.DevId,
      Roles = res.Roles
    };
    
    await p_taskRepository.Update(taskWithDev);
    await p_taskRepository.Save();
    p_logger.LogInformation(@"Добавили задаче с id {taskId} разработчика с id {devId}", request.TaskId, request.DevId);
    return true;
  }

  private async Task<bool> ValidationCheck(TaskModel task)
  {
    var allRoles = (await p_settingRepository.GetById(task.ServerId)).Roles.Split(" ");
    var roles = task.Roles.Split(" ");
    
    return !roles.Any(role => allRoles.All(r => r != role));
  }

  // Отправляет сообщение в чат разработчиков 
  private Task<bool> SendTask(TaskModel task)
  {
    throw new NotImplementedException();
  }
}