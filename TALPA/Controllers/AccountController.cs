using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL.Models;
using BLL;
using System.Net.Mail;

namespace TALPA.Controllers
{
	public class AccountController : Controller
    {
        private readonly EmployeeManager employeeManager;

        public AccountController()
        {
            employeeManager = new EmployeeManager();
        }

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
            if (User.Identity.IsAuthenticated)
            {
                string user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                string name = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:7112/username")?.Value;
                string email = User.Identity.Name;

                employeeManager.RegisterUser(user, name, email);
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

            if (UserProfile.Role == "Admin")
            {
                return Redirect("dashboard/2");
            }

            if (UserProfile.Role == "Manager")
            {
                return Redirect("dashboard/1");
            }

            return Redirect("dashboard/0");
        }
    }
}
