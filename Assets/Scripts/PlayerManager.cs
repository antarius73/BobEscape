using UnityEngine;
using System.Collections;


/// <summary>
/// Manage player datas and actions througth the all game.
/// </summary>
public class PlayerManager : MonoBehaviour, IGameManager
{

	public ManagerStatus Status {
		get;
		private set;
	}

	public void Startup ()
	{
		this.Status = ManagerStatus.Started;
	}
}
