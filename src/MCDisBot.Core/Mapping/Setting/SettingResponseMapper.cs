using MCDisBot.Core.Dto.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDisBot.Core.Mapping.Setting
{
	public class SettingResponseMapper
	{
		public static GetSettingResponse Map(Models.Setting _setting)
		{
			return new GetSettingResponse
			{
				ServerId = _setting.ServerId,
				Roles = _setting.Roles,
				ChannelClient = _setting.ChannelClient,
				ChannelDev = _setting.ChannelDev
			};
		}
	}
}
