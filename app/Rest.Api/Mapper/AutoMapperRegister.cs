using AutoMapper;
using Rest.Api.Models.Request;
using Rest.Api.Models.Response;
using Rest.Domain.App;

namespace Rest.Api.Mapper
{
    public static class AutoMapperRegister
    {
        public static void ModelMapperConfiguration(this IMapperConfigurationExpression mapperExpression) 
        {
            mapperExpression.CreateMap<Rest.Api.Models.Response.RoleModel, Rest.Infra.CrossCutting.Utils.Enum.Role>()
                .ReverseMap();

            //AutoMapper expression to map the User entity source to UserModel destination
            mapperExpression.CreateMap<User, UserModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(from => from.Id)
                )
                .ForMember(
                    dest => dest.Username,
                    opt => opt.MapFrom(from => from.Username)
                )
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(from => from.FirstName)
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(from => from.LastName)
                )
                .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(from => from.Role)
                );

            mapperExpression.CreateMap<UserRequestModel, User>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(from => from.Id)
                )
                .ForMember(
                    dest => dest.Username,
                    opt => opt.MapFrom(from => from.Username)
                )
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(from => from.FirstName)
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(from => from.LastName)
                )
                .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(from => from.Role)
                )
                .ForMember(
                    dest => dest.PaswordHash,
                    opt => opt.MapFrom(from => from.PasswordHash)
                );
        }
    }
}
