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
		Messenger<float,float>.AddListener(GameEvent.PLAYER_TARGET_SELECTED, OnPlayerTargetSelected);
	}

	private void onPlayerDestinationSelected (float xDest, float yDest)
	{		
		this.TriggerMovement (xDest, yDest);
	}

	private  void OnPlayerTargetSelected(float xDest, float yDest){

	
		Vector3 TargetDirection =	Managers.Mission.GetFacingTo (this.transform.position, new Vector3(xDest,yDest,0));

		this.Horizontal = (int)TargetDirection.x;
		this.Vertical = (int)TargetDirection.y;



		this.Animator.SetBool ("IsAttacking", true);

	}

	private void OnPlayerAttackEnd(){
		this.Animator.SetBool ("IsAttacking", false);
	}


}