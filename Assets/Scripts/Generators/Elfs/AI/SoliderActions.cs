using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderActions
{
	public static void Idle(SoliderElf handle)
	{
		// stay
	}

	public static void MoveStraight(SoliderElf handle)
	{
		handle.transform.Translate(Vector3.forward * handle.MoveSpeed * Time.deltaTime);
	}

	public static void MoveDestination(SoliderElf handle)
	{
		if (handle.Target == null) return;

		Vector3 targetDirection = handle.Target.transform.position - handle.transform.position;
		float singleStep = handle.RotateSpeed * Time.deltaTime;
		Vector3 newDirection = Vector3.RotateTowards(handle.transform.forward, targetDirection, singleStep, 0.0f);
		handle.transform.rotation = Quaternion.LookRotation(newDirection);

		handle.transform.Translate(handle.transform.forward * handle.MoveSpeed * Time.deltaTime);
	}

	public static void FindEnemy(SoliderElf handle)
	{

	}

	public static void Attack(SoliderElf handle)
	{

	}

	public static void Dead(SoliderElf handle)
	{
		
	}
}
