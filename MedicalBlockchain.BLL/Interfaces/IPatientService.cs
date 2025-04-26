using MedicalBlockchain.DAL.Entities;

namespace MedicalBlockchain.BLL.Interfaces
{
	/// <summary>
	/// Describe the contract for patient-related services.
	/// </summary>
	public interface IPatientService
	{

		/// <summary>
		/// Gets all patients asynchronous.
		/// </summary>
		/// <returns>List of patients.</returns>
		Task<List<Patient>> GetAllPatientsAsync();

		/// <summary>
		/// Gets the patient by identifier asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Specific patient.</returns>
		Task<Patient?> GetPatientByIdAsync(Guid id);

		/// <summary>
		/// Creates the patient asynchronous.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>Created patient.</returns>
		Task<Patient> CreatePatientAsync(string name);


		/// <summary>
		/// Adds the medical record asynchronous.
		/// </summary>
		/// <param name="patientId">The patient identifier.</param>
		/// <param name="privateKey">The private key.</param>
		/// <param name="record">The record.</param>
		Task AddMedicalRecordAsync(Guid patientId, string privateKey, MedicalRecord record);

		/// <summary>
		/// Processes the medical records.
		/// </summary>
		/// <param name="doctorId">The doctor identifier.</param>
		/// <param name="privateKey">The private key.</param>
		Task ProcessMedicalRecordsAsync(Guid doctorId, string privateKey);
	}
}
