namespace MedicalBlockchain.DAL.Entities
{
	/// <summary>
	/// Hold information about a doctor.
	/// </summary>
	public class Doctor
	{
		#region Public properties

		/// <summary>
		/// Gets or sets the identifier.
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		public string? Username { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string? Password { get; set; }

		/// <summary>
		/// Gets or sets the public key.
		/// </summary>
		public string? PublicKey { get; set; }

		/// <summary>
		/// Gets or sets the private key.
		/// </summary>
		public string? PrivateKey { get; set; }

		#endregion
	}
}
