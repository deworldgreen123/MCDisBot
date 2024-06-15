namespace MCDisBot.Core.Models;

public class Setting
{
  public ulong ServerId { get; set; }
  public string Roles { get; set; }
  public ulong ChannelClient { get; set; }
  public ulong ChannelDev { get; set; }
}