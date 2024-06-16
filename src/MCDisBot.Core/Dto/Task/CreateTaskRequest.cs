namespace MCDisBot.Core.Dto.Task;

public record CreateTaskRequest
{
  public required ulong Id { get; init; }
  public required string Content { get; init; }
  public required int LifeTime { get; init; }
  public required ulong ServerId { get; init; }
  public required ulong UserId { get; init; }
  public required ulong? DevId { get; init; }
  public required string Roles { get; init; }
}