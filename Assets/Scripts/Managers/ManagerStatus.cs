/// <summary>
/// Differents possibles states of a IGameManager
/// </summary>
public enum ManagerStatus
{
	Shutdown = 0,
	/// <summary>
	/// The initialisation in progress, not yet ready to work.
	/// </summary>
	Initializing = 1,
	/// <summary>
	/// The started and ready to work
	/// </summary>
	Started = 2
}
