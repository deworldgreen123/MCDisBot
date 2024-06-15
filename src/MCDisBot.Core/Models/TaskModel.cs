namespace MCDisBot.Core.Models;

public class TaskModel
{
  public required ulong Id { get; init; }
  public required string Content { get; init; }
  public TaskStatus Status { get; set; }
  public required ulong ServerId { get; init; }
  public required ulong UserId { get; init; }
  public ulong? DevId { get; set; }
  public required string Roles { get; init; }
}