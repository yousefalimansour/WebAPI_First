using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_First.DTO;
using WebAPI_First.Models;
using WebAPI_First.Models.Data;

namespace WebAPI_First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        AppDBContext Context;
        public EmployeeController(AppDBContext Context)
        {
            this.Context = Context;
        }
        [HttpGet]
        public ActionResult<GeneralResponse> Get(int id)
        {
            Empolyee Emp =
                Context.empolyees.FirstOrDefault(emp => emp.Id == id);
            GeneralResponse generalResponse = new GeneralResponse();
            if (Emp != null)
            {
                //Success
                generalResponse.IsSuccess = true;
                generalResponse.Data = Emp;
            }
            else
            {
                // Not Success
                generalResponse.IsSuccess = false;
                generalResponse.Data = "ID INValid";
            }
            return generalResponse;
        }
    }
}
