using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manage the tile selector system for the player movement.
/// </summary>
public class MouvementSelector : TileSelector
{

	/// <summary>
	/// Containe all tile reachable by the player in the level.
	/// </summary>
	private GameObject[] _ReachableTiles;

	// Use this for initialization
	protected override void Start ()
	{
		this._ReachableTiles = GameObject.FindGameObjectsWithTag ("ReachableTile");	
		base.Start ();
	}

	void Awake ()
	{
		Messenger.AddListener (GameEvent.PLAYER_TURN_START, onPlayerTurnStart);
		Messenger<float,float>.AddListener (GameEvent.PLAYER_DESTINATION_SELECTED, OnActionSelected);
		Messenger<float,float>.AddListener (GameEvent.PLAYER_TARGET_SELECTED, OnActionSelected);
	}

	private void OnActionSelected (float xDest, float yDest)
	{
		this.ClearAllSelectableTile ();
	}

	private void onPlayerTurnStart ()
	{
		this.ShowMouvementReachableTile ();
	}

	/// <summary>
	/// Show UI tile for movement of the player
	/// </summary>
	private void ShowMouvementReachableTile ()
	{
		Vector2 start = transform.position;	
		float distance;

		foreach (var tile in this._ReachableTiles) {
			
			distance = MouvementSelector.ManhattanDistance2D (start, tile.transform.position);

			if (distance <= Managers.Player.MovementTileSpeed && distance > 0 && !Managers.Mission.CheckIfEnemyOnTile (tile.transform.position)) {
			
				this.InstantiateSelectableTile (tile.transform.position);

			}
		}	
	}
}
