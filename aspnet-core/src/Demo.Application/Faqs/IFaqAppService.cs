using Abp.Application.Services;
using Demo.Faqs.Dto;

namespace Demo.Faqs
{
    public interface IFaqAppService : IAsyncCrudAppService<FrequentlyAskedQuestionDto, int, PagedFaqResultRequestDto, CreateFrequentlyAskedQuestionInput, UpdateFrequentlyAskedQuestionInput>
    {
    }
}
