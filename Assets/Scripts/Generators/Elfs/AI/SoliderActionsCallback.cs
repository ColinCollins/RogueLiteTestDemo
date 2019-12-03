using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderActionsCallback : MonoBehaviour
{
	private SoliderElf manager;

	public void Init (SoliderElf manager)
	{
		this.manager = manager;
	}

	public void BornedFinished()
	{
		manager.BornedFinished();
	}

	public void AttackFinishedCallback() {
		manager.AttackFinished();
	}

	public void FindEnemyAnimFinished() {
		manager.FindEnemyAnimFinished();
	}

	public void DeadFinished() {
		manager.DeadFinished();
	}
}
