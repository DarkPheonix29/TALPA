using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL.Models;
using BLL;

namespace TALPA.Controllers
{
	public class AccountController : Controller
    {
        public async Task Login()
        {
            var returnUrl = Url.Action(nameof(Auth0LoginCallback));

			var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a login.
                // Note that the resulting absolute Uri must be added to the
                // **Allowed Callback URLs** settings for the app.
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
		}

        public ActionResult Auth0LoginCallback()
        {
            return Redirect("/");
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                System.Data.DataTable user = DAL.UserDataManager.GetUser(userId);

                if (user == null)
                {
                    DAL.UserDataManager.UserSubmit(userId);
                }
            }
            return Redirect("/");
        }

			[Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be added to the
                // **Allowed Logout URLs** settings for the app.
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task SignUp(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithParameter("screen_hint", "signup")
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        public async Task<IActionResult> EmailVerification()
        {
            return View();
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

		[Authorize]
		public IActionResult Dashboard()
		{
            var UserProfile = new UserProfile
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:7112/username")?.Value,
                EmailAddress = User.Identity.Name,
				ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
			};

            List<Employee> employees = new List<Employee>();
            for (int i = 1; i <= 10; i++)
            {
                employees.Add(new Employee("Employee" + i, "Employee" + i + "@talpa.com", i, i, i, i));
            }

            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                UserProfile = UserProfile,
                Employees = employees
            };

            if (UserProfile.Role == "Admin")
            {
                return View("AdminDashboard", UserProfile);
            }

            if (UserProfile.Role == "Manager")
            {
                return View("ManagerDashboard", employeeViewModel);
            }

            return View("EmployeeDashboard", UserProfile);
        }

        [Authorize]
        public IActionResult Employees()
        {
            var UserProfile = new UserProfile
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:7112/username")?.Value,
                EmailAddress = User.Identity.Name,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
            };

            if (UserProfile.Role != "Manager" && UserProfile.Role != "Admin")
            {
                return Redirect("/dashboard");
            }

            List<Employee> employees = new List<Employee>();
            for (int i = 1; i <= 10; i++)
            {
                employees.Add(new Employee("Employee" + i, "Employee" + i + "@talpa.com", i, i, i, i));
            }

            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                UserProfile = UserProfile,
                Employees = employees
            };

            return View(employeeViewModel);
        }

        public IActionResult CreatePoll()
        {
            var UserProfile = new UserProfile
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:7112/username")?.Value,
                EmailAddress = User.Identity.Name,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
            };

            if (UserProfile.Role != "Manager" && UserProfile.Role != "Admin")
            {
                return Redirect("/dashboard");
            }

            List<BLL.Models.Activity> activities = new List<BLL.Models.Activity>();
            for (int i = 1; i <= 10; i++)
            {
                List<string> categories = new List<string>();
                List<Restriction> restrictions = new List<Restriction>();
                string description = string.Concat(Enumerable.Repeat("Description", 7));
                for (int j = 1; j <= 3; j++)
                {
                    categories.Add("Cata" + i);
                    restrictions.Add(new Restriction("restriction" + i, "description", "cata"));
                }
                activities.Add(new BLL.Models.Activity(i, "Activiteit" + i, description, categories, restrictions));
            }

            ActivityViewModel activityViewModel = new ActivityViewModel
            {
                UserProfile = UserProfile,
                Activities = activities
            };

            return View(activityViewModel);
        }
    }
}
