using AutoMapper;
using CNX_Domain.Entities;
using CNX_Domain.Models;

namespace CNX_Domain.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserVM>();

            CreateMap<UserVM, User>()
                .ForMember(user => user.CascadeMode, vm => vm.Ignore())
                .ForMember(user => user.ValidationResult, vm => vm.Ignore());

            CreateMap<PersonalNote, PersonalNoteVM>();

            CreateMap<PersonalNoteVM, PersonalNote>()
                .ForMember(c => c.CascadeMode, o => o.Ignore());
        }
        public void Register()
        {
          
        }
    }
}
