using UnityEngine;
using System.Collections;
using AStar;
using System.Collections.Generic;

public class LevelController
{

	private int _width;
	private int _height;

	private bool[,] _baseMap;
	private bool[,] _currentMap;

	public LevelController (int width, int height)
	{
		this._width = width;
		this._height = height;
		this._currentMap = new bool[this._width, this._height];
		this._baseMap = new bool[this._width, this._height];
		this.InitialiseBaseMap ();
			
	}

	private void InitialiseBaseMap ()
	{
		this.initialiseDefaultFloorMap ();
		this.addWallToMap ();
	}

	private void UpdateCurrentMap ()
	{
		

		this._currentMap = (bool[,])this._baseMap.Clone ();
		//this.addEnemyToMap ();
		//this.addplayerToMap ();
	}

	private void initialiseDefaultFloorMap ()
	{
		for (int i = 0; i < this._width; i++) {
			for (int j = 0; j < this._height; j++) {
				this._baseMap [i, j] = true;
			}
		}
	}

	private void addWallToMap ()
	{
		this.BlockTilesTaggedBy ("Wall", false);		
	}

	private void addEnemyToMap ()
	{
		this.BlockTilesTaggedBy ("Enemy", true);	
	}

	private void addplayerToMap ()
	{
		this.BlockTilesTaggedBy ("Player", true);	
	}


	private void BlockTilesTaggedBy (string tag, bool onCurrentMap)
	{
	
		GameObject[] tiles = GameObject.FindGameObjectsWithTag (tag);	

		foreach (var tile in tiles) {
			if (onCurrentMap) {
				this._currentMap [(int)tile.transform.position.x, (int)tile.transform.position.y] = false;
			} else {
				this._baseMap [(int)tile.transform.position.x, (int)tile.transform.position.y] = false;
			}
		}
	}


	public Vector3 GetNextMoveTo (Vector3 currentPos, Vector3 targetPos)
	{
		Debug.Log ("currentPos  : " + currentPos.x + "," + currentPos.y);
		Debug.Log ("target pos : " + targetPos.x + "," + targetPos.y);
		this.UpdateCurrentMap ();

		SearchParameters searchParameters = new SearchParameters (currentPos, targetPos, this._currentMap);
		PathFinder path = new PathFinder (searchParameters);

		List<Vector3> findedPath = path.FindPath ();
		this.debugPath (findedPath);


		if (findedPath.Count > 0) 
			return findedPath [0];
		
		else
			return currentPos;
			
	}

	private void debugPath(List<Vector3> findedPath)
	{
		string pathLog = "path : ";
		foreach (var item in findedPath) {
			pathLog +=item.x+","+item.y+" ";
		}
		Debug.Log (pathLog);
	}

	public bool CheckIfEnemyOnTile(Vector3 tile){
		GameObject[] EnemyTiles = GameObject.FindGameObjectsWithTag ("Enemy");	


		foreach (var Enemy in EnemyTiles) {
			if (Enemy.transform.position == tile)
				return true;
			
		}
		return false;
	}

}
