using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services.Interfaces;
using Moq;

namespace test.TaskService
{
	public class TaskServiceTests : IClassFixture<TaskServiceFixture>
	{
		private readonly ITaskRepository p_taskRepository;
		private readonly ITaskService p_taskService;
		private readonly ISettingRepository p_settingRepository;
		private readonly TaskServiceFixture p_fixture;
		public TaskServiceTests(TaskServiceFixture _taskServiceFixture)
		{
			p_fixture = _taskServiceFixture;
			p_taskRepository = p_fixture.TaskRepositoryMock.Object;
			p_settingRepository = p_fixture.SettingRepositoryMock.Object;
			p_taskService = new MCDisBot.Core.Services.TaskService(p_taskRepository, p_settingRepository);
		}

		[Fact]
		public async Task Given_service_is_successfully_create_task()
		{
			//arrange
			var task = new TaskModel { Id = 11, Content = "Content11", Status = TaskStatus.WaitingForActivation, ServerId = 1111, UserId = 2211, DevId = 3311, Roles = "Junior Backend" };

			//act
			await p_taskService.Create(task);
			var actual = p_taskService.GetById(task.Id).Result;

			//assert
			Assert.Equal(task, actual);
		}

		[Fact]
		public async Task Given_service_is_successfully_remove_task()
		{
			//arrange
			var task = new TaskModel { Id = 22, Content = "Content22", Status = TaskStatus.WaitingForActivation, ServerId = 2222, UserId = 2222, DevId = 3322, Roles = "Junior Backend" };
			await p_taskService.Create(task);

			//act
			await p_taskService.Delete(task.Id);

			//assert
			var exception = await Assert.ThrowsAsync<Exception>(() => p_taskService.GetById(task.Id));
			Assert.Equal("No such task", exception.Message);
		}

		[Fact]
		public async Task Given_service_is_successfully_return_status()
		{
			//arrange
			var task = new TaskModel { Id = 33, Content = "Content33", Status = TaskStatus.Running, ServerId = 3333, UserId = 3333, DevId = 3333, Roles = "Junior Backend" };
			await p_taskService.Create(task);

			//act
			var actual = p_taskService.GetStatus(task.Id).Result;

			//assert
			Assert.Equal(TaskStatus.Running, actual);
		}

		[Fact]
		public async Task Given_service_return_exception_When_there_is_no_task()
		{
			//arrange
			ulong expectedId = 9999;

			//act

			//assert
			var exception = await Assert.ThrowsAsync<Exception>(() => p_taskService.GetStatus(expectedId));
			Assert.Equal("No such task", exception.Message);
		}

		[Fact]
		public async Task Given_service_is_successfully_return_task_by_id()
		{
			//arrange
			var task = new TaskModel { Id = 44, Content = "Content44", Status = TaskStatus.Running, ServerId = 4444, UserId = 4444, DevId = 4444, Roles = "Junior Backend" };
			await p_taskService.Create(task);

			//act
			var actual = p_taskService.GetById(task.Id).Result;

			//assert
			Assert.Equal(task, actual);
		}

		[Fact]
		public async Task Given_service_return_exception_When_there_is_no_task_with_this_id()
		{
			//arrange
			ulong actualId = 54321;
			string excpectedExceptionMessage = "No such task";

			//act

			//assert
			var exception = await Assert.ThrowsAsync<Exception>(() => p_taskService.GetById(actualId));
			Assert.Equal(excpectedExceptionMessage, exception.Message);
		}

		[Fact]
		public async Task Given_service_is_successfully_add_developer_to_task()
		{
			//arrange
			ulong devId = 5555;
			var expectedTask = new TaskModel { Id = 55, Content = "Content55", Status = TaskStatus.RanToCompletion, ServerId = 5555, UserId = 5555, DevId = 5555, Roles = "Junior Backend" };
			var actualTask = new TaskModel { Id = 55, Content = "Content55", Status = TaskStatus.RanToCompletion, ServerId = 5555, UserId = 5555, DevId = null, Roles = "Junior Backend" };

			//act
			await p_taskService.AddDevToTask(actualTask, devId);
			var updatedTask = p_taskService.GetById(actualTask.Id).Result;

			//assert
			Assert.Equal(devId, updatedTask.DevId);
		}
	}
	public class TaskServiceFixture
	{
		public TaskServiceFixture()
		{
			TaskRepositoryMock = new Mock<ITaskRepository>();
			SettingRepositoryMock = new Mock<ISettingRepository>();
		}
		public Mock<ITaskRepository> TaskRepositoryMock { get; private set; }
		public Mock<ISettingRepository> SettingRepositoryMock { get; private set; }
	}
}
