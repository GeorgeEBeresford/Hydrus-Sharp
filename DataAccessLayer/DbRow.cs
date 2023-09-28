using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataAccessLayer
{
    public class DbRow
    {
        private IDictionary<string, object> Data { get;set; }

        public DbRow(DbDataReader dataReader)
        {
            Data = new Dictionary<string, object>(dataReader.FieldCount);

            for (int columnIndex = 0; columnIndex < dataReader.FieldCount; columnIndex++)
            {
                string columnName = dataReader.GetName(columnIndex);
                Data.Add(columnName, dataReader.GetValue(columnIndex));
            }
        }

        public object GetColumnValue(string columnName)
        {
            return Data[columnName];
        }
    }
}
