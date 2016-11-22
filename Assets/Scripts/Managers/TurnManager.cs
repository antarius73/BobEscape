using UnityEngine;
using System.Collections;

/// <summary>
/// Handle turn mechanism between the player, the enemy and the game world.
/// </summary>
public class TurnManager : MonoBehaviour, IGameManager
{

	private bool _PlayerTurn;

	/// <summary>
	/// indicate if it is the player turn.
	/// </summary>
	/// <value><c>true</c> if player turn; otherwise, <c>false</c>.</value>
	public bool PlayerTurn {
		get {
			return this._PlayerTurn;
		}
		set { 
			this._PlayerTurn = value;
		}
	}

	public ManagerStatus Status {
		get;
		private set;
	}

	public void Startup ()
	{

		this.PlayerTurn = true;


		this.Status = ManagerStatus.Started;
	}
}