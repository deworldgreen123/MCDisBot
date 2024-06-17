using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDisBot.Core.Mapping.Task
{
	public class TaskResponseMapper
	{
		public static GetTaskResponse Map(TaskModel _taskModel)
		{
			return new GetTaskResponse
			{
				Content = _taskModel.Content,
				LifeTime = _taskModel.LifeTime,
				Status = _taskModel.Status,
				ServerId = _taskModel.ServerId,
				UserId = _taskModel.UserId,
				DevId = _taskModel.DevId,
				Roles = _taskModel.Roles
			};
		}
	}
}
