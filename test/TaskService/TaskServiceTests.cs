using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services;
using MCDisBot.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;

namespace test.TaskService
{
	public class TaskServiceTests : IClassFixture<TaskServiceFixture>
	{
		private readonly ITaskRepository p_taskRepository;
		private readonly ITaskService p_taskService;
		private readonly ISettingRepository p_settingRepository;
		private readonly ILogger<MCDisBot.Core.Services.TaskService> p_logger;
		private readonly TaskServiceFixture p_fixture;
		public TaskServiceTests(TaskServiceFixture _taskServiceFixture, ILogger<MCDisBot.Core.Services.TaskService> _logger)
		{
			p_logger = _logger;
			p_fixture = _taskServiceFixture;
			p_taskRepository = p_fixture.TaskRepositoryMock.Object;
			p_settingRepository = p_fixture.SettingServiceMock.Object;
			p_taskService = new MCDisBot.Core.Services.TaskService(p_taskRepository, p_settingRepository, p_logger);
		}

		[Fact]
		public async Task Given_service_is_successfully_create_task()
		{
			//arrange
			var task = new CreateTaskRequest { Id = 11, Content = "Content11", LifeTime = 30, ServerId = 1111, UserId = 2211, DevId = 3311, Roles = "Junior Backend" };

			//act
			var actual = p_taskService.Create(task).Result;

			//assert
			Assert.True(actual);
		}

		[Fact]
		public async Task Given_service_return_false_When_task_data_invalid()
		{
			//arrange
			var task = new CreateTaskRequest { Id = 0, Content = "Content11", LifeTime = 30, ServerId = 1111, UserId = 2211, DevId = 3311, Roles = "Junior Backend" };

			//act
			var actual = p_taskService.Create(task).Result;

			//assert
			Assert.False(actual);
		}

		[Fact]
		public async Task Given_service_is_successfully_remove_task()
		{
			//arrange
			var task = new CreateTaskRequest { Id = 22, Content = "Content22", LifeTime = 30, ServerId = 2222, UserId = 2222, DevId = 3322, Roles = "Junior Backend" };
			await p_taskService.Create(task);

			//act
			var actual = p_taskService.Delete(task.Id).Result;

			//assert
			Assert.True(actual);
		}

		[Fact]
		public async Task Given_service_return_false_When_not_founded_task_to_remove()
		{
			//arrange
			ulong actualId = 12345678;

			//act
			var actual = p_taskService.Delete(actualId).Result;

			//assert
			var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => p_taskService.Delete(actualId));
			Assert.False(actual);
		}

		[Fact]
		public async Task Given_service_is_successfully_return_status()
		{
			//arrange
			var task = new CreateTaskRequest { Id = 33, Content = "Content33", LifeTime = 30, ServerId = 3333, UserId = 3333, DevId = 3333, Roles = "Junior Backend" };
			await p_taskService.Create(task);

			//act
			var actual = p_taskService.GetStatus(task.Id).Result;

			//assert
			Assert.Equal(MCDisBot.Core.Enums.StatusTask.CREATED, actual);
		}

		[Fact]
		public async Task Given_service_return_exception_When_there_is_no_task()
		{
			//arrange
			ulong expectedId = 9999;

			//act

			//assert
			var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => p_taskService.GetStatus(expectedId));
		}

		[Fact]
		public async Task Given_service_is_successfully_return_task_by_id()
		{
			//arrange
			var task = new CreateTaskRequest { Id = 44, Content = "Content44", LifeTime = 30, ServerId = 4444, UserId = 4444, DevId = 4444, Roles = "Junior Backend" };
			await p_taskService.Create(task);

			//act
			var actual = p_taskService.GetById(task.Id).Result;

			//assert
			Assert.Equal(task.Id, actual.ServerId);
		}

		[Fact]
		public async Task Given_service_return_exception_When_there_is_no_task_with_this_id()
		{
			//arrange
			ulong actualId = 54321;

			//act

			//assert
			var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => p_taskService.GetById(actualId));
		}

		[Fact]
		public async Task Given_service_is_successfully_add_developer_to_task()
		{
			//arrange
			var addDev = new AddDevToTaskRequest { TaskId = 55, DevId = 5555 };
			ulong devId = 5555;
			var actualTask = new CreateTaskRequest { Id = 55, Content = "Content55", LifeTime = 30, ServerId = 5555, UserId = 5555, DevId = null, Roles = "Junior Backend" };
			await p_taskService.Create(actualTask);
			var task = p_taskService.GetById(actualTask.Id).Result;

			//act
			var actual = p_taskService.AddDevToTask(addDev).Result;
			var updatedTask = p_taskService.GetById(actualTask.Id).Result;

			//assert
			Assert.Equal(devId, updatedTask.DevId);
			Assert.True(actual);
		}
	}
	public class TaskServiceFixture
	{
		public TaskServiceFixture()
		{
			TaskRepositoryMock = new Mock<ITaskRepository>();
			SettingServiceMock = new Mock<ISettingRepository>();
		}
		public Mock<ITaskRepository> TaskRepositoryMock { get; private set; }
		public Mock<ISettingRepository> SettingServiceMock { get; private set; }
	}
}
