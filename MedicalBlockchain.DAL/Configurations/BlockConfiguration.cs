using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MedicalBlockchain.DAL.Entities;

namespace MedicalBlockchain.DAL.Configurations
{
	/// <summary>
	/// Block configiration.
	/// </summary>
	public class BlockConfiguration : IEntityTypeConfiguration<Block>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Block> builder)
		{
			builder.HasKey(b => b.Id);

			builder.Property(b => b.Timestamp)
				.IsRequired();
			
			builder.Property(b => b.PreviousHash)
				.IsRequired(false);
			
			builder.Property(b => b.Hash)
				.IsRequired(false);
			
			builder.Property(b => b.Nonce)
				.IsRequired();
			
			builder.Property(b => b.DoctorSignature)
				.IsRequired(false);
			
			builder.HasMany(b => b.MedicalRecords)
				.WithOne(mr => mr.Block)
				.HasForeignKey(mr => mr.BlockId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
