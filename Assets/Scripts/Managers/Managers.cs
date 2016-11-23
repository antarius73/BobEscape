using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This component or require at compilation
[RequireComponent (typeof(PlayerManager))]
[RequireComponent (typeof(TurnManager))]
[RequireComponent (typeof(MissionManager))]

/// <summary>
/// Manage and centralise all others manager through the game
/// </summary>
public class Managers : MonoBehaviour
{
	public static PlayerManager Player{ get; private set; }

	public static TurnManager Turn{ get; private set; }

	public static MissionManager Mission{ get; private set; }

	private IList<IGameManager> _startSequence;

	/// <summary>
	/// Call when the class is loaded.
	/// </summary>
	void Awake ()
	{
		//this object must survive through the all game, even when a new level is loaded.
		MonoBehaviour.DontDestroyOnLoad (this.gameObject);

		// Get the component added in the unity IDE
		Managers.Player = this.GetComponent<PlayerManager> ();
		Managers.Turn = this.GetComponent<TurnManager> ();
		Managers.Mission = this.GetComponent<MissionManager> ();

		this._startSequence = new List<IGameManager> ();
		this._startSequence.Add (Managers.Player);
		this._startSequence.Add (Managers.Turn);
		this._startSequence.Add (Managers.Mission);

		// call a subroutine
		this.StartCoroutine (this.StartupManagers ());
	}

	/// <summary>
	/// a coroutine don't need to be executed through the same frame. it can wait subsequent frame to continue execution.
	/// </summary>
	/// <returns>a coroutine return always a IEnumerator</returns>
	private IEnumerator StartupManagers ()
	{	
		foreach (var manager in this._startSequence) {
			manager.Startup ();
		}

		// Yield mark a point where the coroutine can pause between two frames
		yield return null;

		int numModules = this._startSequence.Count;
		int numModulesReady = 0;

		// Loading of all managers is asynchrone, so we must wait for all to be done
		while (numModulesReady < numModules) {
			int lastModuleReady = numModulesReady;
			numModulesReady = 0;
			
			foreach (var manager in this._startSequence) {
				if (manager.Status == ManagerStatus.Started) {
					numModulesReady++;
				}
			}

			if (numModulesReady > lastModuleReady) {
				Debug.Log ("Loading progress " + numModulesReady + "/" + numModules);
			}
		}

		yield return null;
		Debug.Log ("All managers started up");
		Messenger.Broadcast (GameEvent.MANAGERS_STARTED);

	}
}
