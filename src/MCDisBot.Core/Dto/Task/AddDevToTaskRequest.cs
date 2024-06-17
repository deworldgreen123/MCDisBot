namespace MCDisBot.Core.Dto.Task;

public record AddDevToTaskRequest
{
  public required ulong TaskId { get; init; }
  public required ulong DevId { get; init; }
}