using MedicalBlockchain.DAL.Entities;

namespace MedicalBlockchain.BLL.Interfaces
{
	/// <summary>
	/// Describe methods for working with blockchain.
	/// </summary>
	public interface IBlockChainService
	{
		/// <summary>
		/// Gets the last block.
		/// </summary>
		/// <returns></returns>
		Task<Block> GetLastBlockAsync();

		/// <summary>
		/// Creates the block asynchronous.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <returns>Created block.</returns>
		Task<Block> CreateBlockAsync(Block block);

		/// <summary>
		/// Gets the not procesed medical records.
		/// </summary>
		/// <returns></returns>
		Task<List<MedicalRecord>> GetNotProcesedMedicalRecordsAsync();

		/// <summary>
		/// Adds the block identifier asynchronous.
		/// </summary>
		/// <param name="medicalRecords">The medical records.</param>
		/// <param name="blockId">The block identifier.</param>
		Task AddBlockIdAsync(List<MedicalRecord>? medicalRecords, Guid blockId);

		/// <summary>
		/// Processes the medical records asynchronous.
		/// </summary>
		/// <param name="medicalRecords">The medical records.</param>
		Task ProcessMedicalRecordsAsync(List<MedicalRecord>? medicalRecords);
	}
}
