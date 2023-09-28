using DataAccessLayer;
using HydrusSharp.Data.Models.Client;
using HydrusSharp.Data.Services;

namespace HydrusSharp.Data.Repositories.DataAccess
{
    public class DAOptionRepository : IOptionRepository
    {
        public OptionCollection GetOptions()
        {
            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("Client")))
            {
                DbRow row = dbContext.GetDataRowAsync("SELECT options FROM options").GetAwaiter().GetResult();

                if (row == null)
                {
                    return null;
                }

                string value = row.GetColumnValue("options")?.ToString();

                return new OptionCollection { Value = value };
            }
        }
    }
}
