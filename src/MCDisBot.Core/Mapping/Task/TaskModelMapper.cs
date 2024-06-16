using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDisBot.Core.Mapping.Task
{
	public class TaskModelMapper
	{
		public static TaskModel Map(CreateTaskRequest dto)
		{
			return new TaskModel
			{
				Id = dto.Id,
				Content = dto.Content,
				LifeTime = dto.LifeTime,
				Status = TaskStatus.Created,
				ServerId = dto.ServerId,
				UserId = dto.UserId,
				DevId = null,
				Roles = dto.Roles
			};
		}
	}
}
