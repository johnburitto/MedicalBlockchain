namespace MedicalBlockchain.DAL.Entities
{
	/// <summary>
	/// Hold information about a block in the blockchain.
	/// </summary>
	public class Block
	{
		#region Public properties

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the timestamp.
		/// </summary>
		public DateTime Timestamp { get; set; }

		/// <summary>
		/// Gets or sets the previous hash.
		/// </summary>
		public string? PreviousHash { get; set; }

		/// <summary>
		/// Gets or sets the hash.
		/// </summary>
		public string? Hash { get; set; }

		/// <summary>
		/// Gets or sets the nonce.
		/// </summary>
		public long Nonce { get; set; }

		/// <summary>
		/// Gets or sets the doctor signature.
		/// </summary>
		public byte[]? DoctorSignature { get; set; }

		#endregion

		#region Relations

		/// <summary>
		/// Gets or sets the doctor identifier.
		/// </summary>
		public Guid DoctorId { get; set; }

		/// <summary>
		/// Gets or sets the doctor.
		/// </summary>
		public Doctor? Doctor { get; set; }

		/// <summary>
		/// Gets or sets the medical records.
		/// </summary>
		public List<MedicalRecord>? MedicalRecords { get; set; }

		#endregion
	}
}
