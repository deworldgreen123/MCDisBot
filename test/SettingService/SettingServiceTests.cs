using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services;
using MCDisBot.Core.Services.Interfaces;
using Moq;

namespace test.SettingService
{
	public class SettingServiceTests : IClassFixture<SettingServiceFixture>
	{
		private readonly ISettingService p_settingService;
		private readonly ISettingRepository p_settingRepository;
		private readonly SettingServiceFixture p_fixture;
		public SettingServiceTests(SettingServiceFixture _settingServiceFixture)
		{
			p_fixture = _settingServiceFixture;
			p_settingRepository = p_fixture.SettingRepositoryMock.Object;
			p_settingService = new MCDisBot.Core.Services.SettingService(p_settingRepository);
		}

		[Fact]
		public void Given_service_is_successfully_add_setting()
		{
			//arrange
			var setting = new Setting { ServerId = 21, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 221, ChannelDev = 211 };

			//act
			p_settingService.Add(setting);
			var actual = p_settingService.GetById(setting.ServerId).Result;

			//assert
			Assert.Equal(setting, actual);
		}

		[Fact]
		public void Given_service_return_false_When_setting_invalid()
		{
			//arrange
			var setting = new Setting { ServerId = 0, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 221, ChannelDev = 211 };

			//act
			var actualResult = p_settingService.Add(setting).Result;
			var actualSetting = p_settingService.GetById(setting.ServerId).Result;

			//assert
			Assert.False(actualResult);
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => p_settingService.GetById(setting.ServerId));
		}

		[Fact]
		public void Given_service_is_successfully_remove_setting()
		{
			//arrange
			var setting = new Setting { ServerId = 22, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 224, ChannelDev = 214 };
			p_settingService.Add(setting);

			//act
			var actual = p_settingService.Delete(setting.ServerId).Result;

			//assert
			Assert.True(actual);
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => p_settingService.GetById(setting.ServerId));
		}

		[Fact]
		public void Given_service_return_false_When_not_founded_setting()
		{
			//arrange
			var setting = new Setting { ServerId = 22, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 224, ChannelDev = 214 };
			p_settingService.Add(setting);

			//act
			var actual = p_settingService.Delete(setting.ServerId).Result;

			//assert
			Assert.False(actual);
		}

		[Fact]
		public async void Given_service_is_successfully_update_setting()
		{
			//arrange
			var oldSetting = new Setting { ServerId = 23, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 225, ChannelDev = 215 };
			var updatedSetting = new Setting { ServerId = 23, Roles = "Junior Middle Senior FullStack", ChannelClient = 225, ChannelDev = 215 };
			await p_settingService.Add(oldSetting);

			//act
			var actualResult = p_settingService.Update(updatedSetting).Result;
			var actual = p_settingService.GetById(updatedSetting.ServerId).Result;

			//assert
			Assert.True(actualResult);
			Assert.Equal(updatedSetting, actual);
			Assert.NotEqual(oldSetting, actual);
		}

		[Fact]
		public async void Given_service_return_false_When_setting_invalid_to_update()
		{
			//arrange
			var oldSetting = new Setting { ServerId = 233, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 225, ChannelDev = 215 };
			var updatedSetting = new Setting { ServerId = 233, Roles = "Junior Middle Senior FullStack", ChannelClient = 225, ChannelDev = 0 };
			await p_settingService.Add(oldSetting);

			//act
			var actualResult = p_settingService.Update(updatedSetting).Result;

			//assert
			Assert.False(actualResult);
		}

		[Fact]
		public async void Given_service_is_successfully_return_setting_by_id()
		{
			//arrange
			var setting = new Setting { ServerId = 24, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 226, ChannelDev = 216 };
			await p_settingService.Add(setting);

			//act
			var actual = p_settingService.GetById(setting.ServerId).Result;

			//assert
			Assert.Equal(setting, actual);
		}

		[Fact]
		public void Given_service_return_exception_When_there_is_no_setting_with_this_id()
		{
			//arrange
			ulong actualId = 54321;

			//act

			//assert
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => p_settingService.GetById(actualId));
		}
	}
	public class SettingServiceFixture
	{
		public SettingServiceFixture()
		{
			SettingRepositoryMock = new Mock<ISettingRepository>();
		}
		public Mock<ISettingRepository> SettingRepositoryMock { get; private set; }
	}
}
