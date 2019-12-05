using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions
{
	public static void Idle(EnemyElf handle)
	{
		// stay
		handle.PlayBornedAnim();
		handle.transform.localEulerAngles = Vector3.zero;
		handle.Entity.localScale = Vector3.one * 5;
	}

	public static void MoveStraight(EnemyElf handle)
	{
		handle.transform.Translate(Vector3.back * handle.MoveSpeed * Time.deltaTime);
	}

	public static void MoveDestination(EnemyElf handle)
	{
		if (handle.Target == null) return;

		Vector3 targetDirection = handle.Target.transform.position - handle.transform.position;
		float singleStep = handle.RotateSpeed * Time.deltaTime;
		Vector3 newDirection = Vector3.RotateTowards(-handle.transform.forward, targetDirection, singleStep, 0.0f);
		handle.transform.rotation = Quaternion.LookRotation(newDirection);

		handle.transform.Translate(handle.transform.forward * handle.MoveSpeed * Time.deltaTime);
	}

	public static void FindEnemy(EnemyElf handle)
	{

	}

	public static void Attack(EnemyElf handle)
	{
		handle.PlayAttack();
	}

	public static void Dead(EnemyElf handle)
	{
		handle.PlayDead();
	}
}
