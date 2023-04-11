namespace Wetcardboard_Utilities_Api_Services.Interfaces
{
    public interface IWetcardboard_Utilities_ApiService_TokenService
    {
        Task<bool> CreateUserJwtTokenAsync(string userGuid);
    }
}
