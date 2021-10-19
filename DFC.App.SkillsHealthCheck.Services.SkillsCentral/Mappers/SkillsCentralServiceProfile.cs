using AutoMapper;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using SkillsDocumentService;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Mappers
{
    public class SkillsCentralServiceProfile : Profile
    {
        public SkillsCentralServiceProfile()
        {
            CreatDocumentMapping();
            GetDocumentMapping();
            AssementQuestionMapping();
        }

        private void AssementQuestionMapping()
        {
            CreateMap<Enums.AssessmentType, SkillsDocumentService.AssessmentType>();
            CreateMap<Enums.Level, SkillsDocumentService.Level>();
            CreateMap<Enums.Accessibility, SkillsDocumentService.Accessibility>();


            CreateMap<Models.Question, Question>()
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.Accessibility, opt => opt.MapFrom(src => src.Accessibility))
                .ForMember(dest => dest.AssessmentType, opt => opt.MapFrom(src => src.AssessmentType));

            CreateMap<SkillsDocumentService.AssessmentType, Enums.AssessmentType>();
            CreateMap<SkillsDocumentService.Level, Enums.Level>();
            CreateMap<SkillsDocumentService.Accessibility, Enums.Accessibility>();

            CreateMap<Question, Models.Question>()
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.Accessibility, opt => opt.MapFrom(src => src.Accessibility))
                .ForMember(dest => dest.AssessmentType, opt => opt.MapFrom(src => src.AssessmentType));

        }

        private void GetDocumentMapping()
        {
            CreateMap<SkillsDocumentExpiryEnum, SkillsDocumentExpiry>();
            CreateMap<Models.SkillsDocumentDataValue, SkillsDocumentDataValue>();
            CreateMap<SkillsDocument, Models.SkillsDocument>()
                .ForMember(dest => dest.SkillsDocumentExpiry, opt => opt.MapFrom(src => src.ExpiresType))
                .ForMember(dest => dest.SkillsDocumentDataValues, opt => opt.MapFrom(src => src.DataValues));
        }

        private void CreatDocumentMapping()
        {
            CreateMap<SkillsDocumentExpiry, SkillsDocumentExpiryEnum>();
            CreateMap<SkillsDocumentDataValue, Models.SkillsDocumentDataValue>();
            CreateMap<Models.SkillsDocument, SkillsDocument>()
                .ForMember(dest => dest.ExpiresType, opt => opt.MapFrom(src => src.SkillsDocumentExpiry))
                .ForMember(dest => dest.DataValues, opt => opt.MapFrom(src => src.SkillsDocumentDataValues));
        }
    }
}
