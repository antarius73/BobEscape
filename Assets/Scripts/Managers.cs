using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ces objet seront requis à la compilation
[RequireComponent (typeof(PlayerManager))]
[RequireComponent (typeof(TurnManager))]

public class Managers : MonoBehaviour
{

	// list des managers gerer par la classe
	public static PlayerManager Player{ get; private set; }
	public static TurnManager Turn{ get; private set; }

	private IList<IGameManager> _startSequence;

	// methode appelée quand la classe est loadée
	void Awake ()
	{
		// cet objet doit survire quelque soit la scene 
		MonoBehaviour.DontDestroyOnLoad (this.gameObject);

		// recuperation du composent établi visuelement pour le lié a la variable
		Managers.Player = this.GetComponent<PlayerManager> ();
		Managers.Turn = this.GetComponent<TurnManager> ();

		this._startSequence = new List<IGameManager> ();
		this._startSequence.Add (Managers.Player);
		this._startSequence.Add (Managers.Turn);

		// appeler une coroutine
		this.StartCoroutine (this.StartupManagers ());
	}


	// coroutine
	// une methode qui n'a pas besoin d'etre executer durant la meme frame
	private IEnumerator StartupManagers ()
	{	

		foreach (var manager in this._startSequence) {
			manager.Startup ();
		}

		// point de pose de la coroutine , elle reprendra de la a la prochaine frame
		yield return null;

		int numModules = this._startSequence.Count;
		int numModulesReady = 0;

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
	}
}
