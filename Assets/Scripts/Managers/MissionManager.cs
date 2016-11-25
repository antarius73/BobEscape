using UnityEngine;
using System.Collections;

/// <summary>
/// Mission manager. Handle levels management through the game.
/// </summary>
public class MissionManager : MonoBehaviour, IGameManager
{

	private const int _maxLevelX = 12;
	private const int _maxLevelY = 6;

	private LevelController _currentLevel;
	public LevelController CurrentLevel {
		get {
			return _currentLevel;
		}
	}

	/// <summary>
	/// Gets the status.
	/// </summary>
	/// <value>The status.</value>
	public ManagerStatus Status {
		get;
		private set;
	}

	/// <summary>
	/// Startup this instance.
	/// </summary>
	public void Startup ()
	{
		
		this.Status = ManagerStatus.Started;
	}

	/// <summary>
	/// Check if a movement is in the bound of the game. 
	/// </summary>
	/// <returns><c>true</c>, if movement was alloweded, <c>false</c> otherwise.</returns>
	/// <param name="end">End. Vector to the destination tile</param>
	public bool AllowedMovement (Vector2 end)
	{	
		return end.x >= 0 && end.y >= 0 && end.x <= MissionManager._maxLevelX && end.y <= MissionManager._maxLevelY;
	}

}
