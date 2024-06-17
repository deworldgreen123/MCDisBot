namespace MCDisBot.Core.Dto.Setting;

public record GetSettingResponse
{
  public required ulong ServerId { get; init; }
  public required string Roles { get; init; }
  public required ulong ChannelClient { get; init; }
  public required ulong ChannelDev { get; init; }
}