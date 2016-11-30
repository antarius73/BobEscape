using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy generator. Manage spwaning of enemies in the game
/// </summary>
public class EnemyGenerator : MonoBehaviour
{

	/// <summary>
	/// The type of the enemy to spawn
	/// </summary>
	public GameObject EnemyType;

	/// <summary>
	/// probability to spawn each turn
	/// </summary>
	public float SpawnProbability;

	private void Awake ()
	{
		Messenger.AddListener (GameEvent.WORLD_ITEM_TURN_START, OnWorldItemTurnStart);
	}

	private void OnWorldItemTurnStart ()
	{
		this.DoGeneratorAction ();
	}

	/// <summary>
	/// If space is aviable and test is ok, spawn an enemy on the generator
	/// </summary>
	private void DoGeneratorAction ()
	{
		if (this.SpawnProbabilityTest () && this.GeneratorSpaceAvaible ()) {
			this.GenerateEnemy ();
		} else {
			Messenger.Broadcast (GameEvent.WORLD_ITEM_TURN_END);
		}
	}

	/// <summary>
	/// Generates an enemy on the generator.
	/// </summary>
	private void GenerateEnemy ()
	{
		GameObject spawnEnemy = Instantiate (this.EnemyType);
		spawnEnemy.transform.position = this.transform.position;
		Messenger.Broadcast (GameEvent.WORLD_ITEM_TURN_END);
	}

	/// <summary>
	/// Test the spawning probabilities
	/// </summary>
	/// <returns><c>true</c>, if probability test was spawned, <c>false</c> otherwise.</returns>
	private bool SpawnProbabilityTest ()
	{
		return Random.value >= this.SpawnProbability;	

	}

	/// <summary>
	/// Check if the generator tile is aviable for spwaning.
	/// </summary>
	/// <returns><c>true</c>, if space avaible was generatored, <c>false</c> otherwise.</returns>
	private bool GeneratorSpaceAvaible ()
	{

		GameObject[] currentEnemies = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (var enemy in currentEnemies) {
			if (this.transform.position == enemy.transform.position)
				return false;
		}

		return true;
	}
}
