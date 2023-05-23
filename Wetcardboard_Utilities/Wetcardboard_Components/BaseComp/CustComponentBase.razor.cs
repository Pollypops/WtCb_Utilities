using Microsoft.AspNetCore.Components;
using System.Text;

namespace Wetcardboard_Components.BaseComp
{
    public partial class CustComponentBase
    {
        #region Fields & Properties
        #region Parameters
        [Parameter]
        public Func<EventArgs, Task> OnClick { get; set; } = async (e) => { /* Default to do nothing */ };
        [Parameter]
        public string Align_Content { get; set; } = "unset";
        [Parameter]
        public string Align_Items { get; set; } = "unset";
        [Parameter]
        public string Align_Self { get; set; } = "unset";
        [Parameter]
        public string Justify_Content { get; set; } = "unset";
        [Parameter]
        public string Justify_Items { get; set; } = "unset";
        [Parameter]
        public string Justify_Self { get; set; } = "unset";
        [Parameter]
        public string Margin_Bottom { get; set; } = "0px";
        [Parameter]
        public string Margin_Left { get; set; } = "0px";
        [Parameter]
        public string Margin_Right { get; set; } = "0px";
        [Parameter]
        public string Margin_Top { get; set; } = "0px";
        [Parameter]
        public string Padding_Bottom { get; set; } = "0px";
        [Parameter]
        public string Padding_Left { get; set; } = "0px";
        [Parameter]
        public string Padding_Right { get; set; } = "0px";
        [Parameter]
        public string Padding_Top { get; set; } = "0px";
        [Parameter]
        public string Height { get; set; } = "auto";
        [Parameter]
        public string Width { get; set; } = "auto";
        [Parameter]
        public string Text { get; set; } = "Button";
        #endregion \ Parameters

        #region Properties
        protected string ComponentStyle
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append($"height: {Height};");
                sb.Append($"width: {Width};");
                sb.Append($"margin: 0;");
                sb.Append($"margin-bottom: {Margin_Bottom};");
                sb.Append($"margin-left: {Margin_Left};");
                sb.Append($"margin-right: {Margin_Right};");
                sb.Append($"margin-top: {Margin_Top};");
                sb.Append($"padding-bottom: {Padding_Bottom};");
                sb.Append($"padding-left: {Padding_Left};");
                sb.Append($"padding-right: {Padding_Right};");
                sb.Append($"padding-top: {Padding_Top};");
                sb.Append($"Align-Content: {Align_Content};");
                sb.Append($"Align-Items: {Align_Items};");
                sb.Append($"Align-Self: {Align_Self};");
                sb.Append($"Justify-Content: {Justify_Content};");
                sb.Append($"Justify-Items: {Justify_Items};");
                sb.Append($"Justify-Self: {Justify_Self};");
                return sb.ToString();
            }
        }
        #endregion \ Properties
        #endregion \ Fields & Properties
    }
}
