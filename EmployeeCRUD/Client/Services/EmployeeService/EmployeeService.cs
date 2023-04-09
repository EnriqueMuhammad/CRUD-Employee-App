using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace EmployeeCRUD.Client.Services.EmployeeService
{
	public class EmployeeService : IEmployeeService
	{
		private readonly HttpClient _http;
		private readonly NavigationManager _navigationManager;

		public EmployeeService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
			_navigationManager = navigationManager;
        }
        public List<Employee> Employees { get; set ; } = new List<Employee>();

		public async Task<Employee> GetEmployeeById(int id)
		{
			var result = await _http.GetFromJsonAsync<Employee>($"api/employee/{id}");
			if (result != null) 
			{
				return result;
			}
			throw new Exception("Employee not found");
		}

		public async Task<List<Employee>> GetEmployees()
		{
			var result = await _http.GetFromJsonAsync<List<Employee>>("/api/employee");
			if(result != null)
			{
				Employees = result;
			}
			return result;
		}

		private async Task SetEmployees(HttpResponseMessage result)
		{
			var response = await result.Content.ReadFromJsonAsync<List<Employee>>();
			Employees = response;
			_navigationManager.NavigateTo("employees");
		}

		public async Task CreateEmployee(Employee emplo)
		{
			var result = await _http.PostAsJsonAsync("api/employee", emplo);
			await SetEmployees(result);
		}

		public async Task UpdateEmployee(Employee emplo)
		{
			var result = await _http.PutAsJsonAsync($"api/employee/{emplo.Id}", emplo);
			await SetEmployees(result);
		}

		public async Task DeleteEmployee(int id)
		{
			var result = await _http.DeleteAsync($"api/employee/delete/{id}");
			await SetEmployees(result);
		}

		public async Task<List<Employee>> SearchEmployees(string searchTerm)
		{
			var result = await _http.GetFromJsonAsync<List<Employee>>($"api/employee/search?searchTerm={searchTerm}");
			if (result != null)
			{
				Employees = result;
			}
			return result;

		}
	}
}
