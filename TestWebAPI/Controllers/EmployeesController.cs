using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebAPI.Data;
using TestWebAPI.Model;

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
        public IActionResult  Get()
        {
            IEnumerable<Employee> employees = _context.Employees;
            return View(employees);
        }

        [HttpGet("id")]
        public IActionResult Get(int id) 
        {
            if(id==0)
            {
                return NotFound();
            }
            Employee employee = _context.Employees.SingleOrDefault(e => e.Id == id);
            return View(employee);
        }
    }
}
