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

	private GameObject[] _enemies;

	private int _enemiesCounter;

	public int EnemiesCounter {
		get {
			return this._enemiesCounter;
		}
		set {
			this._enemiesCounter = value;
			Debug.Log ("_enemiesCounter=" + this._enemiesCounter);
		}
	}

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
	}

	private void OnEnemyMoveEnd(){
		this.RegisterEnemiesMoveEnd ();
	}

	private void OnEnemyTurnStart(){
		
		this.LunchEnemiesMovePrediction ();
	}

	private void OnEnemyMovePredictionEnd(){
		this.RegisterEnemiesMovePrediction ();
	}

	private void RegisterEnemiesMovePrediction(){
		this.EnemiesCounter++;
		if (this.EnemiesCounter == this._enemies.Length) {
			Debug.Log ("mise a zero");
			this.EnemiesCounter = 0;
			Debug.Log ("all prediction ok");
			Messenger.Broadcast (GameEvent.ENEMY_MOVE_START);

		}
	}


	private void RegisterEnemiesMoveEnd(){
		this.EnemiesCounter++;
		if (this.EnemiesCounter == this._enemies.Length) {
			Debug.Log ("all move ok");
			Messenger.Broadcast (GameEvent.ENEMY_TURN_END);
		}
	}


	private void LunchEnemiesMovePrediction(){
		Debug.Log ("mise a zero");
		this.EnemiesCounter = 0;
		this._enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		Messenger.Broadcast (GameEvent.ENEMY_MOVE_PREDICTION_START);
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

	private bool[,] GetBaseMapWithBlockingEnemies(){
		bool[,] currentMap = (bool[,])this._baseMap.Clone ();
		SimpleEnemyController enemyCtrl;

		foreach (var enemy in this._enemies) {
			enemyCtrl = enemy.GetComponent<SimpleEnemyController>();
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
}