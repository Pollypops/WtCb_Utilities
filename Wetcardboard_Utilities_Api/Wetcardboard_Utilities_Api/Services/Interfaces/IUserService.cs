using Wetcardboard_Utilities_Models.Database;

namespace Wetcardboard_Utilities_Api.Services.Interfaces
{
    public interface IUserService
    {
        bool SaveUserSettings(string userGuid, IEnumerable<Wetcardboard_Utilities_UserSettings> settings);
        IEnumerable<Wetcardboard_Utilities_UserSettings> GetUserSettings(string userGuid);
    }
}
