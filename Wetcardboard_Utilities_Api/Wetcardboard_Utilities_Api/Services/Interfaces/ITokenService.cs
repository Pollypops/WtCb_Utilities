namespace Wetcardboard_Utilities_Api.Services.Interfaces
{
    public interface ITokenService
    {
        bool CreateUserJwtToken(string userGuid);
    }
}
