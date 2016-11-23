using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileSelector : MonoBehaviour {

	/// <summary>
	/// Layer containing gameobjects who block movement.
	/// </summary>
	public LayerMask BlockingLayer;

	public GameObject MovementSelector;

	private List<GameObject> _lstTilesSelectable;
	private GameObject[] _ReachableTiles;


	// Use this for initialization
	void Start () {

		this._lstTilesSelectable = new List<GameObject> ();
		this._ReachableTiles = GameObject.FindGameObjectsWithTag("ReachableTile");

		/*
		this._boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, this.BlockingLayer);
		this._boxCollider.enabled = true;
		if (hit.transform == null) {
		*/
	
	}

	void Awake(){
		Messenger.AddListener (GameEvent.PLAYER_TURN_START, onPlayerTurnStart);
		Messenger<float,float>.AddListener (GameEvent.PLAYER_DESTINATION_SELECTED, onPlayerDestinationSelected);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void onPlayerDestinationSelected(float xDest,float yDest){
		this.ClearAllSelectableTile ();
	}

	private void onPlayerTurnStart(){
		this.ShowMouvementReachableTile ();
	}

	private void ShowMouvementReachableTile(){
		Debug.Log ("ShowMouvementReachableTile");


		Vector2 start = transform.position;
	
		float distance;

		foreach (var tile in this._ReachableTiles) {

			distance = TileSelector.ManhattanDistance2D (start, tile.transform.position);

			if (distance <= Managers.Player.MovementTileSpeed && distance>0) {
			
				this.InstantiateSelectableTile (tile.transform.position);

			}
		}


	
	}

	private static float ManhattanDistance2D(Vector2 start,Vector2 end){
		return Mathf.Abs (end.x - start.x) + Mathf.Abs (end.y - start.y);
	}

	private void InstantiateSelectableTile(Vector2 position){
		GameObject selectableTile;
		selectableTile = Instantiate(this.MovementSelector);
		selectableTile.transform.position = position;
		//selectableTile.transform.parent = this.transform;
	
		this._lstTilesSelectable.Add (selectableTile);
	}
	private void ClearAllSelectableTile(){
	
		foreach (var tile in this._lstTilesSelectable) {
			MonoBehaviour.Destroy (tile);
		}
	}


}
