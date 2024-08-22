using Axidel.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Axidel.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[CustomAuthorize]
//[Authorize]

public class BaseController : ControllerBase
{
    public long GetUserId => Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value);
}