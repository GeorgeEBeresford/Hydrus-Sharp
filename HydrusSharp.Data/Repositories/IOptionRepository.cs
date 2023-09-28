using HydrusSharp.Data.Models.Client;

namespace HydrusSharp.Data.Repositories
{
    /// <summary>
    /// Retrieves data relating to saved options
    /// </summary>
    public interface IOptionRepository
    {
        /// <summary>
        /// Gets all of the options as a collection
        /// </summary>
        /// <returns></returns>
        OptionCollection GetOptions();
    }
}
