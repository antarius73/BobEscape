using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manage the tile selector system for the player movement.
/// </summary>
public class TileSelector : MonoBehaviour
{
	/// <summary>
	/// Layer containing gameobjects who block LOS.
	/// </summary>
	public LayerMask BlockingLayer;

	/// <summary>
	/// Asset tile specific for selectionUI purpose.
	/// </summary>
	public GameObject SelectorType;

	/// <summary>
	/// List of tile generated for selection UI purpose. 
	/// </summary>
	private List<GameObject> _lstTilesSelectable;

	// Use this for initialization
	protected virtual void Start ()
	{
		this._lstTilesSelectable = new List<GameObject> ();
	}

	protected static float ManhattanDistance2D (Vector2 start, Vector2 end)
	{
		return Mathf.Abs (end.x - start.x) + Mathf.Abs (end.y - start.y);
	}

	/// <summary>
	/// Instantiates a selectable tile for UI.
	/// </summary>
	/// <param name="position">Position.</param>
	protected void InstantiateSelectableTile (Vector2 position)
	{
		GameObject selectableTile;
		selectableTile = Instantiate (this.SelectorType);
		selectableTile.transform.position = position;
		this._lstTilesSelectable.Add (selectableTile);
	}

	/// <summary>
	/// Delete all the UI tile when the destination has been choosen.
	/// </summary>
	protected void ClearAllSelectableTile ()
	{	
		foreach (var tile in this._lstTilesSelectable) {
			MonoBehaviour.Destroy (tile);
		}
	}


}
