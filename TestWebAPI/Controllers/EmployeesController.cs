using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebAPI.Data;
using TestWebAPI.Model;
using TestWebAPI.Model.DTO;

namespace TestWebAPI.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Employee> employees = _context.Employees;

            var employeeDto = employees.Select(e => new EmployeeDto { Id = e.Id, Name = e.Name, Salary = e.Salary, UAN = e.UAN });

            return Ok(employeeDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        public IActionResult Get(int id) 
        {
            if(id==0)
            {
                return NotFound();
            }
            Employee employee = _context.Employees.SingleOrDefault(e => e.Id == id);
            if(employee==null)
            {
                return BadRequest();
            }
            EmployeeDto employeeDto = new() { Id=employee.Id,Name=employee.Name, UAN=employee.UAN,Salary=employee.Salary };

            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        public IActionResult Create(EmployeeDto employeeDto)
        {
            Employee employeeAdd = new() { Name = employeeDto.Name, Salary = employeeDto.Salary, UAN = employeeDto.UAN, CareatedDate = DateTime.UtcNow };

            _context.Employees.Add(employeeAdd);
            _context.SaveChanges();

            employeeDto.Id = employeeAdd.Id;
            return Ok(employeeDto);
        }
    }
}
