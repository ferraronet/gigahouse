using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.Auth.AuthenticateUser;

public sealed class AuthenticateUserProfile : Profile
{
    public AuthenticateUserProfile()
    {
        CreateMap<User, AuthenticateUserResult>()
            .ForMember(dest => dest.Token, opt => opt.Ignore());

        CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
        CreateMap<AuthenticateUserResult, AuthenticateUserResponse>();
        CreateMap<User, AuthenticateUserResponse>()
            .ForMember(dest => dest.Token, opt => opt.Ignore());
    }
}
