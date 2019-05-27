using System;
using System.Linq;
using HRIS.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HRIS.WebQueryModel;

namespace HRIS.Web.Host.Controllers
{
public class PublicHolidayDetailsController : HRISControllerBase
{

private readonly IPublicHolidayDetailsAppService _publicholidaydetailsAppService;

public PublicHolidayDetailsController(IPublicHolidayDetailsAppService publicholidaydetailsAppService)
{
_publicholidaydetailsAppService = publicholidaydetailsAppService;
}
[HttpGet]
public async Task<IActionResult> Get(int id)
{
if (id<0)
{
Logger.Error("ERROR: BadRequest: Id is null or empty");
return BadRequest("Id is null or empty");
}

try
{
var result = await _publicholidaydetailsAppService.GetById(id);

return result == null ? StatusCode(StatusCodes.Status204NoContent, result) : Ok(result);
}
catch (Exception ex)
{
Logger.Error("ERROR: [PublicHolidayDetailsController] -[Get]: ExceptionMessage: " + ex.Message +
", InnerException: " + ex.InnerException +
", StackTrace: " + ex.StackTrace);

return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException);
}
}

[HttpGet]
public async Task<IActionResult> GetAll(ModelQuery queryObject)
{
try
{
var result = await _publicholidaydetailsAppService.GetAll(queryObject);
return result.TotalItems > 0 ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, result);
}
catch (Exception ex)
{
Logger.Error("ERROR: [PublicHolidayDetailsController] -[GetAll]: ExceptionMessage: " + ex.Message +
", InnerException: " + ex.InnerException +
", StackTrace: " + ex.StackTrace);

return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException);
}
}

[HttpPost]
public async Task<IActionResult> Create([FromBody] PublicHolidayDetailsDto publicholidaydetails)
{
if (publicholidaydetails == null)
{
Logger.Error("ERROR: BadRequest: PublicHolidayDetails is empty or null");
return BadRequest("PublicHolidayDetails is empty or null");
}
try
{
var result = await _publicholidaydetailsAppService.Create(publicholidaydetails);
return result != null ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, result);
}
catch (Exception ex)
{
Logger.Error("ERROR: [PublicHolidayDetailsController] -[Create]: ExceptionMessage: " + ex.Message +
", InnerException: " + ex.InnerException +
", StackTrace: " + ex.StackTrace);

return StatusCode(StatusCodes.Status500InternalServerError);
}
}

[HttpPost]
public async Task<IActionResult> Update([FromBody] PublicHolidayDetailsDto publicholidaydetails)
{
if (publicholidaydetails == null)
{
Logger.Error("ERROR: BadRequest: PublicHolidayDetails is empty or null");
return BadRequest("PublicHolidayDetails is empty or null");
}
try
{
var result = await _publicholidaydetailsAppService.Update(publicholidaydetails);
return result != null ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, result);
}
catch (Exception ex)
{
Logger.Error("ERROR: [PublicHolidayDetailsController] -[Update]: ExceptionMessage: " + ex.Message +
", InnerException: " + ex.InnerException +
", StackTrace: " + ex.StackTrace);
return StatusCode(StatusCodes.Status500InternalServerError);
}
}

[HttpDelete]
public async Task<IActionResult> DeleteById(int? id)
{
if (!id.HasValue)
{
Logger.Error("ERROR: BadRequest: Id is empty or null");
return BadRequest("Id is empty or null");
}

try
{
await _publicholidaydetailsAppService.DeleteById(id.Value);
return Ok(true);
}
catch (Exception ex)
{
Logger.Error("ERROR: [PublicHolidayDetailsController] -[Delete_ID]: ExceptionMessage: " + ex.Message +
", InnerException: " + ex.InnerException +
", StackTrace:" + ex.StackTrace);

return StatusCode(StatusCodes.Status500InternalServerError);
}
}

}
}
