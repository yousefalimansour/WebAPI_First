using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_First.DTO;
using WebAPI_First.Models;
using WebAPI_First.Models.Data;

namespace WebAPI_First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        AppDBContext context;
        public DepartmentController(AppDBContext context )
        {
            this.context = context;
        }

        [HttpGet] //api/Department/ verb get
        [Authorize]
        public IActionResult DisplaAllDept()
        {
            List<Department> deptList = 
                context.Departments.ToList();
            return Ok( deptList );
        }

        [HttpGet("Count")]
        public ActionResult<List<DeptWithEmpCount>> GetDeptDetails()
        {
            List<Department> deptlist =
                context.Departments.Include(d=>d.Empolyes).ToList();
            List<DeptWithEmpCount> deptlistDto = new List<DeptWithEmpCount>();
            foreach (Department dept in deptlist)
            {
                DeptWithEmpCount deptDTO = new DeptWithEmpCount();
                deptDTO.Id = dept.Id;
                deptDTO.Name = dept.Name;
                deptDTO.EmpCount = dept.Empolyes.Count();
                deptlistDto.Add(deptDTO);
            }
            return deptlistDto; //return Class ActionResult
            //return Ok( deptlist ); return interface IActionResult
        }



        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetByID(int id)
        {
            Department dept = context.Departments
                .FirstOrDefault(x => x.Id == id);
            return Ok(dept);
              
        }
        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            Department dept = context.Departments
                .FirstOrDefault(x => x.Name == name);
            return Ok(dept);

        }




        [HttpPost] //api/Department/ verb get
        public IActionResult AddDept(Department dept)
        {
            context.Departments.Add(dept);
            context.SaveChanges();
            //return Created($"http://localhost:56653/api/Department/{dept.Id}", dept);
            return CreatedAtAction("GetByID", new { id = dept.Id }, dept);

         }
        [HttpPut("{id:int}")]
        public IActionResult UpdateDept(int id,Department deptfromRequest)
        {
            Department deptfromDB = 
                context.Departments.FirstOrDefault(x => x.Id == id);
            if (deptfromDB is not null) { 
                deptfromDB.Name = deptfromRequest.Name;
                deptfromDB.ManagerName = deptfromRequest.ManagerName;
                context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound("Department Not Found");
            }
        }

    }
}
