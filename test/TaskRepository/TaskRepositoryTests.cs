using FluentAssertions;
using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using Moq;

namespace test.TaskRepository
{
	public class TaskRepositoryTests : IClassFixture<TaskRepositoryFixture>
	{
		private readonly TaskRepositoryFixture p_fixture;
		private readonly ITaskRepository p_taskRepository;

		public TaskRepositoryTests(TaskRepositoryFixture fixture)
		{
			p_fixture = fixture;
			p_taskRepository = p_fixture.TaskRepositoryMock.Object;
		}

		[Fact]
		public async Task Given_repository_is_successfully_get_all_tasks()
		{
			//Arrange
			var expectedTasks = new List<TaskModel>
			{
				new TaskModel { Id = 1, Content = "Content1", Status = TaskStatus.RanToCompletion, ServerId = 111, UserId = 221, DevId = 331, Roles = "Junior Backend" },
				new TaskModel { Id = 2, Content = "Content2", Status = TaskStatus.Canceled, ServerId = 112, UserId = 222, DevId = 332, Roles = "Middle Backend" },
				new TaskModel { Id = 3, Content = "Content3", Status = TaskStatus.Created, ServerId = 113, UserId = 223, DevId = 333, Roles = "Junior Frontend" }
				};

			p_fixture.TaskRepositoryMock.Setup(_repo => _repo.GetAll()).ReturnsAsync((IReadOnlyCollection<TaskModel>)expectedTasks);

			//Act
			var actual = await p_taskRepository.GetAll();

			//Assert
			Assert.NotNull(actual);
			Assert.Equal(expectedTasks.Count, actual.Count);
			Assert.Equal(expectedTasks, actual);
		}

		[Fact]
		public async Task Given_repository_return_empty_When_tasks_is_empty()
		{
			//Arrange
			var expectedTasks = new List<TaskModel>();

			p_fixture.TaskRepositoryMock.Setup(_repo => _repo.GetAll()).ReturnsAsync((IReadOnlyCollection<TaskModel>)expectedTasks);

			//Act
			var result = await p_taskRepository.GetAll();

			//Assert
			Assert.Empty(result);
		}

		[Fact]
		public async Task Given_repository_return_exception_When_connection_is_dead()
		{
			//Arrange
			p_fixture.TaskRepositoryMock.Setup(_repo => _repo.GetAll()).ThrowsAsync(new Exception("Connection is dead"));

			//Act and Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => p_taskRepository.GetAll());

			//Assert
			Assert.Equal("Connection is dead", exception.Message);
		}

		[Fact]
		public async Task Given_repository_is_successfully_return_task_by_id()
		{
			//arrange
			var neededTask = new TaskModel { Id = 55, Content = "Content55", Status = TaskStatus.Faulted, ServerId = 555, UserId = 555, DevId = 555, Roles = "Middle FullStack" };
			ulong actualId = 55;
			await p_taskRepository.Add(neededTask);

			//act
			var actual = await p_taskRepository.GetById(actualId);

			//assert
			Assert.Equal(neededTask, actual);
		}

		[Fact]
		public async Task Given_repository_return_exeption_When_there_is_no_task()
		{
			//arrange
			var neededTask = new TaskModel { Id = 66, Content = "Content66", Status = TaskStatus.Faulted, ServerId = 666, UserId = 666, DevId = 666, Roles = "Senior ML" };
			ulong actualId = 55555;
			await p_taskRepository.Add(neededTask);

			//act
			var exception = await Assert.ThrowsAsync<Exception>(() => p_taskRepository.GetById(actualId));

			//assert
			Assert.Equal("No such task", exception.Message);
		}

		[Fact]
		public async void Given_repository_is_successfully_added_task()
		{
			//arrange
			var newTaskModel = new TaskModel { Id = 777, Content = "Content777", Status = TaskStatus.WaitingForActivation, ServerId = 777, UserId = 777, DevId = 777, Roles = "Senior Fullstack" };

			//act
			await p_taskRepository.Add(newTaskModel);
			var actual = p_taskRepository.GetById(newTaskModel.Id).Result;

			//assert
			Assert.Equal(newTaskModel, actual);
		}

		[Fact]
		public async void Given_repository_is_successfully_updated_task()
		{
			//arrange
			ulong actualId = 8;
			var updatedTaskModel = new TaskModel { Id = 8, Content = "Content8", Status = TaskStatus.Running, ServerId = 888, UserId = 888, DevId = 23423423423, Roles = "Junior Backend" };
			var oldTask = new TaskModel { Id = 8, Content = "Content8", Status = TaskStatus.WaitingForActivation, ServerId = 888, UserId = 888, DevId = null, Roles = "Junior Backend" };
			await p_taskRepository.Add(oldTask);

			//act
			await p_taskRepository.Update(updatedTaskModel);
			var actual = p_taskRepository.GetById(actualId).Result;

			//assert
			Assert.Equal(updatedTaskModel, actual);
			Assert.NotEqual(oldTask, actual);
		}
		[Fact]
		public async void Given_repository_is_successfully_removed_task()
		{
			//arrange
			var removedTask = new TaskModel { Id = 9, Content = "Content9", Status = TaskStatus.Canceled, ServerId = 999, UserId = 999, DevId = 999, Roles = "Junior Backend" };
			await p_taskRepository.Add(removedTask);

			//act
			await p_taskRepository.Remove(removedTask.Id);
			var exception = await Assert.ThrowsAsync<Exception>(() => p_taskRepository.GetById(removedTask.Id));

			//assert
			Assert.Equal("No such task", exception.Message);
		}
	}

	public class TaskRepositoryFixture
	{
		public TaskRepositoryFixture()
		{
			TaskRepositoryMock = new Mock<ITaskRepository>();
		}
		public Mock<ITaskRepository> TaskRepositoryMock { get; private set; }
	}
}
