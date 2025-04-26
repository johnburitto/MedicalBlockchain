using MedicalBlockchain.DAL.Entities;
using MedicalBlockchain.BLL.Interfaces;
using MedicalBlockchain.BLL.Crypto.Interfaces;
using MedicalBlockchain.DAL.Abstractions.Interafces;

namespace MedicalBlockchain.BLL.Implementations
{
	/// <summary>
	/// Realization of the <see cref="IPatientService"/> interface.
	/// </summary>
	public class PatientService : IPatientService
	{
		#region Private fields

		/// <summary>
		/// The patient repository.
		/// </summary>
		private readonly IPatientRepository _patientRepository;

		/// <summary>
		/// The crypto service.
		/// </summary>
		private readonly ICryptoService _cryptoService;

		/// <summary>
		/// The block service.
		/// </summary>
		private readonly IBlockChainService _blockChainService;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="PatientService"/> class.
		/// </summary>
		/// <param name="patientRepository">The patient repository.</param>
		/// <param name="cryptoService">The crypto service.</param>
		/// <param name="blockChainService">The block chain service.</param>
		public PatientService(IPatientRepository patientRepository, ICryptoService cryptoService, IBlockChainService blockChainService)
		{
			_patientRepository = patientRepository;
			_cryptoService = cryptoService;
			_blockChainService = blockChainService;
		}

		#endregion

		#region Impelementation of IPatientService

		/// <inheritdoc />
		public Task<List<Patient>> GetAllPatientsAsync()
			=> _patientRepository.GetAllPatientsAsync();

		/// <inheritdoc />
		public Task<Patient?> GetPatientByIdAsync(Guid id)
			=> _patientRepository.GetPatientByIdAsync(id);

		/// <inheritdoc />
		public async Task<Patient> CreatePatientAsync(string name)
		{
			var newPatient = new Patient
			{
				Id = Guid.NewGuid(),
				Name = name,
				MedicalRecords = new List<MedicalRecord>(),
				Blocks = new List<Block>()
			};

			await _patientRepository.CreatePatientAsync(newPatient);

			return newPatient;
		}

		/// <inheritdoc />
		public async Task AddMedicalRecordAsync(Guid patientId, string privateKey, MedicalRecord record)
		{
			var patient = await _patientRepository.GetPatientByIdAsync(patientId);

			record.Id = Guid.NewGuid();
			record.PatientId = patientId;
			record.DoctorSignature = _cryptoService.SignData($"{record.Id}{record.PatientId}{record.Diagnosis}{record.Treatment}", privateKey);

			await _patientRepository.AddMedicalRecordAsync(patientId, record);
		}

		/// <inheritdoc />
		public async Task ProcessMedicalRecordsAsync(Guid doctorId, string privateKey)
		{
			var medicalRecords = await _blockChainService.GetNotProcesedMedicalRecordsAsync();

			if (medicalRecords.Count == 0)
			{
				return;
			}

			var latestBlock = await _blockChainService.GetLastBlockAsync();
			var medicalRecordsHash = _cryptoService.BuildMerkelRoot(medicalRecords);
			var latestBlockHash = latestBlock.Hash;
			var newBlockHash = string.Empty;
			var nonce = 0;

			while (!newBlockHash.StartsWith('0'))
			{
				newBlockHash = _cryptoService.Sha256Hash($"{medicalRecordsHash}{latestBlockHash}{nonce}");
				nonce++;
			}

			var block = await _blockChainService.CreateBlockAsync(new()
			{
				Hash = newBlockHash,
				Nonce = nonce,
				DoctorSignature = _cryptoService.SignData(newBlockHash, privateKey),
				Timestamp = DateTime.UtcNow,
				PreviousHash = latestBlock.Hash,
				DoctorId = doctorId
			});

			await _blockChainService.AddBlockIdAsync(medicalRecords, block.Id);
			await _blockChainService.ProcessMedicalRecordsAsync(latestBlock.MedicalRecords);
		}

		#endregion
	}
}
