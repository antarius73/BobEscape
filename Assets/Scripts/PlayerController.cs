using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Manage movement and animation of the player Charactere
/// </summary>
public class PlayerController :  MovingCharactereController
{

	protected override void Start ()
	{
		this.Horizontal = 1;
		this.Vertical = 0;
		this.MoveEndEvent = GameEvent.PLAYER_MOVE_END;
		base.Start ();
	}

	private void Awake ()
	{
		Messenger<float,float>.AddListener (GameEvent.PLAYER_DESTINATION_SELECTED, onPlayerDestinationSelected);
	}

	private void onPlayerDestinationSelected (float xDest, float yDest)
	{		
		this.TriggerMovement (xDest, yDest);
	}


}