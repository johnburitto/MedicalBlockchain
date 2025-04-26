namespace MedicalBlockchain.DAL.Dtos
{
	/// <summary>
	/// Holds data for auth operations.
	/// </summary>
	public class AuthDto
	{
		#region Public properties

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		public string? Username { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string? Password { get; set; }

		#endregion
	}
}
