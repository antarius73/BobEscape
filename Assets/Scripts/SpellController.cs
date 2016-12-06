using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour
{
	private GameObject _currentSpellEffet;
	private GameObject _currentExplosionEffet;
	private Vector3 _currentTargetPos;

	private void Awake ()
	{
		Messenger<float,float>.AddListener (GameEvent.PLAYER_TARGET_SELECTED, OnPlayerTargetSelected);
		Messenger.AddListener (GameEvent.PLAYER_ATTACK_START, OnPlayerAttackStart);
	}

	private void OnPlayerTargetSelected (float targetX, float targetY)
	{
		this.CastSpellToTarget (targetX, targetY);
	}

	private void OnPlayerAttackStart ()
	{
		this.DestroyCurrentSpell ();
		this.TriggerExplosionOnTarget ();
	}

	private void DestroyCurrentSpell ()
	{
		MonoBehaviour.Destroy (this._currentSpellEffet);
	}

	private void CastSpellToTarget (float targetX, float targetY)
	{
		this._currentSpellEffet = MonoBehaviour.Instantiate (Managers.Player.SpellVisualEffect);
		this._currentSpellEffet.transform.position = this.transform.position;

		MovingSpell fireball = this._currentSpellEffet.GetComponent<MovingSpell> ();
		this._currentTargetPos = new Vector3 (targetX, targetY, 0);
		fireball.Target = this._currentTargetPos;
	}

	private void TriggerExplosionOnTarget ()
	{
		this._currentExplosionEffet = MonoBehaviour.Instantiate (Managers.Player.SpellExplosionEffect);
		this._currentExplosionEffet.transform.position = this._currentTargetPos;
		this.DamageThingsOnTargetTile ();
		MonoBehaviour.Destroy (this._currentExplosionEffet, 3);
		StartCoroutine (DelayAttackEnd ());	
	}

	private IEnumerator DelayAttackEnd ()
	{
		yield return new WaitForSeconds (3);
		Messenger.Broadcast (GameEvent.PLAYER_ATTACK_END);
	}

	private void DamageThingsOnTargetTile ()
	{
		Messenger<float,float,int>.Broadcast (GameEvent.DAMAGE_ON_TILE, this._currentTargetPos.x, this._currentTargetPos.y, Managers.Player.SpellDamage);
	}
}