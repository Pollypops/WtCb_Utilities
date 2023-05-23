using Microsoft.AspNetCore.Components;
using Wetcardboard_Models.Ui;
using Wetcardboard_Services.Implementations;

namespace Wetcardboard_Components.Ui.Toast
{
    public partial class Toast_Container : ComponentBase
    {
        #region Fields & Properties
        #region Properties
        public List<Wetcardboard_ToastMessage> ToastMessages { get; set; } = new List<Wetcardboard_ToastMessage>();
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Methods
        #region Private Methods
        private async Task CheckToastMessages()
        {
            while (true)
            {
                await Task.Delay(2500);

                var expiredMsgs = ToastMessages.Where(x => DateTime.Now > x.ExpiryTime)?.ToList();
                if (expiredMsgs is null)
                {
                    continue;
                }

                foreach (var expiredMsg in expiredMsgs)
                {
                    ToastMessages.Remove(expiredMsg);
                }

                StateHasChanged();
            }
        }
        private void ToastMessageCreated(object sender, ToastMessageEventArgs e)
        {
            var toastMessage = new Wetcardboard_ToastMessage
            {
                ExpiryTime = DateTime.Now.AddSeconds(35),
                Message = e.ToastMessage
            };
            ToastMessages.Add(toastMessage);
            StateHasChanged();
        }
        #endregion \ Private Methods

        #region Public Methods
        public async Task ToastMessageOnClick(EventArgs e, Wetcardboard_ToastMessage tMessage)
        {
            if (tMessage is null)
            {
                return;
            }
            ToastMessages.Remove(tMessage);
            StateHasChanged();
        }
        #endregion \ Public Methods
        #endregion \ Methods


        #region Overrides
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _toastService.CreateToastMessageHandler += ToastMessageCreated;

                await CheckToastMessages();
            }

            await base.OnAfterRenderAsync(firstRender);
        }
        #endregion \ Overrides
    }
}
