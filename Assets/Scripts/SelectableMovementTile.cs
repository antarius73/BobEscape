using UnityEngine;
using System.Collections;

/// <summary>
/// Component for selection tile of the player movement
/// </summary>
public class SelectableMovementTile : MonoBehaviour
{
	void OnMouseDown ()
	{
		Messenger<float,float>.Broadcast (GameEvent.PLAYER_DESTINATION_SELECTED, this.transform.position.x, this.transform.position.y);
	}
}
