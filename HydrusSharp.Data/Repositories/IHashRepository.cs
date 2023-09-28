using HydrusSharp.Data.Models.ClientMaster;

/// <summary>
/// Retrieves data relating to saved hashes
/// </summary>
public interface IHashRepository
{
	/// <summary>
	/// Gets the hash which matches the given ID
	/// </summary>
	/// <param name="hashId"></param>
	Hash GetById(int hashId);
}