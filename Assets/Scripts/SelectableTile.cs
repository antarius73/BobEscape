using UnityEngine;
using System.Collections;

/// <summary>
/// Component for selection tile of the player movement
/// </summary>
public class SelectableTile : MonoBehaviour
{
	public enmGameEvents TriggerOnSelection;
	
	void OnMouseDown ()
	{
		Messenger<float,float>.Broadcast (this.TriggerOnSelection.ToString(), this.transform.position.x, this.transform.position.y);
	}
}
