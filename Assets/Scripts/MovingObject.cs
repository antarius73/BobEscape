using UnityEngine;
using System.Collections;

/// <summary>
/// A movingObject can move himeself from a tile to another with a smooth movement.
/// </summary>
public abstract class MovingObject : MonoBehaviour
{
	/// <summary>
	/// speed factor for the movment between the tiles.
	/// </summary>
	public const float Movetime = 3f;

	/// <summary>
	/// Layer containing gameobjects who block movement.
	/// </summary>
	public LayerMask BlockingLayer;

	/// <summary>
	/// The box collider of this object
	/// </summary>
	private BoxCollider2D _boxCollider;

	private Rigidbody2D _rb2D;

	private bool _isMoving;

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="MovingObject"/> is moving.
	/// </summary>
	/// <value><c>true</c> if is moving; otherwise, <c>false</c>.</value>
	protected virtual bool isMoving {
		get {
			return _isMoving;
		}
		set {
			_isMoving = value;
		}
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	protected virtual void Start ()
	{
		this.isMoving = false;
		this._boxCollider = GetComponent<BoxCollider2D> ();
		this._rb2D = GetComponent<Rigidbody2D> ();      
	}

	/// <summary>
	/// Move the gameobect to the xDir and yDir destination tile.
	/// </summary>
	/// <param name="xDir">X destination</param>
	/// <param name="yDir">Y destination</param>
	/// <param name="hit">Hit result if blocked movement</param>
	protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		//Debug.Log ("X : "+end.x+"Y : "+end.y);

		// colision test with the blocking layer.
		this._boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, this.BlockingLayer);
		this._boxCollider.enabled = true;

		// check if movement in game bound
		if (!Managers.Mission.AllowedMovement (end))
			return false;
		
		// if all is ok, start the movement
		if (hit.transform == null) {
			// indicate that a movement is in progress
			this.isMoving = true;
			StartCoroutine (SmoothMovement (end));
			return true;
		}
		return false;
	}

	/// <summary>
	/// Coroutine, Smooths movement to the destination tile.
	/// </summary>
	/// <returns>The movement.</returns>
	/// <param name="end">Destination Vector</param>
	protected IEnumerator SmoothMovement (Vector3 end)
	{
		// distance betwwen acual position and destination
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		//while the distance is not negligible
		while (sqrRemainingDistance > float.Epsilon) {
			// calcul next step of the movement
			Vector3 newPosition = Vector3.MoveTowards (this._rb2D.position, end, Movetime * Time.deltaTime);
			// do the step
			this._rb2D.MovePosition (newPosition);
			// recalculate distance from destination
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			// indicate this object have finishing his move
			if (sqrRemainingDistance <= float.Epsilon)
				this.isMoving = false;

			yield return null;
		}
	}
}
