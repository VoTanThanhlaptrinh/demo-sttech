using AutoMapper;

namespace Demo.Faqs.Dto;

public class FrequentlyAskedQuestionMapProfile : Profile
{
    public FrequentlyAskedQuestionMapProfile()
    {
        CreateMap<CreateFrequentlyAskedQuestionInput, FrequentlyAskedQuestion>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.SortOrder, opt => opt.Ignore());

        CreateMap<UpdateFrequentlyAskedQuestionInput, FrequentlyAskedQuestion>()
            .ForMember(x => x.SortOrder, opt => opt.Ignore());

        CreateMap<FrequentlyAskedQuestion, FrequentlyAskedQuestionDto>();
    }
}
