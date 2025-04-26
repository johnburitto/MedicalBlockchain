using Microsoft.EntityFrameworkCore;

using MedicalBlockchain.DAL.Entities;
using MedicalBlockchain.DAL.Configurations;

namespace MedicalBlockchain.DAL.Data
{
	/// <summary>
	/// App db context.
	/// </summary>
	public class AppDbContext : DbContext
	{
		#region Public properties

		/// <summary>
		/// Gets or sets the patients.
		/// </summary>
		public DbSet<Patient> Patients { get; set; }


		/// <summary>
		/// Gets or sets the medical records.
		/// </summary>
		public DbSet<MedicalRecord> MedicalRecords { get; set; }

		/// <summary>
		/// Gets or sets the blocks.
		/// </summary>
		public DbSet<Block> Blocks { get; set; }

		/// <summary>
		/// Gets or sets the doctors.
		/// </summary>
		public DbSet<Doctor> Doctors { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="AppDbContext"/> class.
		/// </summary>
		/// <param name="options">The options.</param>
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		#endregion

		#region Overrides

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new BlockConfiguration());
		}

		#endregion
	}
}
