using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropableItem : MonoBehaviour {


    /// <summary>
	/// The target player for this enemy
	/// </summary>
	private GameObject _player;

    protected void Start()
    {
       
        this._player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.WORLD_ITEM_TURN_START, OnWorldItemTurnStart);
    }

    private void OnWorldItemTurnStart()
    {
        Debug.Log("tour objet");
        if(this.transform.position == this._player.transform.position)
        {

            Debug.Log("joueur sur objet");
            this.LunchItemEffect();
        }
    }

    protected abstract void LunchItemEffect();
}