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

	void Awake ()
	{
		Messenger.AddListener (GameEvent.MANAGERS_STARTED, OnManagersStarted);
		Messenger.AddListener (GameEvent.PLAYER_MOVE_END, OnPlayerMoveEnd);
		Messenger.AddListener (GameEvent.ENEMY_TURN_END, OnEnemyTurnEnd);
		Messenger.AddListener (GameEvent.WORLD_TURN_END, OnWorldTurnEnd);
	}

	private void OnManagersStarted ()
	{
		this.StartTurn ();
	}

	private void StartTurn ()
	{
		Messenger.Broadcast (GameEvent.PLAYER_TURN_START);
	}

	private void OnPlayerMoveEnd ()
	{
		this.StartEnemiesTurn ();	
	}

	private void OnWorldTurnEnd(){
		this.StartPlayerTurn ();
	}

	private void OnEnemyTurnEnd ()
	{
		this.StartWorldTurn ();
	}
		
	private void StartEnemiesTurn ()
	{
		Messenger.Broadcast (GameEvent.ENEMY_TURN_START);
	}

	private void StartPlayerTurn ()
	{
		Messenger.Broadcast (GameEvent.PLAYER_TURN_START);
	}

	private void StartWorldTurn(){
		Messenger.Broadcast (GameEvent.WORLD_TURN_START);
	}
}