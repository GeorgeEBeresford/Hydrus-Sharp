using HydrusSharp.Data.Models.Client;

namespace HydrusSharp.Services
{
	public class OptionService
	{
		public string GetValue(OptionCollection optionCollection, string optionName)
		{
			string[] options = optionCollection.Value.Split('\n');
			string matchingOption = null;

			for (int optionIndex = 0; optionIndex < options.Length && matchingOption == null; optionIndex++)
			{
                // If the option is a primitive data type, return the value directly
                if (options[optionIndex].StartsWith($"{optionName}: "))
                {
					matchingOption = options[optionIndex].Substring($"{optionName}: ".Length);
                    return matchingOption;
                }
				else if (options[optionIndex].StartsWith($"{optionName}:"))
				{
					bool subOptionsCompleted = false;
					matchingOption = "";

					// We don't know how many subvalues a list type might have. Parse any lines beginning with "-" as a sub option until we meet another option name
					for (int subOptionIndex = optionIndex; subOptionIndex < options.Length && subOptionsCompleted != true; subOptionIndex++)
                    {
                        // If the option has more than 1 value (begins with a "-") then return a concatenated collection of all the possible values
                        if (options[subOptionIndex].StartsWith("- "))
						{
							matchingOption += options[subOptionIndex].Substring(2) + '\n';
						}
                    }

                    return matchingOption.TrimEnd("\n".ToCharArray());
                }
			}

			return null;
		}
	}
}