using MCDisBot.Core.Enums;
using MCDisBot.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDisBot.Core.Mapping.Task
{
	public class TaskWithDevMapper
	{
		public static TaskModel Map(TaskModel _taskModel, ulong _devId)
		{
			return new TaskModel
			{
				Id = _taskModel.Id,
				Content = _taskModel.Content,
				LifeTime = _taskModel.LifeTime,
				Status = StatusTask.COMPLETED,
				ServerId = _taskModel.ServerId,
				UserId = _taskModel.UserId,
				DevId = _devId,
				Roles = _taskModel.Roles
			};
		}
	}
}
