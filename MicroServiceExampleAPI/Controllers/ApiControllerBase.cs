using Microsoft.AspNetCore.Mvc;

namespace MicroServiceExampleAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}
