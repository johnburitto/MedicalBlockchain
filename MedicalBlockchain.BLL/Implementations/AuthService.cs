using Microsoft.EntityFrameworkCore;

using MedicalBlockchain.DAL.Data;
using MedicalBlockchain.DAL.Dtos;
using MedicalBlockchain.DAL.Entities;
using MedicalBlockchain.BLL.Interfaces;
using MedicalBlockchain.BLL.Crypto.Interfaces;

namespace MedicalBlockchain.BLL.Implementations
{
	/// <summary>
	/// Realisation of <see cref="IAuthService"/> interface.
	/// </summary>
	public class AuthService : IAuthService
	{
		#region Private fields

		/// <summary>
		/// The context
		/// </summary>
		private AppDbContext _context;

		/// <summary>
		/// The crypto service.
		/// </summary>
		private readonly ICryptoService _cryptoService;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthService"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="service">The service.</param>
		public AuthService(AppDbContext context, ICryptoService service)
		{
			_context = context;
			_cryptoService = service;
		}

		#endregion

		#region Implentation of IAuthService

		/// <inheritdoc />
		public Task<bool> LoginAsync(AuthDto dto)
			=> _context.Doctors.AnyAsync(doctor => doctor.Username == dto.Username && doctor.Password == dto.Password);

		/// <inheritdoc />
		public async Task<Doctor> RegisterAsync(AuthDto dto)
		{
			(var publicKey, var privateKey) = _cryptoService.GenerateRsaKeys();
			var doctor = new Doctor
			{
				Username = dto.Username,
				Password = dto.Password,
				PublicKey = privateKey,
				PrivateKey = privateKey
			};

			await _context.Doctors.AddAsync(doctor);
			await _context.SaveChangesAsync();

			return doctor;
		}

		/// <inheritdoc />
		public async Task<Guid?> GetDoctorIdAsync(AuthDto dto)
		{
			var doctor = await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.Username == dto.Username && doctor.Password == dto.Password);

			return doctor?.Id;
		}

		/// <inheritdoc />
		public Task<Doctor?> GetDoctorByIdAsync(Guid id)
			=> _context.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == id);

		#endregion
	}
}
