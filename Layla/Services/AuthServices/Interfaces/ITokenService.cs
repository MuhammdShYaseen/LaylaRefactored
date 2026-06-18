using Layla.Models.DtosModels.AuthDtos;
using Layla.Models.MainModels;

namespace Layla.Services.AuthServices.Interfaces
{
    public interface ITokenService
    {
        string GenerateRandomToken();
        RefreshToken CreateRefreshToken(string ipAddress, int userId);
        Task<AuthResponse> GenerateAuthResponseAsync(User user, string originIp);
        (string Token, DateTime Expires) GenerateJwtToken(User user);
    }
}
