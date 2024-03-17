using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebAPI.Data;
using TestWebAPI.Model;
using TestWebAPI.Model.DTO;

namespace TestWebAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region Dependancy and Cunstructor
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAll  Method
        [HttpGet]
        [ProducesResponseType(typeof(EmployeeDto), 200)]
        public IActionResult GetAll()
        {
            //IEnumerable<Employee> employees = _context.Employees.ToList();
            IEnumerable<EmployeeDto> employeeDto = _context.Employees.Select(e => new EmployeeDto { Id = e.Id, Name = e.Name, UAN = e.UAN, Salary = e.Salary });
            return Ok(employeeDto);
        }

        #endregion

        #region GetBy Id Method
        [HttpGet("{id:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Employee? employee = _context.Employees.Find(id);
            if (employee == null) return NotFound();
            EmployeeDto employeeDto = new() { Id = employee.Id, Name = employee.Name, UAN = employee.UAN, Salary = employee.Salary };
            return Ok(employeeDto);
        }

        #endregion

        #region Post Method

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null) return BadRequest();

            if(_context.Employees.SingleOrDefault(e => e.Id==employeeDto.Id) != null)
            {
                ModelState.AddModelError("","Id Alrady exist");
            }


            Employee employee = new()
            {
                Name = employeeDto.Name,
                UAN = employeeDto.UAN,
                Salary = employeeDto.Salary,
                CareatedDate = DateTime.UtcNow
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
            employeeDto.Id = employee.Id;

            return CreatedAtRoute("GetById", new { id = employee.Id }, employee);


        }

        #endregion

        #region Put Method
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update(int? id, [FromBody] EmployeeDto employeeDto)
        {
            if (id == null || id == 0 || id != employeeDto.Id)
            {
                return BadRequest();
            }

            Employee? employeeFormDb = _context.Employees.Find(id);

            if (employeeFormDb == null) return BadRequest();


            employeeFormDb.Name = employeeDto.Name;
            employeeFormDb.Salary = employeeDto.Salary;
            employeeFormDb.UAN = employeeDto.UAN;

            _context.Update(employeeFormDb);
            _context.SaveChanges();

            return NoContent();


        }
        #endregion

        #region Delete Method
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();

            Employee? employeeFromDb = _context.Employees.Find(id);
            if (employeeFromDb == null) return NotFound();
            _context.Employees.Remove(employeeFromDb);
            _context.SaveChanges();
            return NoContent();

        }
        #endregion

        #region Patch Method
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult PactchPartial(int id,[FromBody] JsonPatchDocument<EmployeeDto> jsonPatch)
        {
            if(id == 0 || jsonPatch == null ) return BadRequest();

            Employee? employeeFromDb = _context.Employees.AsNoTracking().FirstOrDefault(e => e.Id==id);
            if (employeeFromDb == null) return NotFound();

            EmployeeDto employeeDto = new() { 
                Id = employeeFromDb.Id,
                Name = employeeFromDb.Name,
                Salary = employeeFromDb.Salary,
                UAN = employeeFromDb.UAN
            };   

            jsonPatch.ApplyTo(employeeDto);

            Employee employee = new()
            {
                Id = employeeFromDb.Id,
                Name = employeeFromDb.Name,
                UAN = employeeFromDb.UAN,
                Salary = employeeFromDb.Salary
            };
            _context.Employees.Update(employee);

            return NoContent();

        }
        #endregion




    }
}
