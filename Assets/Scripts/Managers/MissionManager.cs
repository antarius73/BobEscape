using UnityEngine;
using System.Collections;
using AStar;
using System.Collections.Generic;

/// <summary>
/// Mission manager. Handle levels management through the game.
/// </summary>
public class MissionManager : MonoBehaviour, IGameManager
{

	private const int _maxLevelX = 12;
	private const int _maxLevelY = 6;

	/// <summary>
	/// The base map for A* pathfinding.
	/// </summary>
	private bool[,] _baseMap;

	/// <summary>
	/// Array of all current enemies.
	/// </summary>
	private GameObject[] _enemies;

	/// <summary>
	/// Use for detect that all enemies have respond to an event
	/// </summary>
	private int _enemiesCounter;

	/// <summary>
	/// Array of all current generator
	/// </summary>
	private GameObject[] _generators;

	/// <summary>
	/// Use for detect that all gerators have respond to an event
	/// </summary>
	private int __generatorCounter;


	/// <summary>
	/// Gets the status.
	/// </summary>
	/// <value>The status.</value>
	public ManagerStatus Status {
		get;
		private set;
	}

	/// <summary>
	/// Startup this instance.
	/// </summary>
	public void Startup ()
	{
		this.SetBaseMap ();
		this.Status = ManagerStatus.Started;
	}

	void Awake ()
	{
		Messenger.AddListener (GameEvent.ENEMY_TURN_START, OnEnemyTurnStart);
		Messenger.AddListener (GameEvent.ENEMY_MOVE_PREDICTION_END, OnEnemyMovePredictionEnd);
		Messenger.AddListener (GameEvent.ENEMY_MOVE_END, OnEnemyMoveEnd);
		Messenger.AddListener (GameEvent.WORLD_TURN_START, OnWorldTurnStart);
		Messenger.AddListener (GameEvent.WORLD_ITEM_TURN_END, OnWorldItemTurnEnd);
	}

	private void OnEnemyMoveEnd ()
	{
		this.RegisterEnemiesMoveEnd ();
	}

	private void OnEnemyTurnStart ()
	{
		
		this.LunchEnemiesMovePrediction ();
	}

	private void OnEnemyMovePredictionEnd ()
	{
		this.RegisterEnemiesMovePrediction ();
	}

	private void OnWorldTurnStart(){
		this.LunchGeneratorAction ();
	}

	private void OnWorldItemTurnEnd(){
		this.RegisterGeneratorActionEnded ();
	}

	/// <summary>
	/// Lunch event ENEMY_MOVE_START when all prediction are made
	/// </summary>
	private void RegisterEnemiesMovePrediction ()
	{
		this._enemiesCounter++;
		if (this._enemiesCounter == this._enemies.Length) {
			this._enemiesCounter = 0;
			Messenger.Broadcast (GameEvent.ENEMY_MOVE_START);
		}
	}

	/// <summary>
	/// Lunch event ENEMY_TURN_END when all enemies movements are made
	/// </summary>
	private void RegisterEnemiesMoveEnd ()
	{
		this._enemiesCounter++;
		if (this._enemiesCounter == this._enemies.Length) {
			Messenger.Broadcast (GameEvent.ENEMY_TURN_END);
		}
	}

	/// <summary>
	/// Trigger the ENEMY_MOVE_PREDICTION_START to all enemies
	/// </summary>
	private void LunchEnemiesMovePrediction ()
	{
		this._enemiesCounter = 0;
		this._enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (this._enemies.Length > 0)
			Messenger.Broadcast (GameEvent.ENEMY_MOVE_PREDICTION_START);
		else {
			Messenger.Broadcast (GameEvent.ENEMY_TURN_END);
		}
	}

	/// <summary>
	/// Trigger the WORLD_ITEM_TURN_START to all generator of the game
	/// </summary>
	private void LunchGeneratorAction(){
		this.__generatorCounter = 0;
		this._generators = this.GetGameObjectFromSubTags ("Generator");
		Messenger.Broadcast (GameEvent.WORLD_ITEM_TURN_START);

	}

	/// <summary>
	/// Lunch event WORLD_TURN_END when all generator actions are made
	/// </summary>
	private void RegisterGeneratorActionEnded ()
	{
		this.__generatorCounter++;
		if (this.__generatorCounter == this._generators.Length) {
			Messenger.Broadcast (GameEvent.WORLD_TURN_END);
		}
	}

	/// <summary>
	/// Check if a movement is in the bound of the game. 
	/// </summary>
	/// <returns><c>true</c>, if movement was alloweded, <c>false</c> otherwise.</returns>
	/// <param name="end">End. Vector to the destination tile</param>
	public bool AllowedMovement (Vector2 end)
	{	
		return end.x >= 0 && end.y >= 0 && end.x <= MissionManager._maxLevelX && end.y <= MissionManager._maxLevelY;
	}

	/// <summary>
	/// Set the base map with blocking wall.
	/// </summary>
	private void SetBaseMap ()
	{
		this._baseMap = new bool[_maxLevelX + 1, _maxLevelY + 1];
		this.initialiseDefaultFloorMap ();
		this.addWallToMap ();
	}

	/// <summary>
	/// Initialises the default floor map without blocking.
	/// </summary>
	private void initialiseDefaultFloorMap ()
	{
		for (int i = 0; i <= _maxLevelX; i++) {
			for (int j = 0; j <= _maxLevelY; j++) {
				this._baseMap [i, j] = true;
			}
		}
	}

	/// <summary>
	/// Adds the wall to map.
	/// </summary>
	private void addWallToMap ()
	{
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Wall");	

		foreach (var tile in tiles) {
			this._baseMap [(int)tile.transform.position.x, (int)tile.transform.position.y] = false;
		}	
	}

	/// <summary>
	/// Use A* pathfinding from current to target position
	/// </summary>
	/// <returns>The next move to the target</returns>
	/// <param name="currentPos">Current position.</param>
	/// <param name="targetPos">Target position.</param>
	public Vector3 GetNextMoveTo (Vector3 currentPos, Vector3 targetPos)
	{		
		bool[,] currentMap = this.GetBaseMapWithBlockingEnemies ();

		SearchParameters searchParameters = new SearchParameters (currentPos, targetPos, currentMap);
		PathFinder path = new PathFinder (searchParameters);
		List<Vector3> findedPath = path.FindPath ();

		if (findedPath.Count > 1)
			return findedPath [0];
		else
			return currentPos;
	}


	public Vector3 GetFacingTo(Vector3 currentPos, Vector3 targetPos)
	{		
		bool[,] currentMap = this._baseMap;

		SearchParameters searchParameters = new SearchParameters (currentPos, targetPos, currentMap);
		PathFinder path = new PathFinder (searchParameters);
		List<Vector3> findedPath = path.FindPath ();

		if (findedPath.Count > 1) 
			return findedPath [0] - currentPos;		
		else
			return currentPos;
	}




	/// <summary>
	/// Add all enemies to the default level map
	/// </summary>
	/// <returns>A copie of the base map with blocking enemies.</returns>
	private bool[,] GetBaseMapWithBlockingEnemies ()
	{
		bool[,] currentMap = (bool[,])this._baseMap.Clone ();
		SimpleEnemyController enemyCtrl;

		foreach (var enemy in this._enemies) {
			enemyCtrl = enemy.GetComponent<SimpleEnemyController> ();
			currentMap [(int)enemyCtrl.PredictionMove.x, (int)enemyCtrl.PredictionMove.y] = false;
		}

		return currentMap;
	}

	/// <summary>
	/// Detect presence of enemy on a particular tile.
	/// </summary>
	/// <returns><c>true</c>, if an enemy on this is detected, <c>false</c> otherwise.</returns>
	/// <param name="tile">Tile to check</param>
	public bool CheckIfEnemyOnTile (Vector3 tile)
	{
		GameObject[] EnemyTiles = GameObject.FindGameObjectsWithTag ("Enemy");	

		foreach (var Enemy in EnemyTiles) {
			if (Enemy.transform.position == tile)
				return true;
		}
		return false;
	}

	/// <summary>
	/// Gets the list of gameObject who have at list an child tagged with the subtag.
	/// The purpose of this methode is to allowed, via a workaround multiple, tagged prefab instance
	/// </summary>
	/// <returns>The game object from sub tags.</returns>
	/// <param name="subTag">Sub tag.</param>
	private GameObject[] GetGameObjectFromSubTags(string subTag)
	{
		GameObject[] subObjects = GameObject.FindGameObjectsWithTag(subTag);
		GameObject[] parentObjects = new GameObject[subObjects.Length];

		for (int i = 0; i < subObjects.Length; i++) {
				parentObjects[i]= subObjects[i].transform.parent.gameObject;
		}

		return parentObjects;
	}
}