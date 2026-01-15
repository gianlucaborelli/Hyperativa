using Hyperativa.Api.Models.Dto;
using Hyperativa.Core.Controller;

namespace Hyperativa.Api.Services;

public interface IIdentityService
{
    Task<ServiceResponse<string>> Login(LoginDto request);
    Task<ServiceResponse<Guid>> RegisterUser(RegisterUserDto request);
}
