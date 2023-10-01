using System.Collections.Generic;

namespace HydrusSharp.Data.Models.ClientMappings
{
    public class CurrentMapping : ICurrentMapping
    {
        public int TagId { get; set; }

        public int HashId { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}