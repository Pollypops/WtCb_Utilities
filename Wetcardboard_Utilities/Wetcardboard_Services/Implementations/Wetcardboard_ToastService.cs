using Wetcardboard_Services.Interfaces;

namespace Wetcardboard_Services.Implementations
{
    public class ToastMessageEventArgs : EventArgs
    {
        public string ToastMessage { get; set; } = string.Empty;
    }
    public class Wetcardboard_ToastService : IWetcardboard_ToastService
    {
        #region IWetcardboard_ToastService Implementation
        public event EventHandler<ToastMessageEventArgs> CreateToastMessageHandler;

        public void CreateToastMessage(string message)
        {
            if (CreateToastMessageHandler is null)
            {
                return;
            }

            var args = new ToastMessageEventArgs
            {
                ToastMessage = message
            };
            CreateToastMessageHandler(this, args);
        }
        #endregion \ IWetcardboard_ToastService Implementation
    }
}
