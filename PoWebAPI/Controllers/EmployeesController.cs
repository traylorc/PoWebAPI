using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoWebAPI.Data;
using PoWebAPI.Models;

namespace PoWebAPI.Controllers
{
    //find base url via solution exp, right click project, go to properties, find App URL// localhost:"portNumber"
    //start with attributes of the class, convention in microsoft space is to put "api"/ 
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // set up context, readonly means the only place you can set a value inside of the constructor. outside can only read it
        private readonly PoContext _context;

        //controller is dependant on context, so it must be passed in
        public EmployeesController(PoContext context)
        {
            _context = context;
        }

        //get method is a read method
        // GET: api/Employees
        [HttpGet]
        // task is designed for Async calls, actionresult is a class that allows returns of different types of data, ienumerable is a collection of employees
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            // await says that even though this spawns a separate process, when its dont itll come back to here and go through.
            return await _context.Employee.ToListAsync();
        }

        // GET: api/Employees/5
        //adding id, you need to add into url statement
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //put is insert
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            //make sure IDs match so you are updating correct file
            if (id != employee.Id)
            {
                return BadRequest();
            }
            //takes passed in data, to read it then change it
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //post is update, it expects the body of the class to have the info needed.
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
            //createdataction, after you save, this will go get your new ID and bring it back
            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }

        //GET: api/employees/login/password
        [HttpGet("{login}/{password}")]
        public async Task<ActionResult<Employee>> Login(string login, string password)
        {
            var empl = await _context.Employee.SingleOrDefaultAsync(e => e.Login == login && e.Password == password);

            if (empl == null)
            {
                return NotFound();
            }

            return Ok(empl);
        }
    }
}
