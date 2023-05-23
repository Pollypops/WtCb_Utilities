namespace Wetcardboard_Utilities.Pages.Calendar
{
    public partial class CalendarPage
    {
        #region Fields & Properties
        #region Properties
        private DateTime _currentTime = DateTime.Now;
        private string CurrentDateString
        {
            get
            {
                return $"{_currentTime:yyyy-MM-dd hh:mm:ss}";
            }
        }
        #endregion \ Properties
        #endregion \ Fields & Properties
    }
}
