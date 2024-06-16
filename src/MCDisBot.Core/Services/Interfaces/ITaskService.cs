using MCDisBot.Core.Models;
using MCDisBot.Core.Enums;

namespace MCDisBot.Core.Services.Interfaces;

public interface ITaskService
{
  Task Create(TaskModel newTask);
  Task Delete(ulong taskId);
  Task<TaskStatus> GetStatus(ulong taskId);
  Task<TaskModel> GetById(ulong taskId);
  Task AddDevToTask(TaskModel task, ulong devId);
}