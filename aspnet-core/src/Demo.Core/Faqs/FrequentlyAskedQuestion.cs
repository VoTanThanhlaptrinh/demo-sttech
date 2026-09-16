using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Domain.Entities.Auditing;

namespace Demo.Faqs;

[Audited]
public class FrequentlyAskedQuestion : FullAuditedEntity<int>
{
    [Required]
    [StringLength(FrequentlyAskedQuestionConsts.MaxQuestionLength)]
    public string Question { get; set; }

    [Required]
    [StringLength(FrequentlyAskedQuestionConsts.MaxAnswerLength)]
    public string Answer { get; set; }

    public FaqStatus Status { get; set; } = FaqStatus.Public;

    [Range(1, int.MaxValue)]
    public int? SortOrder { get; set; }
}