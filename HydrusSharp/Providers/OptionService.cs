using HydrusSharp.DbContexts;
using System.Linq;

namespace HydrusSharp.Repositories
{
    public class OptionRepository
    {
        private ClientDbContext ClientDbContext { get; set; }

        public OptionRepository(ClientDbContext dbContext) { 

            ClientDbContext = dbContext;
        }

        public string GetOption(string name)
        {
            string[] options = ClientDbContext.Options.First().Value.Split('\n');
            string matchingOption = options.First(option => option.StartsWith($"{name}: "));

            return matchingOption.Substring($"{name}: ".Length);
        }
    }
}