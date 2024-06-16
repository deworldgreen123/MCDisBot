using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Mapping.Task;
using MCDisBot.Core.Models;
using MCDisBot.Infrastructure;
using Moq;
using System.Data.Common;

namespace test.TaskRepository
{
	public class TaskRepositoryGetAllTests : IClassFixture<TaskRepositoryFixtureGetAll>
	{
		private readonly TaskRepositoryFixtureGetAll p_fixture;
		private readonly ITaskRepository p_taskRepository;

		public TaskRepositoryGetAllTests(TaskRepositoryFixtureGetAll fixture)
		{
			p_fixture = fixture;
			p_taskRepository = p_fixture.TaskRepository;
		}

		[Fact]
		public async Task Given_repository_is_successfully_get_all_tasks()
		{
			//Arrange
			p_fixture.Dispose();
			var expectedRequests = new List<CreateTaskRequest>
			{
				new CreateTaskRequest { Id = 1, Content = "Content1", LifeTime = 30, ServerId = 111, UserId = 221, DevId = 331, Roles = "Junior Backend" },
				new CreateTaskRequest { Id = 2, Content = "Content2", LifeTime = 30, ServerId = 112, UserId = 222, DevId = 332, Roles = "Middle Backend" },
				new CreateTaskRequest { Id = 3, Content = "Content3", LifeTime = 30, ServerId = 113, UserId = 223, DevId = 333, Roles = "Junior Frontend" }
			};

			var excpectedList = new List<TaskModel>
			{
				TaskModelMapper.Map(expectedRequests[0]),
				TaskModelMapper.Map(expectedRequests[1]),
				TaskModelMapper.Map(expectedRequests[2])
			};

			foreach (var expected in excpectedList)
			{
				await p_taskRepository.Add(expected);
			}

			//Act
			var actual = await p_taskRepository.GetAll();

			//Assert
			Assert.NotNull(actual);
			Assert.Equal(excpectedList.Count, actual.Count);
			Assert.Equal(excpectedList, actual);
		}

		[Fact]
		public async Task Given_repository_return_empty_When_tasks_is_empty()
		{
			//Arrange
			p_fixture.Dispose();
			var expectedTasks = new List<TaskModel>();

			//Act
			var result = await p_taskRepository.GetAll();

			//Assert
			Assert.Empty(result);
		}

		[Fact]
		public async Task Given_repository_return_exception_When_connection_is_dead()
		{
			//Arrange
			p_fixture.Dispose();

			//Act

			//Assert
			var exception = await Assert.ThrowsAsync<DbException>(() => p_taskRepository.GetAll());
		}
	}

	public class TaskRepositoryFixtureGetAll
	{
		private readonly TaskBotContext p_taskBotContext;
		public TaskRepositoryFixtureGetAll(TaskBotContext _taskBotContext)
		{
			p_taskBotContext = _taskBotContext;
			TaskRepository = new(p_taskBotContext);
		}
		public MCDisBot.Infrastructure.Repositories.TaskRepository TaskRepository { get; private set; }
		public void Dispose()
		{
			p_taskBotContext.Dispose();
			TaskRepository = new MCDisBot.Infrastructure.Repositories.TaskRepository(p_taskBotContext);
		}
	}
}
