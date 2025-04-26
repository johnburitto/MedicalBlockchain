using MedicalBlockchain.DAL.Entities;

namespace MedicalBlockchain.BLL.Crypto.Interfaces
{
	/// <summary>
	/// Describe all methods to process crypto operations
	/// </summary>
	public interface ICryptoService
	{
		/// <summary>
		/// Generate public and private RSA keys for identification of user.
		/// </summary>
		/// <returns>String representations of public and private RSA keys.</returns>
		(string, string) GenerateRsaKeys();

		/// <summary>
		/// Hash data using SHA256 hash function.
		/// </summary>
		/// <param name="value">Data to hash.</param>
		/// <returns>String representations of hash.</returns>
		string Sha256Hash(string? value);

		/// <summary>
		/// Sing data using member private RSA key.
		/// </summary>
		/// <param name="value">Data to sign.</param>
		/// <param name="key">Member private RSA key.</param>
		/// <returns>Array of signed bytes.</returns>
		byte[] SignData(string? value, string? key);

		/// <summary>
		/// Bilds Merkel root.
		/// </summary>
		/// <param name="medicalRecords">Medical records to build from.</param>
		/// <returns>String representation of hash</returns>
		string? BuildMerkelRoot(List<MedicalRecord> medicalRecords);
	}
}
