using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletElf : BaseElf
{
	public ParticleSystem breakPar;
	public GameObject BoomTrigger;
	public AudioSource ExplorAudio;

	public override void Init()
	{
		base.Init();
	}

	public void Update()
	{
		if (!isDead)
			transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);

		if (transform.localPosition.z >= DetectRange) {
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		TriggerEnter(other);
	}

	public virtual void TriggerEnter(Collider other) {}

	protected IEnumerator WaitingForWeaponParticle() {
		breakPar.Play();

		while (breakPar.isPlaying) {
			yield return null;
		}

		Destroy(this.gameObject);
	}
}
