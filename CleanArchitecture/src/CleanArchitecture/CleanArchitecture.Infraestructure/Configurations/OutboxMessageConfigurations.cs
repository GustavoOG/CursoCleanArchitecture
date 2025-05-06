using CleanArchitecture.Infraestructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Configurations
{
    internal sealed class OutboxMessageConfigurations : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("outbox_messages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).HasColumnType("jsonb");
        }
    }
}
