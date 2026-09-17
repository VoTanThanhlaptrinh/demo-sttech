using Abp.Application.Services.Dto;
using Demo.Faqs;

namespace Demo.Faqs.Dto
{
    public class PagedFaqResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public FaqStatus? Status { get; set; }
    }
}
