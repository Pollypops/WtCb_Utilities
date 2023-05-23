using Wetcardboard_Services.Implementations;

namespace Wetcardboard_Services.Interfaces
{
    public interface IWetcardboard_ToastService
    {
        event EventHandler<ToastMessageEventArgs> CreateToastMessageHandler;

        void CreateToastMessage(string message);
    }
}
