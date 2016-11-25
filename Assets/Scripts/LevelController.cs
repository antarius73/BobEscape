using UnityEngine;
using System.Collections;
using AStar;

public class LevelController  {

	private int _width;
	private int _height;

	private bool[,] _baseMap;
	private bool[,] _currentMap;

	private SearchParameters _searchParameters;

	public LevelController (int width, int height)
	{
		this._width = width;
		this._height = height;
		this._currentMap= new bool[this._width, this._height];
			this._baseMap = new bool[this._width, this._height];
			
	}

	private void InitialiaseBaseMap(){
		this.initialiseDefaultFloorMap ();
		this.addWallToMap ();
	}

	private void InitialiseCurrentMap() {
		this._currentMap = (bool[,])this._baseMap.Clone();
		this.addEnemyToMap ();
		this.addplayerToMap ();
	}

	private void initialiseDefaultFloorMap(){
		for (int i = 0; i < this._width; i++) {
			for (int j = 0; j < this._height; j++) {
				this._baseMap [i, j] = true;
			}
		}
	}

	private void addWallToMap(){
		this.BlockMapTaggedBy ("Wall", false);		
	}

	private void addEnemyToMap(){
		this.BlockMapTaggedBy ("Enemy", true);	
	}

	private void addplayerToMap(){
		this.BlockMapTaggedBy ("Player", true);	
	}


	private void BlockMapTaggedBy(string tag, bool onCurrentMap){
	
		GameObject[] tiles  = GameObject.FindGameObjectsWithTag (tag);	

		foreach (var tile in tiles) {
			if (onCurrentMap) {
				this._baseMap [(int)tile.transform.position.x, (int)tile.transform.position.y] = false;
			} else {
				this._currentMap [(int)tile.transform.position.x, (int)tile.transform.position.y] = false;
			}
		}
	}


	public Vector3 GetNextMoveTo(Vector3 currentPos,Vector3 target){
		return new Vector3 ();
	}

}
