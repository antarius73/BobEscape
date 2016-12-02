using UnityEngine;
using System.Collections;

public class FireBall : MovingObject {


	public const int SpellDamage = 1;
	public const float SpellSpeed = 15f;

	private Vector3 _target;
	public Vector3 Target {
		get {
			return this._target;
		}
		set {
			this._target = value;
		}
	}

 

	protected override void Start ()
	{
		base.Start ();
		this.Movetime = FireBall.SpellSpeed;
		this.MoveEndEvent = GameEvent.PLAYER_ATTACK_START;
		this.MoveToTarget ();

	}



	private void MoveToTarget(){


		Vector3 MovementVector = this.Target - this.transform.position;


		this.Move ((int)MovementVector.x, (int)MovementVector.y);
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



}
