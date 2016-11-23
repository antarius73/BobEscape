using UnityEngine;
using System.Collections;

public class SelectableMovementTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Debug.Log ("clic");
		Messenger<float,float>.Broadcast (GameEvent.PLAYER_DESTINATION_SELECTED, this.transform.position.x, this.transform.position.y);
	}
}
