using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour, IGameManager {

	public ManagerStatus Status {
		get;
		private set;
	}

	public void Startup(){
		this.Status = ManagerStatus.Started;
	}
}