namespace MedicalBlockchain.DAL.Entities
{
	/// <summary>
	/// Hold information about a patient.
	/// </summary>
	public class Patient
	{
		#region Public properties

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string? Name { get; set; }

		#endregion

		#region Relations

		/// <summary>
		/// Gets or sets the medical records.
		/// </summary>
		public List<MedicalRecord>? MedicalRecords { get; set; }

		/// <summary>
		/// Gets or sets the blocks.
		/// </summary>
		public List<Block>? Blocks { get; set; }

		#endregion
	}
}
