using System.Text;
using System.Security.Cryptography;

using MedicalBlockchain.BLL.Crypto.Interfaces;
using MedicalBlockchain.DAL.Entities;

namespace MedicalBlockchain.BLL.Crypto.Implementations
{
	/// <summary>
	/// Realisation of <see cref="ICryptoService"/>.
	/// </summary>
	public class CryptoService : ICryptoService
	{
		#region Constants

		private const int RSA_KEY_LENGTH = 2048;

		#endregion

		#region Realisation of ICryptoService

		/// <inheritdoc/>
		public (string, string) GenerateRsaKeys()
		{
			using var rsa = RSA.Create(RSA_KEY_LENGTH);

			return (Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo()),
				Convert.ToBase64String(rsa.ExportPkcs8PrivateKey()));
		}

		/// <inheritdoc/>
		public string Sha256Hash(string? value)
			=> Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(value ?? "")));

		/// <inheritdoc/>
		public byte[] SignData(string? value, string? key)
		{
			using var rsa = RSA.Create();

			rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(key ?? ""), out _);

			return rsa.SignData(Encoding.UTF8.GetBytes(value ?? ""), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
		}

		/// <inheritdoc/>
		public string BuildMerkelRoot(List<MedicalRecord> medicalRecords)
		{
			var hashes = medicalRecords.Select(mr => $"{mr.Diagnosis}{mr.Treatment}").ToList();

			while (medicalRecords.Count > 1)
			{
				List<string> newHashes = [];

				for (int i = 0; i < hashes.Count; i += 2)
				{
					if (i + 1 < hashes.Count)
					{
						newHashes.Add(Sha256Hash($"{hashes[i]}{hashes[i + 1]}"));
					}
					else
					{
						newHashes.Add(Sha256Hash($"{hashes[i]}{hashes[i]}"));
					}
				}

				hashes = newHashes;
			}

			return hashes[0];
		}

		#endregion
	}
}
