namespace Wetcardboard_Utilities.Shared
{
    public partial class MainLayout
    {
        #region Methods
        #region Private Methods
        private void Login()
        {
            authenticator.Authenticate();
        }
        private void Logout()
        {
            authenticator.SignOut();
        }
        #endregion \ Private Methods
        #endregion \ Methods
    }
}
