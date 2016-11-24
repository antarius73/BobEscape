using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manage the tile selector system for the player movement.
/// </summary>
public class TileSelector : MonoBehaviour
{
	/// <summary>
	/// Layer containing gameobjects who block movement.
	/// </summary>
	public LayerMask BlockingLayer;

	/// <summary>
	/// Asset tile specific for selectionUI purpose.
	/// </summary>
	public GameObject MovementSelector;

	/// <summary>
	/// List of tile generated for section UI purpose. 
	/// </summary>
	private List<GameObject> _lstTilesSelectable;

	/// <summary>
	/// Containe all tile reachable by the player in the level.
	/// </summary>
	private GameObject[] _ReachableTiles;

	// Use this for initialization
	void Start ()
	{
		this._lstTilesSelectable = new List<GameObject> ();
		this._ReachableTiles = GameObject.FindGameObjectsWithTag ("ReachableTile");	
	}

	void Awake ()
	{
		Messenger.AddListener (GameEvent.PLAYER_TURN_START, onPlayerTurnStart);
		Messenger<float,float>.AddListener (GameEvent.PLAYER_DESTINATION_SELECTED, onPlayerDestinationSelected);
	}

	private void onPlayerDestinationSelected (float xDest, float yDest)
	{
		this.ClearAllSelectableTile ();
	}

	private void onPlayerTurnStart ()
	{
		this.ShowMouvementReachableTile ();
	}

	/// <summary>
	/// Show UI tile for movement of the player
	/// </summary>
	private void ShowMouvementReachableTile ()
	{
		Vector2 start = transform.position;	
		float distance;

		foreach (var tile in this._ReachableTiles) {
			
			distance = TileSelector.ManhattanDistance2D (start, tile.transform.position);

			if (distance <= Managers.Player.MovementTileSpeed && distance > 0) {
			
				this.InstantiateSelectableTile (tile.transform.position);

			}
		}	
	}

	private static float ManhattanDistance2D (Vector2 start, Vector2 end)
	{
		return Mathf.Abs (end.x - start.x) + Mathf.Abs (end.y - start.y);
	}

	private void InstantiateSelectableTile (Vector2 position)
	{
		GameObject selectableTile;
		selectableTile = Instantiate (this.MovementSelector);
		selectableTile.transform.position = position;
		this._lstTilesSelectable.Add (selectableTile);
	}

	/// <summary>
	/// Delete all the UI tile when the destination has been choosen.
	/// </summary>
	private void ClearAllSelectableTile ()
	{	
		foreach (var tile in this._lstTilesSelectable) {
			MonoBehaviour.Destroy (tile);
		}
	}
}
