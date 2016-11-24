using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardController : MovingObject
{
	private Animator _animator;
	 
	// Use this for initialization
	protected override void Start ()
	{
		this._animator = this.GetComponent<Animator> ();
		base.Start ();
	}

	// Update is called once per frame
	void Update ()
	{  
		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)(Input.GetAxisRaw ("Horizontal"));
		vertical = (int)(Input.GetAxisRaw ("Vertical"));

		if (horizontal != 0)
			vertical = 0;

		bool IsWalking = (Mathf.Abs (horizontal) + Mathf.Abs (vertical)) == 1;


		if (IsWalking && Input.anyKeyDown && !this.isMoving) {
			RaycastHit2D hit;
			this._animator.SetFloat ("X", horizontal);
			this._animator.SetFloat ("Y", vertical);
			this.Move (horizontal, vertical, out hit);	
		}
		//Debug.Log ("this.isMoving : "+this.isMoving);
		this._animator.SetBool ("IsWalking", this.isMoving);
	}
}
