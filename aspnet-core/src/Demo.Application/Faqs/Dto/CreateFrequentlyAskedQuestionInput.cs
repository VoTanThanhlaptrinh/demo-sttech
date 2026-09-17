using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;

namespace Demo.Faqs.Dto;

/// <summary>
/// DTO nhận dữ liệu tạo mới FAQ.
/// </summary>
[AutoMapTo(typeof(FrequentlyAskedQuestion))]
public class CreateFrequentlyAskedQuestionInput : IShouldNormalize, IValidatableObject
{
    private string _question;
    private string _answer;

    /// <summary>
    /// Câu hỏi (Bắt buộc, tối đa 500 ký tự, tự động Trim, không được chỉ chứa khoảng trắng)
    /// </summary>
    [Required(ErrorMessage = "Câu hỏi là bắt buộc.")]
    [StringLength(FrequentlyAskedQuestionConsts.MaxQuestionLength, ErrorMessage = "Độ dài câu hỏi tối đa là 500 ký tự.")]
    public string Question
    {
        get => _question;
        set => _question = value?.Trim();
    }

    /// <summary>
    /// Câu trả lời (Bắt buộc, tối đa 4000 ký tự, tự động Trim, không được chỉ chứa khoảng trắng)
    /// </summary>
    [Required(ErrorMessage = "Câu trả lời là bắt buộc.")]
    [StringLength(FrequentlyAskedQuestionConsts.MaxAnswerLength, ErrorMessage = "Độ dài câu trả lời tối đa là 4000 ký tự.")]
    public string Answer
    {
        get => _answer;
        set => _answer = value?.Trim();
    }

    /// <summary>
    /// Trạng thái FAQ (Mặc định: Hidden = 2. Hợp lệ: Published = 1, Hidden = 2)
    /// </summary>
    [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
    public FaqStatus Status { get; set; } = FaqStatus.Hidden;

    /// <summary>
    /// Tự động Trim dữ liệu trước khi xử lý theo chuẩn ABP IShouldNormalize
    /// </summary>
    public void Normalize()
    {
        Question = Question?.Trim();
        Answer = Answer?.Trim();
    }

    /// <summary>
    /// Kiểm tra tính hợp lệ của dữ liệu đầu vào
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Question))
        {
            yield return new ValidationResult(
                "Câu hỏi không được chỉ chứa khoảng trắng.",
                new[] { nameof(Question) });
        }

        if (string.IsNullOrWhiteSpace(Answer))
        {
            yield return new ValidationResult(
                "Câu trả lời không được chỉ chứa khoảng trắng.",
                new[] { nameof(Answer) });
        }

        if (!Enum.IsDefined(typeof(FaqStatus), Status) ||
            (Status != FaqStatus.Published && Status != FaqStatus.Hidden))
        {
            yield return new ValidationResult(
                "Trạng thái không hợp lệ. Chỉ chấp nhận Published (1) hoặc Hidden (2).",
                new[] { nameof(Status) });
        }
    }
}
