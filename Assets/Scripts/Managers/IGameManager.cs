using System.Collections;
using UnityEngine;

/// <summary>
/// they persist all along the game and manage actions and data from start to the end of the game.
/// </summary>
public interface IGameManager
{
	/// <summary>
	/// Gets the current status.
	/// </summary>
	/// <value>Current status</value>
	ManagerStatus Status{ get; }

	/// <summary>
	/// Startup this instance.
	/// </summary>
	void Startup ();
}
