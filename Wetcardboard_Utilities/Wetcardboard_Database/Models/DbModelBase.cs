using System.Data;

namespace Wetcardboard_Database.Models
{
    public interface DbModelBase
    {
        static abstract DbModelBase CreateFromDataRow(DataRow row);
    }
}
