using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services;
using MCDisBot.Core.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.TaskService;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
			var actual = p_settingService.GetById(setting.ServerId);

			//assert
			Assert.Equal(setting, actual);
		}

		[Fact]
		public void Given_service_is_successfully_remove_setting()
		{
			//arrange
			var setting = new Setting { ServerId = 22, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 224, ChannelDev = 214 };
			p_settingService.Add(setting);

			//act
			p_settingService.Delete(setting.ServerId);

			//assert
			var exception = Assert.Throws<Exception>(() => p_settingService.GetById(setting.ServerId));
			Assert.Equal("No such setting", exception.Message);
		}

		[Fact]
		public void Given_service_is_successfully_update_setting()
		{
			//arrange
			var oldSetting = new Setting { ServerId = 23, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 225, ChannelDev = 215 };
			var updatedSetting = new Setting { ServerId = 23, Roles = "Junior Middle Senior FullStack", ChannelClient = 225, ChannelDev = 215 };

			//act
			p_settingService.Update(updatedSetting);
			var actual = p_settingService.GetById(updatedSetting.ServerId);

			//assert
			Assert.Equal(updatedSetting, actual);
			Assert.NotEqual(oldSetting, actual);
		}

		[Fact]
		public void Given_service_is_successfully_return_setting_by_id()
		{
			//arrange
			var setting = new Setting { ServerId = 24, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 226, ChannelDev = 216 };
			p_settingService.Add(setting);

			//act
			var actual = p_settingService.GetById(setting.ServerId);

			//assert
			Assert.Equal(setting, actual);
		}

		[Fact]
		public void Given_service_return_exception_When_there_is_no_setting_with_this_id()
		{
			//arrange
			ulong actualId = 54321;
			string excpectedExceptionMessage = "No such setting";

			//act

			//assert
			var exception = Assert.Throws<Exception>(() => p_settingService.GetById(actualId));
			Assert.Equal(excpectedExceptionMessage, exception.Message);
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
