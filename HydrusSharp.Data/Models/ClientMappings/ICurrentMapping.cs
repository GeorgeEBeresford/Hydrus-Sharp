using System.Collections.Generic;

namespace HydrusSharp.Data.Models.ClientMappings
{
    public interface ICurrentMapping
    {
        int TagId { get; set; }

        int HashId { get; set; }

        IEnumerable<string> Tags { get; set; }
    }
}
