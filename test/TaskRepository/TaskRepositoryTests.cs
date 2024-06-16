using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Mapping.Task;
using MCDisBot.Core.Models;
using MCDisBot.Infrastructure;
using Moq;
using System.Data.Common;

namespace test.TaskRepository
{
	public class TaskRepositoryTests : IClassFixture<TaskRepositoryFixture>
	{
		private readonly TaskRepositoryFixture p_fixture;
		private readonly ITaskRepository p_taskRepository;

		public TaskRepositoryTests(TaskRepositoryFixture fixture)
		{
			p_fixture = fixture;
			p_taskRepository = p_fixture.TaskRepository;
		}

		[Fact]
		public async Task Given_repository_is_successfully_return_task_by_id()
		{
			//arrange
			var neededRequest = new CreateTaskRequest { Id = 55, Content = "Content55", LifeTime = 30, ServerId = 555, UserId = 555, DevId = 555, Roles = "Middle FullStack" };
			var neededTask = TaskModelMapper.Map(neededRequest);
			ulong actualId = 55;
			await p_taskRepository.Add(neededTask);

			//act
			var actual = await p_taskRepository.GetById(actualId);

			//assert
			Assert.Equal(neededTask, actual);
		}

		[Fact]
		public async Task Given_repository_return_exeption_When_there_is_no_task_by_this_id()
		{
			//arrange
			var neededRequest = new CreateTaskRequest { Id = 66, Content = "Content66", LifeTime = 30, ServerId = 666, UserId = 666, DevId = 666, Roles = "Senior ML" };
			var neededTask = TaskModelMapper.Map(neededRequest);
			ulong actualId = 55555;
			await p_taskRepository.Add(neededTask);

			//act
			var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => p_taskRepository.GetById(actualId));

			//assert
			Assert.Equal("No such task", exception.Message);
		}

		[Fact]
		public async void Given_repository_is_successfully_added_task()
		{
			//arrange
			var newTaskRequest = new CreateTaskRequest { Id = 777, Content = "Content777", LifeTime = 30, ServerId = 777, UserId = 777, DevId = 777, Roles = "Senior Fullstack" };
			var newNeededTask = TaskModelMapper.Map(newTaskRequest);

			//act
			await p_taskRepository.Add(newNeededTask);
			var actual = p_taskRepository.GetById(newNeededTask.Id).Result;

			//assert
			Assert.Equal(newNeededTask, actual);
		}

		[Fact]
		public async void Given_repository_return_exception_When_task_data_is_invalid_and_can_not_be_added()
		{
			//arrange
			var newTaskRequest = new CreateTaskRequest { Id = 777, Content = "Content777", LifeTime = 30, ServerId = 0, UserId = 777, DevId = 777, Roles = "Senior Fullstack" };
			var newTaskModel = TaskModelMapper.Map(newTaskRequest);

			//act

			//assert
			var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => p_taskRepository.Add(newTaskModel));
		}

		[Fact]
		public async void Given_repository_return_exception_When_task_already_added()
		{
			//arrange
			var newTaskModel = new CreateTaskRequest { Id = 777, Content = "Content777", LifeTime = 30, ServerId = 123, UserId = 777, DevId = 777, Roles = "Senior Fullstack" };
			var newTaskModelMore = new CreateTaskRequest { Id = 777, Content = "Content777", LifeTime = 30, ServerId = 123, UserId = 777, DevId = 777, Roles = "Senior Fullstack" };
			var newTaskModelMap = TaskModelMapper.Map(newTaskModel);
			var newTaskModelMoreMap = TaskModelMapper.Map(newTaskModelMore);
			await p_taskRepository.Add(newTaskModelMap);

			//act

			//assert
			var exception = await Assert.ThrowsAsync<DbException>(() => p_taskRepository.Add(newTaskModelMoreMap));
		}

		[Fact]
		public async void Given_repository_is_successfully_updated_task()
		{
			//arrange
			ulong actualId = 8;
			var updatedTaskModel = new CreateTaskRequest { Id = 8, Content = "Content8", LifeTime = 30, ServerId = 888, UserId = 888, DevId = 23423423423, Roles = "Junior Backend" };
			var oldTask = new CreateTaskRequest { Id = 8, Content = "Content8", LifeTime = 30, ServerId = 888, UserId = 888, DevId = null, Roles = "Junior Backend" };
			var updatedTaskModelMap = TaskModelMapper.Map(updatedTaskModel);
			var oldTaskMap = TaskModelMapper.Map(oldTask);
			await p_taskRepository.Add(oldTaskMap);

			//act
			await p_taskRepository.Update(updatedTaskModelMap);
			var actual = p_taskRepository.GetById(actualId).Result;

			//assert
			Assert.Equal(updatedTaskModelMap, actual);
			Assert.NotEqual(oldTaskMap, actual);
		}

		[Fact]
		public async void Given_repository_return_exception_When_task_data_is_invalid_and_can_not_be_updated()
		{
			//arrange
			var updatedTaskModel = new CreateTaskRequest { Id = 8, Content = "Content8", LifeTime = 30, ServerId = 0, UserId = 888, DevId = 23423423423, Roles = "Junior Backend" };
			var oldTask = new CreateTaskRequest { Id = 8, Content = "Content8", LifeTime = 30, ServerId = 888, UserId = 888, DevId = null, Roles = "Junior Backend" };
			var updatedTaskModelMap = TaskModelMapper.Map(updatedTaskModel);
			var oldTaskMap = TaskModelMapper.Map(oldTask);
			await p_taskRepository.Add(oldTaskMap);

			//act

			//assert
			var exception = await Assert.ThrowsAsync<ArgumentException>(() => p_taskRepository.Update(updatedTaskModelMap));
		}

		[Fact]
		public async void Given_repository_is_successfully_removed_task()
		{
			//arrange
			var removedTask = new CreateTaskRequest { Id = 9, Content = "Content9", LifeTime = 30, ServerId = 999, UserId = 999, DevId = 999, Roles = "Junior Backend" };
			var removedTaskMap = TaskModelMapper.Map(removedTask);
			await p_taskRepository.Add(removedTaskMap);

			//act
			await p_taskRepository.Remove(removedTask.Id);

			//assert
			var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => p_taskRepository.GetById(removedTask.Id));
		}

		[Fact]
		public async void Given_repository_return_exception_When_task_not_founded_to_remove()
		{
			//arrange
			ulong actualId = 43242323;
			var removedTask = new CreateTaskRequest { Id = 9, Content = "Content9", LifeTime = 30, ServerId = 999, UserId = 999, DevId = 999, Roles = "Junior Backend" };
			var removedTaskMap = TaskModelMapper.Map(removedTask);
			await p_taskRepository.Add(removedTaskMap);

			//act

			//assert
			var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => p_taskRepository.Remove(actualId));
		}
	}

	public class TaskRepositoryFixture
	{
		private readonly TaskBotContext p_taskBotContext;
		public TaskRepositoryFixture(TaskBotContext _taskBotContext)
		{
			p_taskBotContext = _taskBotContext;
			TaskRepository = new(p_taskBotContext);
		}
		public MCDisBot.Infrastructure.Repositories.TaskRepository TaskRepository { get; private set; }
	}
}
