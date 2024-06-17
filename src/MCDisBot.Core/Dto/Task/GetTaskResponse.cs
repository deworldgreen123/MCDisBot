using MCDisBot.Core.Enums;

namespace MCDisBot.Core.Dto.Task;

public record GetTaskResponse()
{
  public required string Content { get; init; }
  public required int LifeTime { get; init; }
  public required StatusTask Status { get; init; }
  public required ulong ServerId { get; init; }
  public required ulong UserId { get; init; }
  public required ulong? DevId { get; init; }
  public required string Roles { get; init; }
}