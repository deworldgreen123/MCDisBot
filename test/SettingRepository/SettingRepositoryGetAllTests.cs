using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.SettingRepository
{
	public class SettingRepositoryGetAllTests : IClassFixture<SettingRepositoryFixtureGetAll>
	{
		private readonly SettingRepositoryFixtureGetAll p_fixture;
		private readonly MCDisBot.Infrastructure.Repositories.SettingRepository p_settingRepository;
		public SettingRepositoryGetAllTests(SettingRepositoryFixtureGetAll _fixture)
		{
			p_fixture = _fixture;
			p_settingRepository = p_fixture.SettingRepository;
		}

		[Fact]
		public async Task Given_service_is_successfully_get_all_settings()
		{
			//arrange
			p_fixture.Dispose();
			var expectedSettings = new List<Setting>
			{
				new Setting { ServerId = 1, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 22, ChannelDev = 11 },
				new Setting { ServerId = 2, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 33, ChannelDev = 22 },
				new Setting { ServerId = 3, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 44, ChannelDev = 33 }
			};

			foreach (var setting in expectedSettings)
			{
				await p_settingRepository.Add(setting);
			}

			//act
			var actual = await p_settingRepository.GetAll();

			//assert
			Assert.NotNull(actual);
			Assert.Equal(expectedSettings.Count, actual.Count);
			Assert.Equal(expectedSettings, actual);
		}

		[Fact]
		public async Task Given_repository_return_empty_When_settings_is_empty()
		{
			//Arrange
			p_fixture.Dispose();
			var expectedSettings = new List<Setting>();

			//Act
			var actual = await p_settingRepository.GetAll();

			//Assert
			Assert.Empty(actual);
		}

		[Fact]
		public async Task Given_repository_return_exception_When_connection_is_dead()
		{
			//Arrange + надо разорвать подключение к БД
			p_fixture.Dispose();

			//Act

			//Assert
			var exception = await Assert.ThrowsAsync<DbException>(() => p_settingRepository.GetAll());
		}
	}
	public class SettingRepositoryFixtureGetAll
	{
		private readonly TaskBotContext p_taskBotContext;
		public SettingRepositoryFixtureGetAll(TaskBotContext _taskBotContext)
		{
			p_taskBotContext = _taskBotContext;
			SettingRepository = new(p_taskBotContext);
		}
		public MCDisBot.Infrastructure.Repositories.SettingRepository SettingRepository { get; private set; }
		public void Dispose()
		{
			SettingRepository = new MCDisBot.Infrastructure.Repositories.SettingRepository(p_taskBotContext);
		}
	}
}
