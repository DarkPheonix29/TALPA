using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL.Models;
using BLL;
using DAL;

namespace TALPA
{
	public class EmployeeUtility
    {
		private readonly EmployeeManager employeeManager;

		public EmployeeUtility()
		{
			employeeManager = new EmployeeManager();
		}

		public Employee GetEmployee(ClaimsPrincipal User)
        {
            string id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			var employee = new Employee
            {
                Id = id,
				Name = User.Claims.FirstOrDefault(c => c.Type == "TALPA/username")?.Value,
                Email = User.Identity.Name,
                Team = User.Claims.FirstOrDefault(c => c.Type == "TALPA/groups")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                Points = employeeManager.GetPoints(id)
            };

            return employee;
        }
    }
}
