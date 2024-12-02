using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_First.Models;

namespace WebAPI_First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingController : ControllerBase
    {
        [HttpGet("{name:alpha}/{age:int}")]
        public IActionResult Testprimitive(int age, string? name)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult Testopj(Department department, string name)
        {
            return Ok();
        }
        [HttpGet("{Id}/{Name}/{ManagerName}")]
        public IActionResult TestCustomBind(
            [FromRoute]Department department)
        {
            return Ok();
        }
    }
}
