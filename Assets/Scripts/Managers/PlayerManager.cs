using UnityEngine;
using System.Collections;


/// <summary>
/// Manage player datas and actions througth the all game.
/// </summary>
public class PlayerManager : MonoBehaviour, IGameManager
{
	private int _movementTileSpeed;
	public int MovementTileSpeed {
		get {
			return this._movementTileSpeed;
		}
	}

	public ManagerStatus Status {
		get;
		private set;
	}

	public void Startup ()
	{
		this._movementTileSpeed = 1;
		this.Status = ManagerStatus.Started;
	}
}
