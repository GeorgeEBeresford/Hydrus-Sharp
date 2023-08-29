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
            string matchingOption = null;

            for (int optionIndex = 0; optionIndex < options.Length && matchingOption == null; optionIndex++)
            {
                // If the option is a primitive data type, return the value directly
                if (options[optionIndex].StartsWith($"{name}: "))
                {
                    matchingOption = options[optionIndex].Substring($"{name}:".Length);
                }
                else if (options[optionIndex].StartsWith($"{name}:"))
                {
                    bool subOptionsCompleted = false;
                    matchingOption = "";

                    // We don't know how many subvalues a list type might have. Parse any lines beginning with "-" as a sub option until we meet another option name
                    for (int subOptionIndex = optionIndex; subOptionIndex < options.Length && subOptionsCompleted != true; subOptionIndex++)
                    {
                        if (options[subOptionIndex].StartsWith("- "))
                        {
                            matchingOption += options[subOptionIndex].Substring(2) + '\n';
                        }
                    }
                }
            }

            // If the option has more than 1 value (begins with a "-") then return a concatenated collection of all the possible values
            return matchingOption;
        }
    }
}