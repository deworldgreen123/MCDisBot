using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.SettingRepository
{
	public class SettingRepositoryTests: IClassFixture<SettingRepositoryFixture>
	{
		private readonly ISettingRepository p_settingRepository;
		private readonly SettingRepositoryFixture p_fixture;
    public SettingRepositoryTests(SettingRepositoryFixture _fixture)
    {
			p_fixture = _fixture;
			p_settingRepository = p_fixture.SettingRepositoryMock.Object;
    }

		[Fact]
		public async Task Given_service_is_successfully_get_all_settings()
		{
			//arrange
			var expectedSettings = new List<Setting>
			{
				new Setting { ServerId = 1, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 22, ChannelDev = 11 },
				new Setting { ServerId = 2, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 33, ChannelDev = 22 },
				new Setting { ServerId = 3, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 44, ChannelDev = 33 }
			};

			p_fixture.SettingRepositoryMock.Setup(_repo => _repo.GetAll()).ReturnsAsync((IReadOnlyCollection<Setting>)expectedSettings);

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
			var expectedSettings = new List<Setting>();

			p_fixture.SettingRepositoryMock.Setup(_repo => _repo.GetAll()).ReturnsAsync((IReadOnlyCollection<Setting>)expectedSettings);

			//Act
			var actual = await p_settingRepository.GetAll();

			//Assert
			Assert.Empty(actual);
		}

		[Fact]
		public async Task Given_repository_return_exception_When_connection_is_dead()
		{
			//Arrange
			p_fixture.SettingRepositoryMock.Setup(_repo => _repo.GetAll()).ThrowsAsync(new Exception("Connection is dead"));

			//Act and Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => p_settingRepository.GetAll());

			//Assert
			Assert.Equal("Connection is dead", exception.Message);
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
		public async Task Given_repository_return_exeption_When_there_is_no_setting()
		{
			//arrange
			var neededSetting = new Setting { ServerId = 555, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 124, ChannelDev = 55 };
			ulong actualId = 55555;
			await p_settingRepository.Add(neededSetting);

			//act
			var exception = await Assert.ThrowsAsync<Exception>(() => p_settingRepository.GetById(actualId));

			//assert
			Assert.Equal("No such setting", exception.Message);
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
		public async void Given_repository_is_successfully_removed_setting()
		{
			//arrange
			var removedSetting = new Setting { ServerId = 888, Roles = "Junior Middle Senior Backend Frontend FullStack", ChannelClient = 127, ChannelDev = 88 };
			await p_settingRepository.Add(removedSetting);

			//act
			await p_settingRepository.Remove(removedSetting.ServerId);
			var exception = await Assert.ThrowsAsync<Exception>(() => p_settingRepository.GetById(removedSetting.ServerId));

			//assert
			Assert.Equal("No such setting", exception.Message);
		}
	}
	public class SettingRepositoryFixture
	{
		public SettingRepositoryFixture()
		{
			SettingRepositoryMock = new Mock<ISettingRepository>();
		}
		public Mock<ISettingRepository> SettingRepositoryMock { get; private set; }
	}
}
