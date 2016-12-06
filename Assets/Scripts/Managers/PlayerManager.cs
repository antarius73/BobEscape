using UnityEngine;
using System.Collections;


/// <summary>
/// Manage player datas and actions througth the all game.
/// </summary>
public class PlayerManager : MonoBehaviour, IGameManager
{
	private int _movementTileSpeed= 1;
	public int MovementTileSpeed {
		get {
			return this._movementTileSpeed;
		}
	}

	private int _spellDamage = 1;
	public int SpellDamage {
		get {
			return this._spellDamage;
		}
	}

	private float _spellSpeed = 15f;
	public float SpellSpeed {
		get {
			return this._spellSpeed;
		}
	}

	private int _spellRange=5;
	public int SpellRange {
		get {
			return _spellRange;
		}
	}

	public GameObject SpellVisualEffect;

	public GameObject SpellExplosionEffect;

	public ManagerStatus Status {
		get;
		private set;
	}

	public void Startup ()
	{
		this.Status = ManagerStatus.Started;
	}
}
