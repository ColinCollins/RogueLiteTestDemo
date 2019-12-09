using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagicWeaponElf : BulletElf
{

	public override void TriggerEnter(Collider other)
	{
		if (other.transform.tag.Equals("Enemy") && !isDead)
		{
			isDead = true;
			Destroy(Entity.gameObject);

			StartCoroutine(WaitingForWeaponParticle());
			BoomTrigger.transform.DOScale(Vector3.one * AttackRange, 0.3f);
			ExplorAudio.Play();
		}
	}
}
