using MedicalBlockchain.DAL.Dtos;
using MedicalBlockchain.DAL.Entities;

namespace MedicalBlockchain.BLL.Interfaces
{
	/// <summary>
	/// Describe auth methods.
	/// </summary>
	public interface IAuthService
	{
		/// <summary>
		/// Logins the asynchronous.
		/// </summary>
		/// <param name="dto">The dto.</param>
		/// <returns>true if doctor loged in into system, false if not.</returns>
		Task<bool> LoginAsync(AuthDto dto);

		/// <summary>
		/// Registers the asynchronous.
		/// </summary>
		/// <param name="dto">The dto.</param>
		/// <returns>Registerd doctor.</returns>
		Task<Doctor> RegisterAsync(AuthDto dto);

		/// <summary>
		/// Gets the identifier asynchronous.
		/// </summary>
		/// <param name="dto">The dto.</param>
		/// <returns>Guid of doctor.</returns>
		Task<Guid?> GetDoctorIdAsync(AuthDto dto);

		/// <summary>
		/// Gets the doctor by identifier asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Doctor</returns>
		Task<Doctor?> GetDoctorByIdAsync(Guid id);
	}
}
