using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Demo.Faqs.Dto;

/// <summary>
/// DTO trả về thông tin FAQ cho client.
/// </summary>
[AutoMapFrom(typeof(FrequentlyAskedQuestion))]
public class FrequentlyAskedQuestionDto : EntityDto<int>
{
    public string Question { get; set; }

    public string Answer { get; set; }

    public FaqStatus Status { get; set; }

    public int? SortOrder { get; set; }

    public DateTime CreationTime { get; set; }

    public long? CreatorUserId { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public long? LastModifierUserId { get; set; }
}
