using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL.Models;
using BLL;

namespace TALPA
{
	public class EmployeeUtility
    {
		private readonly EmployeeManager employeeManager;

		public EmployeeUtility()
		{
			employeeManager = new EmployeeManager();
		}

		public static Employee GetEmployee(ClaimsPrincipal User)
        {
            string user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string team = User.Claims.FirstOrDefault(c => c.Type == "TALPA/groups")?.Value;
            int points = 0;

            var employee = new Employee
            {
                Name = User.Claims.FirstOrDefault(c => c.Type == "TALPA/username")?.Value,
                Email = User.Identity.Name,
                Team = team,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                Points = points,
            };

            return employee;
        }
    }
}
