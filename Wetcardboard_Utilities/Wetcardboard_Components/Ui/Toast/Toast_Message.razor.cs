using Microsoft.AspNetCore.Components;

namespace Wetcardboard_Components.Ui.Toast
{
    public partial class Toast_Message
    {
        #region Fields & Properties
        #region Parameters
        [Parameter]
        public Func<EventArgs, Task> OnClick { get; set; } = async (e) => { /* Default to do nothing */ };

        [Parameter]
        public string Message { get; set; } = string.Empty;
        #endregion \ Parameters
        #endregion \ Fields & Properties
    }
}
