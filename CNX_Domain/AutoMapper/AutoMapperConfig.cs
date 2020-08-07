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

            CreateMap<UserVM, User>();
            CreateMap<UserVM, UserFullVM>();
            CreateMap<UserVM, CreateUserVM>();

            CreateMap<UserFullVM, User>();
            CreateMap<UserFullVM, UserVM>();

            CreateMap<CreateUserVM, UserVM>();
            CreateMap<CreateUserVM, UserFullVM>();

            CreateMap<PersonalNote, PersonalNoteVM>();

            CreateMap<PersonalNoteVM, PersonalNote>()
                .ForMember(c => c.CascadeMode, o => o.Ignore());
        }
        public void Register()
        {

        }
    }
}
