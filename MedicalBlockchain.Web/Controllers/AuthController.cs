using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using MedicalBlockchain.DAL.Dtos;
using MedicalBlockchain.BLL.Interfaces;

namespace MedicalBlockchain.Web.Controllers
{
	/// <summary>
	/// Auth controller.
	/// </summary>
	public class AuthController : Controller
	{
		#region Private fileds

		/// <summary>
		/// Member service.
		/// </summary>
		private readonly IAuthService _authService;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthController"/> class.
		/// </summary>
		/// <param name="authService">The authentication service.</param>
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		#endregion

		#region Actions

		/// <summary>
		/// Login action.
		/// </summary>
		/// <returns>Login view.</returns>
		public IActionResult Login()
		{
			return View(new AuthDto());
		}

		/// <summary>
		/// Login action. Process.
		/// </summary>
		/// <param name="model">Login model.</param>
		/// <returns>If login successfully home page, and login view if not.</returns>
		[HttpPost]
		public async Task<IActionResult> Login(AuthDto model)
		{
			if (await _authService.LoginAsync(model))
			{
				var doctorId = await _authService.GetDoctorIdAsync(model);
				var claims = new List<Claim>()
				{
					new(ClaimTypes.NameIdentifier, doctorId?.ToString() ?? ""),
				};
				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				return Redirect("/Patient/Index");
			}

			return RedirectToAction("Login");
		}

		/// <summary>
		/// Logout action.
		/// </summary>
		/// <returns>Home page.</returns>
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();

			return Redirect("/Patient/Index");
		}

		/// <summary>
		/// Register action.
		/// </summary>
		/// <returns>Register view.</returns>
		public IActionResult Register()
		{
			return View(new AuthDto());
		}

		/// <summary>
		/// Register action. Process.
		/// </summary>
		/// <param name="dto">Register dto.</param>
		/// <returns>If register successfully keys archive, and register view if not.</returns>
		[HttpPost]
		public async Task<IActionResult> Register(AuthDto dto)
		{
			await _authService.RegisterAsync(dto);

			return RedirectToAction("Login");
		}

		#endregion
	}
}
