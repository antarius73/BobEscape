using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Manage movement and animation of a MovingObject controlled by Mouse or simple touch
/// </summary>
public class MovingCharactere :  MovingObject
{
	/// <summary>
	/// The prefab animator.
	/// </summary>
	private Animator _animator;

	protected Animator Animator {
		get {
			return this._animator;
		}
	}

	private int _horizontal;
	/// <summary>
	/// Gets or sets the horizontal orientation of the charactere.
	/// </summary>
	public int Horizontal {
		get {
			return this._horizontal;
		}
		set {
			this._horizontal = value;
		}
	}

	private int _vertical;
	/// <summary>
	/// Gets or sets the vertical orientation of the charactere.
	/// </summary>
	public int Vertical {
		get {
			return this._vertical;
		}
		set {
			this._vertical = value;
		}
	}

	private string _moveEndEvent;
	/// <summary>
	/// Gets or sets the move end event to trigger when movement is ending.
	/// </summary>
	/// <value>The move end event.</value>
	protected string MoveEndEvent {
		get {
			return this._moveEndEvent;
		}
		set {
			this._moveEndEvent = value;
		}
	}

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
				Messenger.Broadcast (this.MoveEndEvent);
			}
			base.isMoving = value;
		}
	}

	protected override void Start ()
	{		
		this._animator = this.GetComponent<Animator> ();
		base.Start ();
	}

	/// <summary>
	/// Execute movement to a destination
	/// </summary>
	/// <param name="xDest">X destination.</param>
	/// <param name="yDest">Y destination.</param>
	protected void TriggerMovement (float xDest, float yDest)
	{
		this._horizontal = Convert.ToInt32 (xDest - this.transform.position.x);
		this._vertical = Convert.ToInt32 (yDest - this.transform.position.y);
		this.Move (this._horizontal, this._vertical);	
	}
		
	void Update ()
	{  	
		this._animator.SetFloat ("X", this._horizontal);
		this._animator.SetFloat ("Y", this._vertical);
		this._animator.SetBool ("IsWalking", this.isMoving);
	}
}