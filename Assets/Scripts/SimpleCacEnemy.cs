using UnityEngine;
using System.Collections;
using System;

public class SimpleCacEnemy : MovingObject {

	private GameObject _target;

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
				Messenger.Broadcast (GameEvent.ENEMY_TURN_END);
			}
			base.isMoving = value;
		}
	}



	// Use this for initialization
	protected override void Start ()
	{		
		this._animator = this.GetComponent<Animator> ();
		this._target = GameObject.FindGameObjectWithTag ("Player");
		this._horizontal = -1;
		this._vertical = 0;
		base.Start ();
	}

	private void Awake ()
	{
		Messenger.AddListener (GameEvent.ENEMY_TURN_START, onEnemyTurnStart);
	}


	private void onEnemyTurnStart ()
	{		

		Vector3 targetTile = Managers.Mission.CurrentLevel.GetNextMoveTo (this.transform.position, this._target.transform.position);

		this.TriggerMovement (targetTile.x, targetTile.y);
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
