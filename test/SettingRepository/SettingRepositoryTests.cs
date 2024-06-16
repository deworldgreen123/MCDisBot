using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.SettingRepository
{
	public class SettingRepositoryTests : IClassFixture<SettingRepositoryFixture>
	{
		private readonly SettingRepositoryFixture p_fixture;
		private readonly MCDisBot.Infrastructure.Repositories.SettingRepository p_settingRepository;
		public SettingRepositoryTests(SettingRepositoryFixture _fixture)
		{
			p_fixture = _fixture;
			p_settingRepository = p_fixture.SettingRepository;
		}

		[Fact]
		public async Task Given_repository_is_successfully_return_setting_by_id()
		{
			//arrange
			var neededSetting = new Setting { ServerId = 444, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 123, ChannelDev = 44 };
			ulong actualId = 444;
			await p_settingRepository.Add(neededSetting);

			//act
			var actual = await p_settingRepository.GetById(actualId);

			//assert
			Assert.Equal(neededSetting, actual);
		}

		[Fact]
		public async Task Given_repository_return_exeption_When_there_is_no_setting_by_this_id()
		{
			//arrange
			var neededSetting = new Setting { ServerId = 555, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 124, ChannelDev = 55 };
			ulong actualId = 55555;
			await p_settingRepository.Add(neededSetting);

			//act

			//assert
			var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => p_settingRepository.GetById(actualId));
		}

		[Fact]
		public async void Given_repository_is_successfully_added_setting()
		{
			//arrange
			var newSetting = new Setting { ServerId = 666, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 125, ChannelDev = 66 };

			//act
			await p_settingRepository.Add(newSetting);
			var actual = p_settingRepository.GetById(newSetting.ServerId).Result;

			//assert
			Assert.Equal(newSetting, actual);
		}

		[Fact]
		public async void Given_repository_return_exception_When_setting_data_is_invalid()
		{
			//arrange
			var newSetting = new Setting { ServerId = 666, Roles = null, ChannelClient = 125, ChannelDev = 0 };

			//act

			//assert
			var exception = await Assert.ThrowsAsync<ArgumentException>(() => p_settingRepository.Add(newSetting));
		}

		[Fact]
		public async void Given_repository_is_successfully_updated_setting()
		{
			//arrange
			ulong actualId = 777;
			var updatedSetting = new Setting { ServerId = 777, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 126, ChannelDev = 77 };
			var oldSetting = new Setting { ServerId = 777, Roles = "Junior Middle Senior Backend", ChannelClient = 126, ChannelDev = 77 };
			await p_settingRepository.Add(oldSetting);

			//act
			await p_settingRepository.Update(updatedSetting);
			var actual = p_settingRepository.GetById(actualId).Result;

			//assert
			Assert.Equal(updatedSetting, actual);
			Assert.NotEqual(oldSetting, actual);
		}

		[Fact]
		public async void Given_repository_return_exception_When_updated_setting_data_is_invalid()
		{
			//arrange
			var updatedSetting = new Setting { ServerId = 777, Roles = null, ChannelClient = 0, ChannelDev = 77 };
			var oldSetting = new Setting { ServerId = 777, Roles = "Junior Middle Senior Backend", ChannelClient = 126, ChannelDev = 77 };
			await p_settingRepository.Add(oldSetting);

			//act

			//assert
			var exception = await Assert.ThrowsAsync<ArgumentException>(() => p_settingRepository.Update(updatedSetting));
		}

		[Fact]
		public async void Given_repository_return_exception_When_setting_not_founded()
		{
			//arrange
			var updatedSetting = new Setting { ServerId = 34567, Roles = null, ChannelClient = 0, ChannelDev = 77 };
			var oldSetting = new Setting { ServerId = 777, Roles = "Junior Middle Senior Backend", ChannelClient = 126, ChannelDev = 77 };
			await p_settingRepository.Add(oldSetting);

			//act

			//assert
			var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => p_settingRepository.Update(updatedSetting));
		}

		[Fact]
		public async void Given_repository_is_successfully_removed_setting()
		{
			//arrange
			var removedSetting = new Setting { ServerId = 888, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 127, ChannelDev = 88 };
			await p_settingRepository.Add(removedSetting);

			//act
			await p_settingRepository.Remove(removedSetting.ServerId);

			//assert
			var exception = await Assert.ThrowsAsync<Exception>(() => p_settingRepository.GetById(removedSetting.ServerId));
			Assert.Equal("No such setting", exception.Message);
		}

		[Fact]
		public async void Given_repository_return_exception_When_not_founded_setting_to_remove()
		{
			//arrange
			var removedSetting = new Setting { ServerId = 888, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 127, ChannelDev = 88 };
			await p_settingRepository.Add(removedSetting);

			//act

			//assert
			var exception = await Assert.ThrowsAsync<Exception>(() => p_settingRepository.Remove(removedSetting.ServerId));
			Assert.Equal("No such setting", exception.Message);
		}
	}
	public class SettingRepositoryFixture
	{
		private readonly TaskBotContext p_taskBotContext;
		public SettingRepositoryFixture(TaskBotContext _taskBotContext)
		{
			p_taskBotContext = _taskBotContext;
			SettingRepository = new(p_taskBotContext);
		}
		public MCDisBot.Infrastructure.Repositories.SettingRepository SettingRepository { get; private set; }
	}
}
