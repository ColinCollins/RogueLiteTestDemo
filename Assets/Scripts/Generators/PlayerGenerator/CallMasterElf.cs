using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CallMasterElf : BaseElf
{
	// callMaster init position
	private static Vector3 initPos = new Vector3(0, 0, -13);

	private bool isMoving = false;
	private Vector3 dest = default(Vector3);
	private static float callMasterMovingRange = 12;

	// temporary 
	public CallMasterAttackCtrl AttackCtrl;

	public override void Init()
	{
		base.Init();

		anim = GetComponentInChildren<Animator>();
		transform.localPosition = initPos;

		AttackCtrl = GetComponent<CallMasterAttackCtrl>();
		AttackCtrl.Init(this);
	}

	public void SetMovingTarget(float barValue)
	{
		isMoving = true;
		anim.SetBool("Run", isMoving);

		float posX = (barValue - 0.5f) * callMasterMovingRange;
		dest = new Vector3(posX, initPos.y, initPos.z);
	}

	public void ShotAttack()
	{
		Vector3 centerPos = transform.localPosition;

		float rx = Random.Range(-0.2f, 0.2f);
		float interval = 1.0f;
		if (!AttackCtrl.IsMagic)
		{
			AttackCtrl.ShotAttack(new Vector3(centerPos.x - interval + rx, centerPos.y, centerPos.z));
			AttackCtrl.ShotAttack(centerPos);
			AttackCtrl.ShotAttack(new Vector3(centerPos.x + interval + rx, centerPos.y, centerPos.z));
		}
		else {
			AttackCtrl.ShotAttack(new Vector3(centerPos.x - (interval / 2f) + rx, centerPos.y, centerPos.z));
			AttackCtrl.ShotAttack(new Vector3(centerPos.x + (interval / 2f) + rx, centerPos.y, centerPos.z));
		}

	}

	private void Update()
	{
		if (dest == default(Vector3) || isMoving == false) return;

		Vector3 targetDirection = dest - transform.localPosition;
		float singleStep = RotateSpeed * Time.deltaTime;
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
		Entity.transform.rotation = Quaternion.LookRotation(newDirection);

		transform.localPosition = Vector3.MoveTowards(transform.localPosition, dest, MoveSpeed * Time.deltaTime);

		float dis = Vector3.Distance(dest, transform.localPosition);
		if (dis < 0.5f)
		{
			StopMoving();
		}
	}

	private void StopMoving()
	{
		Vector3 targetDirection = Vector3.forward - transform.localPosition;
		float singleStep = RotateSpeed * Time.deltaTime;
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
		Entity.transform.rotation = Quaternion.LookRotation(newDirection);

		if (transform.rotation.y < 10f)
		{
			transform.eulerAngles = Vector3.zero;
			dest = default(Vector3);
			isMoving = false;
			anim.SetBool("Run", isMoving);
		}
	}
}
