using Microsoft.EntityFrameworkCore;

using MedicalBlockchain.DAL.Data;
using MedicalBlockchain.DAL.Entities;
using MedicalBlockchain.DAL.Abstractions.Interafces;

namespace MedicalBlockchain.DAL.Abstractions.Implementations
{
	/// <summary>
	/// Realization of the <see cref="IPatientRepository"/> interface.
	/// </summary>
	public class PatientRepository : IPatientRepository
	{
		#region Private fields

		/// <summary>
		/// The context.
		/// </summary>
		private readonly AppDbContext _context;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="PatientRepository"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public PatientRepository(AppDbContext context)
		{
			_context = context;
		}

		#endregion

		#region Impelementation of IPatientRepository

		/// <inheritdoc />
		public Task<List<Patient>> GetAllPatientsAsync()
			=> _context.Patients
				.Include(p => p.MedicalRecords)
				.ToListAsync();

		/// <inheritdoc />
		public Task<Patient?> GetPatientByIdAsync(Guid id)
			=> _context.Patients.Include(p => p.MedicalRecords)
				.FirstOrDefaultAsync(p => p.Id == id);

		/// <inheritdoc />
		public async Task<Patient> CreatePatientAsync(Patient patient)
		{
			await _context.Patients.AddAsync(patient);
			await _context.SaveChangesAsync();

			return patient;
		}

		/// <inheritdoc />
		public async Task AddMedicalRecordAsync(Guid patientId, MedicalRecord record)
		{
			record.PatientId = patientId;

			await _context.MedicalRecords.AddAsync(record);
			await _context.SaveChangesAsync();
		}

		#endregion
	}
}
