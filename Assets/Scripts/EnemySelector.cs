using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manage the tile selector system for the player movement.
/// </summary>
public class EnemySelector : TileSelector
{
	/// <summary>
	/// Containe all tile reachable by the player in the level.
	/// </summary>
	private GameObject[] _EnemiesTiles;



	/// <summary>
	/// The player collider.
	/// </summary>
	private Collider2D _PlayerCollider;

	// Use this for initialization
	protected override void Start ()
	{
		
		this._PlayerCollider = this.GetComponent<Collider2D> ();
		base.Start ();
	}

	void Awake ()
	{
		Debug.Log ("EnemySelector Awake");
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
		this.ShowEnemyInRangeTile ();
	}

	/// <summary>
	/// Show UI tile for movement of the player
	/// </summary>
	private void ShowEnemyInRangeTile ()
	{
		Vector2 start = transform.position;	
		float distance;

		this._EnemiesTiles = GameObject.FindGameObjectsWithTag ("Enemy");

	
		foreach (var tile in this._EnemiesTiles) {
			
			distance = EnemySelector.ManhattanDistance2D (start, tile.transform.position);
			if (distance <= Managers.Player.SpellRange && distance > 0 && this.HasLineOfSightOnTarget(tile.transform.position)) {
			
				this.InstantiateSelectableTile (tile.transform.position);

			}
		}	
	}

	private bool HasLineOfSightOnTarget(Vector3 target){
		
		this._PlayerCollider.enabled = false;
	
		// lineCast third parameter wait somthing like 0000010000
		RaycastHit2D hit = Physics2D.Linecast (this.transform.position, target,1<<LayerMask.NameToLayer("BlockingLayer"));
		this._PlayerCollider.enabled = true;

		if (hit.collider != null && hit.transform.position==target) {
			return true;
		}

		return false;
	}
}
