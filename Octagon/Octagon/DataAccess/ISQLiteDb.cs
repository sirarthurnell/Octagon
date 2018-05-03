using SQLite;

namespace Octagon.DataAccess
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

