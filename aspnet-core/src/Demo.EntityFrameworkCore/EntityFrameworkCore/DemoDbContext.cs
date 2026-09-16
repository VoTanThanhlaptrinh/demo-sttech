using Abp.Zero.EntityFrameworkCore;
using Demo.Authorization.Roles;
using Demo.Authorization.Users;
using Demo.Faqs;
using Demo.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Demo.EntityFrameworkCore;

public class DemoDbContext : AbpZeroDbContext<Tenant, Role, User, DemoDbContext>
{
    /* Define a DbSet for each entity of the application */
    public virtual DbSet<FrequentlyAskedQuestion> FrequentlyAskedQuestions { get; set; }

    public DemoDbContext(DbContextOptions<DemoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FrequentlyAskedQuestion>(b =>
        {
            b.ToTable("AppFrequentlyAskedQuestions");

            b.Property(x => x.Question)
                .IsRequired()
                .HasMaxLength(FrequentlyAskedQuestionConsts.MaxQuestionLength);

            b.Property(x => x.Answer)
                .IsRequired()
                .HasMaxLength(FrequentlyAskedQuestionConsts.MaxAnswerLength);

            b.Property(x => x.Status)
                .IsRequired()
                .HasDefaultValue(FaqStatus.Public);

            b.Property(x => x.DisplayOrder)
                .IsRequired()
                .HasDefaultValue(0);

            // Ràng buộc: Không được trùng hoàn toàn với câu hỏi chưa bị xóa
            b.HasIndex(x => x.Question)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");
        });
    }
}
