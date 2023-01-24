using AutoMapper;
using PasswordManager5._0.Entities;

namespace PasswordManager5._0
{
using PasswordManager5._0.Models;

public class PasswordmanagerMappingProfile : Profile
    {
        public PasswordmanagerMappingProfile()
        {
            CreateMap<SignupUserDTO, User>();
            CreateMap<LoginUserDTO, User>();
            CreateMap<AddNewPasswordDTO, Password>();
            CreateMap<DeletePasswordDTO, Password>();
        }

    }
}
