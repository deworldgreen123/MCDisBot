using MCDisBot.Core.Dto.Setting;
using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using MCDisBot.Core.Services;
using MCDisBot.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace test.SettingService
{
	public class SettingServiceTests : IClassFixture<SettingServiceFixture>
	{
		private readonly ISettingService p_settingService;
		private readonly ISettingRepository p_settingRepository;
		private readonly SettingServiceFixture p_fixture;
		private readonly ILogger<MCDisBot.Core.Services.SettingService> p_logger;
		public SettingServiceTests(SettingServiceFixture _settingServiceFixture, ILogger<MCDisBot.Core.Services.SettingService> _logger)
		{
			p_logger = _logger;
			p_fixture = _settingServiceFixture;
			p_settingRepository = p_fixture.SettingRepositoryMock.Object;
			p_settingService = new MCDisBot.Core.Services.SettingService(p_settingRepository, p_logger);
		}

		[Fact]
		public void Given_service_is_successfully_add_setting()
		{
			//arrange
			var request = new AddSettingRequest { ServerId = 21, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 221, ChannelDev = 211 };
			var expected = new GetSettingResponse { ServerId = 21, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 221, ChannelDev = 211 };

			//act
			p_settingService.Add(request);
			var actual = p_settingService.GetById(request.ServerId).Result;

			//assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Given_service_return_false_When_setting_invalid()
		{
			//arrange
			var request = new AddSettingRequest { ServerId = 0, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 221, ChannelDev = 211 };

			//act
			var actualResult = p_settingService.Add(request).Result;
			var actualSetting = p_settingService.GetById(request.ServerId).Result;

			//assert
			Assert.False(actualResult);
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => p_settingService.GetById(request.ServerId));
		}

		[Fact]
		public void Given_service_is_successfully_remove_setting()
		{
			//arrange
			var request = new AddSettingRequest { ServerId = 22, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 224, ChannelDev = 214 };
			p_settingService.Add(request);

			//act
			var actual = p_settingService.Delete(request.ServerId).Result;

			//assert
			Assert.True(actual);
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => p_settingService.GetById(request.ServerId));
		}

		[Fact]
		public void Given_service_return_false_When_not_founded_setting()
		{
			//arrange
			var request = new AddSettingRequest { ServerId = 33, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 225, ChannelDev = 215 };
			ulong actualServerId = 123456789;
			p_settingService.Add(request);

			//act
			var actual = p_settingService.Delete(actualServerId).Result;

			//assert
			Assert.False(actual);
		}

		[Fact]
		public async void Given_service_is_successfully_update_setting()
		{
			//arrange
			var oldRequest = new AddSettingRequest { ServerId = 23, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 225, ChannelDev = 215 };
			var oldResponse = new GetSettingResponse { ServerId = 23, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 225, ChannelDev = 215 };
			var expected = new GetSettingResponse { ServerId = 23, Roles = "Junior Middle Senior FullStack", ChannelClient = 225, ChannelDev = 215 };
			var updatedRequest = new UpdateSettingRequest { ServerId = 23, Roles = "Junior Middle Senior FullStack", ChannelClient = 225, ChannelDev = 215 };
			await p_settingService.Add(oldRequest);

			//act
			var actualResult = p_settingService.Update(updatedRequest).Result;
			var actual = p_settingService.GetById(updatedRequest.ServerId).Result;

			//assert
			Assert.True(actualResult);
			Assert.Equal(expected, actual);
			Assert.NotEqual(oldResponse, actual);
		}

		[Fact]
		public async void Given_service_return_false_When_setting_invalid_to_update()
		{
			//arrange
			var oldRequest = new AddSettingRequest { ServerId = 233, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 225, ChannelDev = 215 };
			var updatedRequest = new UpdateSettingRequest { ServerId = 233, Roles = "Junior Middle Senior FullStack", ChannelClient = 225, ChannelDev = 0 };
			await p_settingService.Add(oldRequest);

			//act
			var actualResult = p_settingService.Update(updatedRequest).Result;

			//assert
			Assert.False(actualResult);
		}

		[Fact]
		public async void Given_service_is_successfully_return_setting_by_id()
		{
			//arrange
			var expected = new GetSettingResponse { ServerId = 24, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 226, ChannelDev = 216 };
			var request = new AddSettingRequest { ServerId = 24, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 226, ChannelDev = 216 };
			await p_settingService.Add(request);

			//act
			var actual = p_settingService.GetById(request.ServerId).Result;

			//assert
			Assert.Equal(expected, actual);
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
