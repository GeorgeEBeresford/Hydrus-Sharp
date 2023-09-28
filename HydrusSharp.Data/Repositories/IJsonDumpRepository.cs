using HydrusSharp.Data.Models.Client;
using System.Collections.Generic;

namespace HydrusSharp.Data.Repositories
{
    public interface IJsonDumpRepository
    {
        /// <summary>
        /// Gets all of the saved JSON dumps
        /// </summary>
        /// <returns></returns>
        IEnumerable<JsonDump> GetJsonDumps();

        /// <summary>
        /// Gets all of the saved named JSON dumps
        /// </summary>
        /// <returns></returns>
        IEnumerable<NamedJsonDump> GetSessions();

        /// <summary>
        /// Gets all of the saved hashed JSON dumps
        /// </summary>
        /// <returns></returns>
        IEnumerable<HashedJsonDump> GetHashedJsonDumps();

        /// <summary>
        /// Gets the hashed JSON dump which matches the specified hash
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        HashedJsonDump GetHashedJsonDump(string hash);
    }
}
