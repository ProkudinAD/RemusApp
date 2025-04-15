using Content.Domain.Entities.News;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Content.Infrastructure.Configuration.News
{
public sealed class NewsMainConfiguration : IEntityTypeConfiguration<NewsMain>
{
    public void Configure(EntityTypeBuilder<NewsMain> builder) 
    {
      builder.ToTable("NewsMain", "Content");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.NewsTitle)
         .HasColumnType("text");


      builder.Property(x => x.NewsText)
           .HasMaxLength(200);
    }
}
}