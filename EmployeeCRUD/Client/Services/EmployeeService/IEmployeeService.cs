using EmployeeCRUD.Shared;

namespace EmployeeCRUD.Client.Services.EmployeeService
{
    public interface IEmployeeService
    {
        List<Employee> Employees { get; set; }
        Task<List<Employee>> GetEmployees();
        Task<List<Employee>> SearchEmployees(string searchTerm);
        Task<Employee> GetEmployeeById(int id);
		Task CreateEmployee(Employee emplo);
		Task UpdateEmployee(Employee emplo);
		Task DeleteEmployee(int id);

	}
}
