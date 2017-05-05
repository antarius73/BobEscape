﻿using UnityEngine;
using System.Collections;

public class GameEvent
{
	public const string MANAGERS_STARTED = "MANAGERS_STARTED";
	public const string PLAYER_TURN_START = "PLAYER_TURN_START";
	public const string PLAYER_DESTINATION_SELECTED = "PLAYER_DESTINATION_SELECTED";
	public const string PLAYER_TARGET_SELECTED = "PLAYER_TARGET_SELECTED";
	public const string PLAYER_MOVE_START = "PLAYER_MOVE_START";
	public const string PLAYER_ATTACK_START = "PLAYER_ATTACK_START";
	public const string PLAYER_ATTACK_END = "PLAYER_ATTACK_END";
	public const string PLAYER_MOVE_END = "PLAYER_MOVE_END";
	public const string PLAYER_TURN_END = "PLAYER_TURN_END";
    public const string PLAYER_DEATH_START = "PLAYER_DEATH_START";
    public const string PLAYER_DEATH_END = "PLAYER_DEATH_END";

    public const string ENEMY_TURN_START = "ENEMY_TURN_START";
	public const string ENEMY_MOVE_PREDICTION_START = "ENEMY_MOVE_PREDICTION_START";
	public const string ENEMY_MOVE_PREDICTION_END = "ENEMY_MOVE_PREDICTION_END";
	public const string ENEMY_MOVE_START = "ENEMY_MOVE_START";
	public const string ENEMY_MOVE_END = "ENEMY_MOVE_END";
	public const string ENEMY_TURN_END = "ENEMY_TURN_END";

	public const string WORLD_TURN_START = "WORLD_TURN_START";
	public const string WORLD_ITEM_TURN_START = "WORLD_ITEM_TURN_START";
	public const string WORLD_ITEM_TURN_END = "WORLD_ITEM_TURN_END";
	public const string WORLD_TURN_END = "WORLD_TURN_END";

	public const string DAMAGE_ON_TILE = "DAMAGE_ON_TILE";


}

public enum enmGameEvents
{
	MANAGERS_STARTED = 1,
	PLAYER_TURN_START = 2,
	PLAYER_DESTINATION_SELECTED = 3,
	PLAYER_TARGET_SELECTED = 4,
	PLAYER_MOVE_START = 5,
	PLAYER_ATTACK_START = 6,
	PLAYER_ATTACK_END = 7,
	PLAYER_MOVE_END = 8,
	PLAYER_TURN_END = 9,
	ENEMY_TURN_START = 10,
	ENEMY_MOVE_PREDICTION_START = 11,
	ENEMY_MOVE_PREDICTION_END = 12,
	ENEMY_MOVE_START = 13,
	ENEMY_MOVE_END = 14,
	ENEMY_TURN_END = 15,
	WORLD_TURN_START = 16,
	WORLD_ITEM_TURN_START = 17,
	WORLD_ITEM_TURN_END = 18,
	WORLD_TURN_END = 19,
	DAMAGE_ON_TILE =20,
    PLAYER_DEATH_START = 21,
    PLAYER_DEATH_END = 22
}
