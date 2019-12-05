using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionsCallback : MonoBehaviour
{
	private EnemyElf manager;

	public void Init(EnemyElf manager)
	{
		this.manager = manager;
	}

	public void BornedFinished()
	{
		manager.BornedFinished();
	}

	public void AttackFinishedCallback()
	{
		manager.AttackFinished();
	}

	public void DeadFinished()
	{
		manager.DeadFinished();
	}
}
