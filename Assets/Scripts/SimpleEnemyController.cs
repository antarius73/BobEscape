using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Manage movement and animation of a Simple enemy who hot at contact.
/// </summary>
public class SimpleEnemyController : MovingCharactereController
{
	private int _life;
	public int Life {
		get {
			return this._life;
		}
	}

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
		Messenger<float,float,int>.AddListener (GameEvent.DAMAGE_ON_TILE, OnDamageOnTile);
	}

	private void OnEnemyMoveStart ()
	{
		this.MoveToPredictedDestination ();
	}

	private void OnEnemyMovePredictionStart ()
	{
		this.PredictNextMove ();
	}

	private void OnDamageOnTile(float tileX, float tileY, int damageAmount){
	
		if (this.transform.position.x == tileX && this.transform.position.y == tileY) {
			this.TakeDamage (damageAmount);
		} 
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

	public void TakeDamage(int damageAmount){
		this._life -= damageAmount;
		if (this.Life <= 0) {
			this.destroyMe ();
		}
	}

	public void destroyMe(){
		Messenger.RemoveListener (GameEvent.ENEMY_MOVE_PREDICTION_START, OnEnemyMovePredictionStart);
		Messenger.RemoveListener (GameEvent.ENEMY_MOVE_START, OnEnemyMoveStart);
		Messenger<float,float,int>.RemoveListener (GameEvent.DAMAGE_ON_TILE, OnDamageOnTile);
		MonoBehaviour.Destroy (this.gameObject, 3);
	}
}