namespace MedicalBlockchain.DAL.Entities
{
	/// <summary>
	/// Hold information about a medical record.
	/// </summary>
	public class MedicalRecord
	{
		#region Public properties

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the diagnosis.
		/// </summary>
		public string? Diagnosis { get; set; }

		/// <summary>
		/// Gets or sets the treatment.
		/// </summary>
		public string? Treatment { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is processed.
		/// </summary>
		public bool IsProcessed { get; set; }

		/// <summary>
		/// Gets or sets the doctor signature.
		/// </summary>
		public byte[]? DoctorSignature { get; set; }

		#endregion

		#region Relations

		/// <summary>
		/// Gets or sets the patient identifier.
		/// </summary>
		public Guid PatientId { get; set; }

		/// <summary>
		/// Gets or sets the patient.
		/// </summary>
		public Patient? Patient { get; set; }

		/// <summary>
		/// Gets or sets the block identifier.
		/// </summary>
		public Guid? BlockId { get; set; }

		/// <summary>
		/// Gets or sets the block.
		/// </summary>
		public Block? Block { get; set; }

		#endregion
	}
}
