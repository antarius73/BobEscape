using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour, IGameManager {


	private bool _PlayerTurn;
	public bool PlayerTurn {
		get {
			return this._PlayerTurn;
		}
		set{ 
			this._PlayerTurn = value;
		}
	}

	private bool _PlayerMove;
	public bool PlayerMove {
		get {
			return this._PlayerMove;
		}
		set{ 
			this._PlayerMove = value;
		}
	}

	public ManagerStatus Status {
		get;
		private set;
	}

	public void Startup(){

		this.PlayerTurn = true;
		this.PlayerMove = false;

		this.Status = ManagerStatus.Started;
	}
}