using IdentityModel.Client;

namespace Arqom.Utilities.SoftwarePartDetector.Authentications;

public interface ISoftwarePartAuthentication
{
    Task<TokenResponse> LoginAsync();
}