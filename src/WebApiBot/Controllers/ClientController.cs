using MCDisBot.Core.Dto.Setting;
using MCDisBot.Core.Dto.Task;
using MCDisBot.Core.Enums;
using MCDisBot.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApiBot.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController(ITaskService taskService, ISettingService settingService) : ControllerBase
{
  private readonly ITaskService p_taskService = taskService;
  private readonly ISettingService p_settingService = settingService;
  
  [HttpPost("task")]
  public async Task<ActionResult> CreateTask([FromBody]CreateTaskRequest task)
  {
    var result = await p_taskService.Create(task);
    if (!result)
      return BadRequest();
    
    return Ok();
  }
  
  [HttpDelete("task/{taskId:long}")]
  public async Task<ActionResult> CancelTask(ulong taskId)
  {
    var result = await p_taskService.Delete(taskId);
    if (!result)
      return BadRequest();
    
    return Ok();
  }
  
  [HttpGet("task/{taskId:long}/status")]
  public async Task<ActionResult<StatusTask>> GetStatusTask(ulong taskId)
  {
    var result = await p_taskService.GetStatus(taskId);
    if (result is null)
      return BadRequest();
    
    return result;
  }
  
  [HttpGet("task/{taskId:long}")]
  public async Task<ActionResult<GetTaskResponse>> GetTask(ulong taskId)
  {
    var result = await p_taskService.GetById(taskId);
    if (result is null)
      return BadRequest();
    
    return result;
  }
  
  [HttpPost("setting")]
  public async Task<ActionResult> AddSetting([FromBody]AddSettingRequest setting)
  {
    var result = await p_settingService.Add(setting);
    if (!result)
      return BadRequest();
    
    return Ok();
  }
  
  [HttpPut("setting")]
  public async Task<ActionResult> UpdateSetting([FromBody] UpdateSettingRequest setting)
  {
    var result = await p_settingService.Update(setting);
    if (!result)
      return BadRequest();
    
    return Ok();
  }
  
  [HttpDelete("setting/{settingId:long}")]
  public async Task<ActionResult> DeleteSetting(ulong settingId)
  {
    var result = await p_settingService.Delete(settingId);
    if (!result)
      return BadRequest();
    
    return Ok();
  }
  
  [HttpGet("setting/{settingId:long}")]
  public async Task<ActionResult<GetSettingResponse>> GetSetting(ulong settingId)
  {
    var result = await p_settingService.GetById(settingId);
    if (result is null)
      return BadRequest();
    
    return result;
  }
}