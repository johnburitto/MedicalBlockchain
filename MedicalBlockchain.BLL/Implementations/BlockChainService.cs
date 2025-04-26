using Microsoft.EntityFrameworkCore;

using MedicalBlockchain.DAL.Data;
using MedicalBlockchain.DAL.Entities;
using MedicalBlockchain.BLL.Interfaces;

namespace MedicalBlockchain.BLL.Implementations
{
	/// <summary>
	/// Realization of the <see cref="IBlockChainService"/> interface.
	/// </summary>
	public class BlockChainService : IBlockChainService
	{
		#region Private fields

		/// <summary>
		/// The context.
		/// </summary>
		private readonly AppDbContext _context;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="BlockChainService"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public BlockChainService(AppDbContext context)
		{
			_context = context;
		}

		#endregion

		#region Impelementation of IBlockChainService

		/// <inheritdoc />
		public async Task<Block> GetLastBlockAsync()
		{
			var block = await _context.Blocks.OrderByDescending(b => b.Timestamp)
				.Include(b => b.MedicalRecords)
				.FirstOrDefaultAsync();

			return block ?? new()
			{
				Hash = "0",
				Nonce = 0,
				MedicalRecords = []
			};
		}

		/// <inheritdoc />
		public async Task<Block> CreateBlockAsync(Block block)
		{
			await _context.Blocks.AddAsync(block);
			await _context.SaveChangesAsync();

			return block;
		}

		/// <inheritdoc />
		public Task<List<MedicalRecord>> GetNotProcesedMedicalRecordsAsync()
			=> _context.MedicalRecords
				.Where(m => m.BlockId == null)
				.ToListAsync();

		public async Task AddBlockIdAsync(List<MedicalRecord>? medicalRecords, Guid blockId)
		{
			medicalRecords?.ForEach(mr => mr.BlockId = blockId);

			_context.MedicalRecords.UpdateRange(medicalRecords ?? throw new ArgumentNullException(nameof(medicalRecords)));
			await _context.SaveChangesAsync();
		}

		public async Task ProcessMedicalRecordsAsync(List<MedicalRecord>? medicalRecords)
		{
			medicalRecords?.ForEach(mr => mr.IsProcessed = true);

			_context.MedicalRecords.UpdateRange(medicalRecords ?? throw new ArgumentNullException(nameof(medicalRecords)));
			await _context.SaveChangesAsync();
		}

		#endregion
	}
}
