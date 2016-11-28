using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Manage movement and animation of a Simple enemy who hot at contact.
/// </summary>
public class SimpleEnemyController : MovingCharactereController {

	/// <summary>
	/// The target player for this enemy
	/// </summary>
	private GameObject _target;

	protected override void Start ()
	{		
		this.Horizontal = -1;
		this.Vertical = 0;
		this.MoveEndEvent = GameEvent.ENEMY_TURN_END;
		this._target = GameObject.FindGameObjectWithTag ("Player");
		base.Start ();
	}

	private void Awake ()
	{
		Messenger.AddListener (GameEvent.ENEMY_TURN_START, onEnemyTurnStart);
	}

	private void onEnemyTurnStart ()
	{		
		Vector3 targetTile = Managers.Mission.GetNextMoveTo (this.transform.position, this._target.transform.position);
		this.TriggerMovement (targetTile.x, targetTile.y);
	}
}
