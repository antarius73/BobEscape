using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Manage movement and animation of a Simple enemy who hot at contact.
/// </summary>
public class SimpleEnemyController : MovingCharactereController
{
	/// <summary>
	/// The target player for this enemy
	/// </summary>
	private GameObject _target;

	private Vector3 _predictionMove;

	/// <summary>
	/// Gets the prediction move for the current game turn.
	/// </summary>
	/// <value>The prediction move.</value>
	public Vector3 PredictionMove {
		get {
			return this._predictionMove;
		}
	}

	protected override void Start ()
	{		
		this.Horizontal = -1;
		this.Vertical = 0;
		this.MoveEndEvent = GameEvent.ENEMY_MOVE_END;
		this._target = GameObject.FindGameObjectWithTag ("Player");
		this._predictionMove = this.transform.position;
		base.Start ();
	}

	private void Awake ()
	{
		Messenger.AddListener (GameEvent.ENEMY_MOVE_PREDICTION_START, OnEnemyMovePredictionStart);
		Messenger.AddListener (GameEvent.ENEMY_MOVE_START, OnEnemyMoveStart);
	}

	private void OnEnemyMoveStart ()
	{
		this.MoveToPredictedDestination ();
	}

	private void OnEnemyMovePredictionStart ()
	{
		this.PredictNextMove ();
	}

	/// <summary>
	/// Set the next predicate move 
	/// </summary>
	private void PredictNextMove ()
	{
		this._predictionMove = Managers.Mission.GetNextMoveTo (this.transform.position, this._target.transform.position);
		Messenger.Broadcast (GameEvent.ENEMY_MOVE_PREDICTION_END);
	}

	/// <summary>
	/// Trigger movement to the current predicate move
	/// </summary>
	private void MoveToPredictedDestination ()
	{		
		if (this.PredictionMove != this.transform.position) {
			this.TriggerMovement (this._predictionMove.x, this._predictionMove.y);
		} else {
			Messenger.Broadcast (GameEvent.ENEMY_MOVE_END);
		}
	}
}