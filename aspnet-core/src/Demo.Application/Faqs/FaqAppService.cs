using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Demo.Faqs.Dto;
using Microsoft.EntityFrameworkCore;
using Abp.Extensions;
using System.Linq.Dynamic.Core;

namespace Demo.Faqs
{
    public class FaqAppService : AsyncCrudAppService<FrequentlyAskedQuestion, FrequentlyAskedQuestionDto, int, PagedFaqResultRequestDto, CreateFrequentlyAskedQuestionInput, UpdateFrequentlyAskedQuestionInput>, IFaqAppService
    {
        public FaqAppService(IRepository<FrequentlyAskedQuestion, int> repository) : base(repository)
        {
        }

        protected override IQueryable<FrequentlyAskedQuestion> CreateFilteredQuery(PagedFaqResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Question.Contains(input.Keyword) || x.Answer.Contains(input.Keyword))
                .WhereIf(input.Status.HasValue, x => x.Status == input.Status);
        }

        protected override IQueryable<FrequentlyAskedQuestion> ApplySorting(IQueryable<FrequentlyAskedQuestion> query, PagedFaqResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime);
        }

        public override async Task<FrequentlyAskedQuestionDto> CreateAsync(CreateFrequentlyAskedQuestionInput input)
        {
            await CheckDuplicateQuestionAsync(input.Question, null);
            return await base.CreateAsync(input);
        }

        public override async Task<FrequentlyAskedQuestionDto> UpdateAsync(UpdateFrequentlyAskedQuestionInput input)
        {
            await CheckDuplicateQuestionAsync(input.Question, input.Id);
            return await base.UpdateAsync(input);
        }

        private async Task CheckDuplicateQuestionAsync(string question, int? expectedId = null)
        {
            var isExist = await Repository.GetAll()
                .Where(faq => faq.Question.ToLower() == question.ToLower())
                .WhereIf(expectedId.HasValue, faq => faq.Id != expectedId.Value)
                .AnyAsync();

            if (isExist)
            {
                throw new UserFriendlyException("Câu hỏi này đã tồn tại. Vui lòng nhập câu hỏi khác.");
            }
        }
    }
}
