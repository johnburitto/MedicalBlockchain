using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MedicalBlockchain.DAL.Entities;
using MedicalBlockchain.BLL.Interfaces;
using System.Security.Claims;

namespace MedicalBlockchain.Web.Controllers
{

	/// <summary>
	/// Patient controller.
	/// </summary>
	[Authorize]
	public class PatientController : Controller
	{
		#region Private fields

		/// <summary>
		/// The patient service.
		/// </summary>
		private readonly IPatientService _patientService;

		/// <summary>
		/// The authentication service.
		/// </summary>
		private readonly IAuthService _authService;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="PatientController"/> class.
		/// </summary>
		/// <param name="patientService">The patient service.</param>
		/// <param name="authService">The authentication service.</param>
		public PatientController(IPatientService patientService, IAuthService authService)
		{
			_patientService = patientService;
			_authService = authService;
		}

		#endregion

		#region Actoions

		/// <summary>
		/// Indexes this instance.
		/// </summary>
		/// <returns>Main page.</returns>
		public async Task<IActionResult> Index()
		{
			var patients = await _patientService.GetAllPatientsAsync();
			
			return View(patients);
		}

		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns>Create page.</returns>
		public IActionResult Create()
		{
			return View();
		}


		/// <summary>
		/// Creates the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>Create page if model isn't valid, or Home page if is.</returns>
		[HttpPost]
		public async Task<IActionResult> Create(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				ModelState.AddModelError("", "Ім'я пацієнта обов'язкове.");
				
				return View();
			}

			await _patientService.CreatePatientAsync(name);
			
			return RedirectToAction(nameof(Index));
		}

		/// <summary>
		/// Detailses the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Details page.</returns>
		public async Task<IActionResult> Details(Guid id)
		{
			var patient = await _patientService.GetPatientByIdAsync(id);
			
			if (patient == null)
			{
				return NotFound();
			}
			
			return View(patient);
		}

		/// <summary>
		/// Adds the medical record.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="diagnosis">The diagnosis.</param>
		/// <param name="treatment">The treatment.</param>
		/// <returns>Details page.</returns>
		[HttpPost]
		public async Task<IActionResult> AddMedicalRecord(Guid id, string diagnosis, string treatment)
		{
			if (string.IsNullOrEmpty(diagnosis) || string.IsNullOrEmpty(treatment))
			{
				ModelState.AddModelError("", "Діагноз і лікування обов'язкові.");
				
				return RedirectToAction(nameof(Details), new { id });
			}

			var doctorId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
			var doctor = await _authService.GetDoctorByIdAsync(doctorId);
			var record = new MedicalRecord
			{
				Diagnosis = diagnosis,
				Treatment = treatment
			};

			await _patientService.AddMedicalRecordAsync(id, doctor?.PrivateKey!, record);
			
			return RedirectToAction(nameof(Details), new { id });
		}

		/// <summary>
		/// Processes this instance.
		/// </summary>
		/// <returns>Main page.</returns>
		public async Task<IActionResult> Process()
		{
			var doctorId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
			var doctor = await _authService.GetDoctorByIdAsync(doctorId);

			await _patientService.ProcessMedicalRecordsAsync(doctorId, doctor?.PrivateKey!);

			return RedirectToAction(nameof(Index));
		}

		#endregion
	}
}
