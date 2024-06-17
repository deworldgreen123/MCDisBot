using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.Models;
using MCDisBot.Core.Enums;

namespace MCDisBot.Core.Services.Interfaces;

public interface ITaskService
{
  Task<bool> Create(CreateTaskRequest newTask);
  Task<bool> Delete(ulong taskId);
  Task<StatusTask?> GetStatus(ulong taskId);
  Task<GetTaskResponse?> GetById(ulong taskId);
  Task<bool> AddDevToTask(AddDevToTaskRequest request);
}