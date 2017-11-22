using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : DropableItem
{
    public int EnergyAmount = 50; 

    protected override void LunchItemEffect()
    {
        Debug.Log("player on potion");
        Messenger<int>.Broadcast(GameEvent.PLAYER_ENERGY_CHANGE, this.EnergyAmount);


    }
}
