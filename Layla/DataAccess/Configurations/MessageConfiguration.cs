using Layla.DomainEvents.Domain.Common;
using Layla.Models.MainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Layla.DataAccess.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(m => m.Conversation)
                   .WithMany(c => c.Messages)
                   .HasForeignKey(m => m.ConversationId);
            //builder.HasIndex(m => new { m.ConversationId, m.IsRead });
            builder.HasIndex(m => new { m.ReceiverId, m.IsRead });
            builder.HasIndex(m => new { m.ConversationId, m.ReceiverId, m.IsRead });
            builder.HasIndex(m => new {  m.ConversationId,  m.CreatedAt });
        }
    }
}
