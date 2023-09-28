using DataAccessLayer;
using HydrusSharp.Data.Models.ClientMaster;
using HydrusSharp.Data.Services;
using System;

namespace HydrusSharp.Data.Repositories.DataAccess
{
    public class DAHashRepository : IHashRepository
    {
        public Hash GetById(int hashId)
        {
            DbRow row;

            using (DbContext dbContext = new DbContext(ConnectionStringService.GetConnectionString("ClientMaster")))
            {
                row = dbContext.GetDataRow("SELECT * FROM hashes WHERE hash_id = @hashId", new[] { DbContext.GenerateParameter("@hashId", hashId) });
            }

            return row != null ? new Hash
            {
                HashId = Convert.ToInt32(row.GetColumnValue("hash_id")),
                HashBytes = (byte[])row.GetColumnValue("hash")
            }
            : null;
        }
    }
}
