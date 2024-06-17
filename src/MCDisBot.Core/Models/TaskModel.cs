﻿using MCDisBot.Core.Enums;

namespace MCDisBot.Core.Models;

public class TaskModel
{
  public required ulong Id { get; init; }
  public required string Content { get; init; }
  public required int LifeTime { get; init; }
  public required StatusTask Status { get; init; }
  public required ulong ServerId { get; init; }
  public required ulong UserId { get; init; }
  public required ulong? DevId { get; init; }
  public required string Roles { get; init; }
}