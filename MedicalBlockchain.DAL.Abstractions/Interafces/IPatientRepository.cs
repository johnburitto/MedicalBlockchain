using MedicalBlockchain.DAL.Entities;

namespace MedicalBlockchain.DAL.Abstractions.Interafces
{
	/// <summary>
	/// Describe all methods to interact with the patient table in the database.
	/// </summary>
	public interface IPatientRepository
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
		/// <returns>Specific pacient.</returns>
		Task<Patient?> GetPatientByIdAsync(Guid id);

		/// <summary>
		/// Creates the patient asynchronous.
		/// </summary>
		/// <param name="patient">The patient.</param>
		/// <returns>Created patient.</returns>
		Task<Patient> CreatePatientAsync(Patient patient);

		/// <summary>
		/// Adds the medical record asynchronous.
		/// </summary>
		/// <param name="patientId">The patient identifier.</param>
		/// <param name="record">The record.</param>
		Task AddMedicalRecordAsync(Guid patientId, MedicalRecord record);
	}
}
