using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.Models;
using MCDisBot.Core.Enums;

namespace MCDisBot.Core.Services.Interfaces;

public interface ITaskService
{
  Task<bool> Create(CreateTaskRequest newTask);
  Task<bool> Delete(ulong taskId);
  Task<TaskStatus> GetStatus(ulong taskId);
  Task<TaskModel> GetById(ulong taskId);
  Task<bool> AddDevToTask(ulong devId);
}