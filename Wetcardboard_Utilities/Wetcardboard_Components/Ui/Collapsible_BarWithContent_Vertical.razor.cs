using Microsoft.AspNetCore.Components;
using System.Text;

namespace Wetcardboard_Components.Ui
{
    public partial class Collapsible_BarWithContent_Vertical
    {
        #region Fields & Properties
        #region Parameters
        [Parameter]
        public bool Collapsed { get; set; } = false;
        [Parameter]
        public bool ReverseHeader { get; set; } = false;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public string Border { get; set; } = "1px solid #969696";
        [Parameter]
        public string ContentContainerHeight { get; set; } = "100px";
        [Parameter]
        public string ContentContainerPadding { get; set; } = "10px";
        [Parameter]
        public string HeaderContainerBorderBottom { get; set; } = "1px solid #D1D1D1";
        [Parameter]
        public string HeaderContainerPadding { get; set; } = "10px 10px 5px 10px";
        [Parameter]
        public string HeaderText { get; set; } = string.Empty;
        [Parameter]
        public string HeaderTextFontSize { get; set; } = "16px";
        [Parameter]
        public string HeaderTextPadding { get; set; } = "0";
        #endregion \ Parameters


        #region Properties
        private string ContentContainerStyle
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append($"height: {ContentContainerHeight};");
                sb.Append($"padding: {ContentContainerPadding};");
                sb.Append($"overflow: hidden;");
                sb.Append($"transition: height 0.4s ease-out;");
                var res = sb.ToString();
                return res;
            }
        }
        private string ContentRootContainerStyle
        {
            get
            {
                string height;
                if (Collapsed)
                {
                    height = "0";
                }
                else
                {
                    height = $"{ContentContainerHeight}";

                }

                var sb = new StringBuilder();
                sb.Append($"max-height: {height};");
                sb.Append("overflow: hidden;");
                sb.Append("transition: max-height 0.4s ease-out;");
                sb.Append("border-top: 10px;");
                sb.Append("border-bottom: 10px;");
                var res = sb.ToString();
                return res;
            }
        }
        private string HeaderContainerClass
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("headerContainer ");
                if (ReverseHeader)
                {
                    sb.Append("headerContainer_Order_Reversed ");
                }
                else
                {
                    sb.Append("headerContainer_Order ");
                }
                var res = sb.ToString();
                return res;
            }
        }
        private string HeaderContainerStyle
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append($"border-bottom: {HeaderContainerBorderBottom};");
                sb.Append($"cursor: pointer;");
                sb.Append($"padding: {HeaderContainerPadding};");
                var res = sb.ToString();
                return res;
            }
        }
        private string HeaderIconClass
        {
            get
            {
                string rotation;
                if (Collapsed)
                {
                    if (ReverseHeader)
                    {
                        rotation = "upside-down-rotation-reversed";
                    }
                    else
                    {
                        rotation = "upside-down-rotation";
                    }
                }
                else
                {
                    rotation = "no-rotation";
                }

                var sb = new StringBuilder();
                sb.Append($"{rotation} ");
                sb.Append("header-icon");
                var res = sb.ToString();
                return res;
            }
        }
        private string HeaderIconStyle
        {
            get
            {
                string animationName;
                if (Collapsed)
                {
                    if (ReverseHeader)
                    {
                        animationName = "upside-down-rotation-reversed";
                    }
                    else
                    {
                        animationName = "upside-down-rotation";
                    }
                }
                else
                {
                    animationName = "no-rotation";
                }

                var sb = new StringBuilder();
                sb.Append($"animation-name: {animationName};");
                sb.Append("transition-duration: 0.4s;");
                sb.Append($"font-size: {HeaderTextFontSize};");
                sb.Append($"height: {HeaderTextFontSize};");
                sb.Append($"width: {HeaderTextFontSize};");
                sb.Append($"max-height: {HeaderTextFontSize};");
                sb.Append($"max-width: {HeaderTextFontSize};");
                var res = sb.ToString();
                return res;
            }
        }
        private string HeaderTextStyle
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append($"font-size: {HeaderTextFontSize};");
                sb.Append($"padding: 0;");
                var res = sb.ToString();
                return res;
            }
        }
        private string RootStyle
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append($"border: {Border};");
                var res = sb.ToString();
                return res;
            }
        }
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Methods
        #region Private Methods
        private void ToggleCollapsed()
        {
            Collapsed = !Collapsed;
        }
        #endregion \ Private Methods
        #endregion \ Methods
    }
}
