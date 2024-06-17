using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.IRepositories;
using MCDisBot.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApiBot.Controllers;

[ApiController]
[Route("api/executor")]
public class ExecutorController(ITaskService service) : ControllerBase
{
  private readonly ITaskService p_service = service;
  
  [HttpPut("task")]
  public async Task<ActionResult> AddDevToTask([FromBody] AddDevToTaskRequest addDevToTaskRequest)
  {
    var result = await p_service.AddDevToTask(addDevToTaskRequest);
    if (!result)
      return BadRequest();
    
    return Ok();
  }
}