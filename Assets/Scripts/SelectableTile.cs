using UnityEngine;
using System.Collections;

/// <summary>
/// Component for selection tile of the player movement
/// </summary>
public class SelectableTile : MonoBehaviour
{
	public enmGameEvents TriggerOnSelection;

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D cubHit = Physics2D.Raycast (mousePosition, Vector2.zero);
			if (cubHit.transform.position == this.transform.position)
				Messenger<float,float>.Broadcast (this.TriggerOnSelection.ToString (), this.transform.position.x, this.transform.position.y);	
		}	
	}
}