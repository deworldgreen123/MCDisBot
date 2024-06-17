using MCDisBot.Core.Dto.Setting;
using MCDisBot.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDisBot.Core.Mapping.Setting
{
	public class SettingMapper
	{
		public static Models.Setting Map(UpdateSettingRequest _updatedSetting)
		{
			return new Models.Setting
			{
				ServerId = _updatedSetting.ServerId,
				Roles = _updatedSetting.Roles,
				ChannelClient = _updatedSetting.ChannelClient,
				ChannelDev = _updatedSetting.ChannelDev
			};
		}
	}
}
