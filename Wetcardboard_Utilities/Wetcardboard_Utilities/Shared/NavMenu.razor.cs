namespace Wetcardboard_Utilities.Shared
{
    public partial class NavMenu
    {
        #region Fields & Properties
        #region Fields
        private bool collapseNavMenu = true;
        #endregion \ Fields

        #region Properties
        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;
        #endregion \ Properties
        #endregion \ Fields & Properties


        #region Methods
        #region Private Methods
        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
        #endregion \ Private Methods
        #endregion \ Methods
    }
}
