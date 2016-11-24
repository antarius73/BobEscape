using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Manage movement and animation of a MovingObject controlled by Mouse or simple touch
/// </summary>
public class MouseController :  MovingObject
{
	private Animator _animator;

	private int _horizontal;
	private int _vertical;

	/// <summary>
	/// Redefine setter, need to warn others of the movement end
	/// </summary>
	/// <value><c>true</c> if is moving; otherwise, <c>false</c>.</value>
	protected override bool isMoving {
		get {
			return base.isMoving;
		}
		set {
			if (base.isMoving == true && value == false) {
				Messenger.Broadcast (GameEvent.PLAYER_MOVE_END);
			}
			base.isMoving = value;
		}
	}

	// Use this for initialization
	protected override void Start ()
	{		
		this._animator = this.GetComponent<Animator> ();
		this._horizontal = 1;
		this._vertical = 0;
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

	private void TriggerMovement (float xDest, float yDest)
	{
		this._horizontal = Convert.ToInt32 (xDest - this.transform.position.x);
		this._vertical = Convert.ToInt32 (yDest - this.transform.position.y);
		RaycastHit2D hit;
		this.Move (this._horizontal, this._vertical, out hit);	
	}

	void Update ()
	{  	
		this._animator.SetFloat ("X", this._horizontal);
		this._animator.SetFloat ("Y", this._vertical);
		this._animator.SetBool ("IsWalking", this.isMoving);
	}
}