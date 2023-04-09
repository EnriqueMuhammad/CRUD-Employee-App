using EmployeeCRUD.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUD.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly DataContext _context;

		public EmployeeController(DataContext context)
        {
			_context = context;
		}

        [HttpGet]
		public async Task<ActionResult<List<Employee>>> GetEmployees()
		{
			var emp = await _context.Employees.ToListAsync();
			return Ok(emp);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Employee>> GetSingleEmployee(int id)
		{
			var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
			if(employee == null) 
			{
				return NotFound("No Employee found with that ID");
			}
			else
			{
				return Ok(employee);
			}
		}

		[HttpPost]
		public async Task<ActionResult<List<Employee>>> CreateEmployee(Employee emplo)
		{
			_context.Employees.Add(emplo);
			await _context.SaveChangesAsync();
			return Ok(await GetDbEmployees());
			
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee emplo, int id)
		{
			var dbEmplo = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
			if (dbEmplo == null)
			{
				return NotFound("Employee not found");
			}

			dbEmplo.Nama = emplo.Nama;
			dbEmplo.Alamat = emplo.Alamat;
			dbEmplo.Gaji = emplo.Gaji;

			await _context.SaveChangesAsync();
			return Ok(await GetDbEmployees());
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
		{
			var dbEmplo = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
			if (dbEmplo == null)
			{
				return NotFound("Employee not found");
			}

			_context.Employees.Remove(dbEmplo);
			await _context.SaveChangesAsync();

			return Ok(await GetDbEmployees());
		}

		[HttpGet("search")]
		public async Task<ActionResult<List<Employee>>> SearchEmployee(string searchTerm)
		{
			var employees = await _context.Employees.Where(e => e.Nama.Contains(searchTerm)).ToListAsync();
			return Ok(employees);
		}

		private async Task<List<Employee>> GetDbEmployees()
		{
			return await _context.Employees.ToListAsync();
		}

	}
}
