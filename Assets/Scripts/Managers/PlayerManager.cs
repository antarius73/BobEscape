using UnityEngine;
using System.Collections;
using System;

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

    private int _movementEnergyCost = 1;
    public int MovementEnergyCost
    {
        get
        {
            return _movementEnergyCost;
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

    private int _spellEnergyCost = 5;
    public int SpellEnergyCost
    {
        get
        {
            return _spellEnergyCost;
        }
    }

    private int _currentEnergy = 5;
    public int CurrentEnergy
    {
        get
        {
            return _currentEnergy;
        }

        set
        {
            _currentEnergy = value;
           
            CheckEnergyModification();

        }
    }

    private int _maximumEnergy = 100;
    public int MaximumEnergy
    {
        get
        {
            return _maximumEnergy;
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

    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_MOVE_END, OnPlayerMoveEnd);
        Messenger.AddListener(GameEvent.PLAYER_ATTACK_END, OnPlayerAttackEnd);
    }

    private void OnPlayerAttackEnd()
    {
        this.RemoveSpellEnergyCost();
        Managers.GameUI.RefreshUI();
    }

    private void OnPlayerMoveEnd()
    {
        this.RemoveMovementEnergyCost();
        Managers.GameUI.RefreshUI();
    }

    private void RemoveMovementEnergyCost() {
        this.CurrentEnergy -= this.MovementEnergyCost;
    }

    private void RemoveSpellEnergyCost()
    {
        this.CurrentEnergy -= this.SpellEnergyCost;
    }


    private void CheckEnergyModification()
    {
        if (this.CurrentEnergy > this.MaximumEnergy) this._currentEnergy = this.MaximumEnergy;
        else if (this.CurrentEnergy <= 0) Messenger.Broadcast(GameEvent.PLAYER_DEATH_START);
    }    
}
